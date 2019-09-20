using System;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Localization;
using Nop.Services.Authentication;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Messages;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.Themes.PAA.ViewModels;

namespace Nop.Web.DallasArt.Services
{
  

    public class ProcessRegistation : IProcessRegistration
    {
        #region Fields
         
        private readonly CustomerSettings _customerSettings;
        private readonly IStoreContext _storeContext;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly LocalizationSettings _localizationSettings;
        private readonly IEventPublisher _eventPublisher;
        private readonly IWorkContext _workContext;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly ICustomerAttributeParser _customerAttributeParser;
        private readonly IMessageService _messageService;

        #endregion

        #region ctor
        public ProcessRegistation(
            IMessageService messageService,
            IStoreContext storeContext,
            CustomerSettings customerSettings,
            ICustomerRegistrationService customerRegistrationService,
            IGenericAttributeService genericAttributeService,
            IAuthenticationService authenticationService,
            IWorkflowMessageService workflowMessageService,
            LocalizationSettings localizationSettings,
            IEventPublisher eventPublisher,
            ICustomerAttributeService customerAttributeService,
            ICustomerAttributeParser customerAttributeParser,
            IWorkContext workContext

            )
        {
            _messageService = messageService;
            _customerSettings = customerSettings;
            _storeContext = storeContext;
            _customerRegistrationService = customerRegistrationService;
            _genericAttributeService = genericAttributeService;
            _authenticationService = authenticationService;
            _workflowMessageService = workflowMessageService;
            _localizationSettings = localizationSettings;
            _eventPublisher = eventPublisher;
            _customerAttributeService = customerAttributeService;
            _customerAttributeParser = customerAttributeParser;

            _workContext = workContext;

        }
          #endregion

        private void AddAttribute(string enteredText, CustomerAttribute attribute, ref string attributesXml)
        {
            if (!string.IsNullOrWhiteSpace(enteredText))
            {
                attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml, attribute, enteredText);
            }
        }

        private string PrepareCustomerAttributes(RegistrationViewModel model)
        {
            if (model == null) throw new ArgumentNullException("model");

            string attributesXml = "";
            var attributes = _customerAttributeService.GetAllCustomerAttributes();

            foreach (var attribute in attributes)
            {
                switch (attribute.Name)
                {
                    case "Center Name":
                     //   AddAttribute(model.CenterNameRequest.Trim(), attribute, ref attributesXml);
                        break;

                    case "Center Enrollment Id":
                     //   AddAttribute(model.CenterGroupIdRequest.Trim(), attribute, ref attributesXml);
                        break;

 
                    default:
                        break;
                }
            }
            return attributesXml;
        }


        public bool RegisterUser(RegistrationViewModel model)
        {

            bool isApproved = _customerSettings.UserRegistrationType == UserRegistrationType.Standard;

            Customer customer = model.CurrentCustomer;

            string customerAttributesXml = PrepareCustomerAttributes(model);

            var registrationRequest = new CustomerRegistrationRequest(
                customer,
                model.SelfIdentificationViewModel.Email,
                _customerSettings.UsernamesEnabled ? model.Username : model.SelfIdentificationViewModel.Email,
                model.SelfIdentificationViewModel.Password,
                _customerSettings.DefaultPasswordFormat,
                _storeContext.CurrentStore.Id,
                isApproved);

            var registrationResult = _customerRegistrationService.RegisterCustomer(registrationRequest);

            if (registrationResult.Success)
            {

                //save customer attributes
                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName,
                    model.SelfIdentificationViewModel.FirstName);

                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName,
                    model.SelfIdentificationViewModel.LastName);

                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone,
                    model.SelfIdentificationViewModel.Phone);
                
                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CustomCustomerAttributes,
                    customerAttributesXml);
            
                //login customer now
                if (isApproved) _authenticationService.SignIn(customer, true);

                // delay registration until enrollment is complete
                // if (model.IsEnrollment) return true;

                return AnnounceRegistation(customer);
            }

            return false;
        }


        public bool AnnounceRegistation(Customer customer)
        {
            //notifications
            if (_customerSettings.NotifyNewCustomerRegistration)
            {
                _workflowMessageService
                    .SendCustomerRegisteredNotificationMessage(customer, _localizationSettings.DefaultAdminLanguageId);
            }

            //raise event       
            _eventPublisher.Publish(new CustomerRegisteredEvent(customer));

            //send customer welcome message
            _messageService.SendRegistationWelcomeMessage(customer, _workContext.WorkingLanguage.Id) ;
            return true;
        }

    }
}