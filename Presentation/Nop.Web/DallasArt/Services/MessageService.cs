using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.DallasArt;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Stores;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Logging;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.Interfaces.DashBoard;
using Nop.Web.DallasArt.ViewModels;
using Nop.Core.Domain.Logging;
using Nop.Web.DallasArt.Context;

namespace Nop.Web.DallasArt.Services
{
    /*
    Customer Email
        EMail #1	Membership Invitation With EzLink 
        EMail #2	Satellite Enrollment Invitation with EzLink
        EMail #3	Membership Registration Welcome Letter
        EMail #4	Enrollment Welcome Letter With Instructions
        EMail #5	Help Auto-Response Acknowledgment
        EMail #6	Purchase Order Invoice To Customer
        EMail #7	Printed Membership Instruction
        EMail #8	Center Members Email Notices  
        EMail #9	Prayer Coordinator Invitation With EzLink 
       
     
Other
        EMail #50	Taylor Email Order Generation With FollowUp
        EMail #51	Customer Help Request To Gift of Prayer Program
        EMail #53	Gift of Prayer Membership Notification
  
  
ContactAutoResponseMessage   (via SendCustomerEmail)
ContactRequestMessage       (via SendCustomerEmail)
SendEnrollmentWelcomeMessage
SendSatelliteEzLinkInvitation
SendRegistationWelcomeMessage

     * */



    public class MessageService : IMessageService
    {
        #region fields

     //   private readonly IEnrollmentRequestService _enrollmentRequestService;
        private readonly IMessageTemplateService _messageTemplateService;
        private readonly IEmailAccountService _emailAccountService;
        private readonly EmailAccountSettings _emailAccountSettings;
        private readonly ILanguageService _languageService;
        private readonly IMessageTokenProvider _messageTokenProvider;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICustomerService _customerService;
        private readonly IWorkContext _workContext;
        private readonly IDallasArtContext _dallasArtContext;
  //      private readonly ICustomerAttributeService _customerAttributeService;

        private readonly Store _store;
        private readonly ICustomerRoleService _customerRoleService;
        private readonly ILogger _logger;

        //Message Templates 
        private const string SendEnrollmentWelcomeTemplate =         "PAA.SendEnrollmentWelcomeMessage";               // Enrollment.RequestMessage
        private const string ContactAutoResponseMessageTemplate =    "PAA.ContactAutoResponseMessage";            // Contact.AutoResponseMessage
        private const string ContactRequestMessageTemplate =         "PAA.ContactRequestMessage";                      // Contact.RequestMessage    
        private const string SendSatelliteEzLinkInvitationTemplate = "PAA.SendSatelliteEzLinkInvitation";      // Enrollment.Satellite.Notification 
        private const string SendRegistationWelcomeMessageTemplate = "PAA.SendRegistationWelcomeMessage";      // Registation.WelcomeMessage
        private const string InstructionsForMembershipTemplate =     "PAA.InstructionsForMembership";              // Registation.InstructionMessage
        private const string SendMembershipEzInvitationTemplate =    "PAA.SendRegistationEzLinkInvitation";       // Evantell.Membership.EzInvitation
        private const string SendCenterLocationGroupEmailTemplate =  "PAA.LocalCenterEmailMessage";              // Evantell.Membership.CenterEmail

        private const string SendPrayerCoordinatorMembershipEzInvitationTemplate = "Evantell.SendPrayerCoordinatorRegistationEzLinkInvitation";      

 

        #endregion

        #region Ctor

        public MessageService(
            IDallasArtContext dallasArtContext,
            ICustomerRoleService customerRoleService,
            IStoreContext storeContext,
            IMessageTokenProvider messageTokenProvider,
            ILanguageService languageService,
            EmailAccountSettings emailAccountSettings,
            IEmailAccountService emailAccountService,
            IMessageTemplateService messageTemplateService,
            IEventPublisher eventPublisher,
            ICustomerService customerService,
 //           IEnrollmentRequestService enrollmentRequestService,
            IWorkContext workContext,
 //           ICustomerAttributeService customerAttributeService,
            ILogger logger

            )
        {
            this._dallasArtContext = dallasArtContext;
            this._messageTokenProvider = messageTokenProvider;
            this._languageService = languageService;
            this._emailAccountSettings = emailAccountSettings;
            this._emailAccountService = emailAccountService;
            this._messageTemplateService = messageTemplateService;
            this._eventPublisher = eventPublisher;
            this._customerService = customerService;
         //   this._enrollmentRequestService = enrollmentRequestService;
            this._store = storeContext.CurrentStore;
            this._workContext = workContext;
            this._customerRoleService = customerRoleService;
 //           this._customerAttributeService = customerAttributeService;
            this._logger = logger;
        }

        #endregion

        #region utilities

     

        protected virtual MessageTemplate GetActiveMessageTemplate(string messageTemplateName, int storeId)
        {
            var t = _messageTemplateService.GetMessageTemplateByName(messageTemplateName, storeId);

            //no template found
            if (t == null) return null;

            //ensure it's active
            var isActive = t.IsActive;
            if (!isActive) return null;


            // create clone
            var nt = new MessageTemplate
            {
                Name = t.Name,
                BccEmailAddresses = t.BccEmailAddresses,
                Subject = t.Subject,
                Body = t.Body,
                IsActive = t.IsActive,
                AttachedDownloadId = t.AttachedDownloadId,
                EmailAccountId = t.EmailAccountId,
                LimitedToStores = t.LimitedToStores
            };

            return nt;
        }

        protected virtual EmailAccount GetEmailAccountOfMessageTemplate(MessageTemplate messageTemplate, int languageId)
        {
            var emailAccounId = messageTemplate.GetLocalized(mt => mt.EmailAccountId, languageId);
            var emailAccount = (_emailAccountService.GetEmailAccountById(emailAccounId) ??
                                _emailAccountService.GetEmailAccountById(_emailAccountSettings.DefaultEmailAccountId)) ??
                               _emailAccountService.GetAllEmailAccounts().FirstOrDefault();
            return emailAccount;

        }

        protected virtual int EnsureLanguageIsActive(int languageId, int storeId)
        {
            //load language by specified ID
            var language = _languageService.GetLanguageById(languageId);

            if (language == null || !language.Published)
            {
                //load any language from the specified store
                language = _languageService.GetAllLanguages(storeId: storeId).FirstOrDefault();
            }
            if (language == null || !language.Published)
            {
                //load any language
                language = _languageService.GetAllLanguages().FirstOrDefault();
            }

            if (language == null)
                throw new Exception("No active language could be loaded");
            return language.Id;
        }

        #endregion

        #region Prepare Outgoing Email Message

        private int SetupEmail(ref EmailAccount account, ref MessageTemplate messageTemplate, ref List<Token> tokens, Customer customer)
        {
            var languageId = EnsureLanguageIsActive(0, _store.Id);
            account = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            _messageTokenProvider.AddStoreTokens(tokens, _store, account);
            _messageTokenProvider.AddCustomerTokens(tokens, customer);

            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            return languageId;

        }



        private int SendCustomerEmail(Customer customer, MessageTemplate messageTemplate)
        {
            string toEmail, toName;
            EmailAccount emailAccount = null;
            List<Token> tokens = new List<Token>();

            int languageId = SetupEmail(ref emailAccount, ref messageTemplate, ref tokens, customer);


            // non-customer member get send address
            if (_dallasArtContext.IsContact)
            {
                var si = _dallasArtContext.ContactRequest.SelfIdentificationViewModel;
                toName = $"{si.FirstName} {si.LastName}";
                toEmail = si.Email;
            }
            else
            {
                toName = customer.GetFullName();
                toEmail = customer.Email;
            }

            return Utilities.QueueEmailNotification(
                                      messageTemplate,
                                      emailAccount,
                                      languageId,
                                      tokens,
                                      toEmail,
                                      toName
                                   );
        }


        private int SendMemberListEmail(Customer customer, MessageTemplate messageTemplate, List<string> members)
        {
            //email account
            EmailAccount emailAccount = null;
            List<Token> tokens = new List<Token>();
            int languageId = SetupEmail(ref emailAccount, ref messageTemplate, ref tokens, customer);

            
            if (customer == null)
            {
                _logger.InsertLog(LogLevel.Error, "SendMemberListEmail Customer Not Found" , "Email's - " + members);
                return 0;
            }
           


  
            int queueId = 0;
            foreach (string toEmail in members)
            {

                queueId = Utilities.QueueEmailNotification(
                    messageTemplate,
                    emailAccount,
                    languageId,
                    tokens,
                    toEmail,
                    "Center Member"
                    );

                if (queueId == 0) // fail
                {
                     _logger.InsertLog(LogLevel.Error, "Error creating ezEmail Link", " To Email Address - " + toEmail , customer);

                    continue; // queueId; // failed
                }

            }
            return queueId;
        }



        private int SendStoreOwnerEmail(Customer customer, MessageTemplate messageTemplate)
        {

            //email account
            EmailAccount emailAccount = null;
            List<Token> tokens = new List<Token>();
            int languageId = SetupEmail(ref emailAccount, ref messageTemplate, ref tokens, customer);

            // ToDo Review Addresses
            var toEmail = emailAccount.Username; // messageTemplate.BccEmailAddresses;
            var toName = messageTemplate.Name;

            return Utilities.QueueEmailNotification(
                                      messageTemplate,
                                      emailAccount,
                                      languageId,
                                      tokens,
                                      toEmail,
                                      toName
                                   );

        }


        #endregion Prepare Outgoing Email Message




        #region Email Messages

        /// <summary>
        /// Message From Customer To Store Owner
        /// </summary>
        /// <param name="contactRequest"></param>
        /// <returns></returns>
        public int ContactRequestMessage(ContactRequestViewModel contactRequest)
        {
            Customer customer = contactRequest.CurrentCustomer;
            if (customer == null) throw new ArgumentNullException("customer");

            var messageTemplate = GetActiveMessageTemplate(ContactRequestMessageTemplate, _store.Id);
            if (messageTemplate == null) throw new ArgumentNullException("messagetemplate");

            return SendStoreOwnerEmail(customer, messageTemplate);
        }


        /// <summary>
        /// Send Customer Contact A "Got It"
        /// (Help Auto-Response Acknowledgment) 
        /// </summary>
        /// <param name="contactRequest"></param>
        /// <returns></returns>
        public int ContactAutoResponseMessage(ContactRequestViewModel contactRequest)
        {
            Customer customer = contactRequest.CurrentCustomer;
            if (customer == null) throw new ArgumentNullException("customer");

            MessageTemplate messageTemplate = GetActiveMessageTemplate(ContactAutoResponseMessageTemplate, _store.Id);
            if (messageTemplate == null) throw new ArgumentNullException("template");

            return SendCustomerEmail(customer, messageTemplate);

        }



        /// <summary>
        /// Sends Welcome Message To Customer
        /// Template:
        /// SendRegistationWelcomeMessageTemplate
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>
        public int SendRegistationWelcomeMessage(Customer customer, int languageId)
        {
            if (customer == null) throw new ArgumentNullException("customer");

            var messageTemplate = GetActiveMessageTemplate(SendRegistationWelcomeMessageTemplate, _store.Id);
            if (messageTemplate == null) throw new ArgumentNullException(paramName: "template");

            return SendCustomerEmail(customer, messageTemplate);
        }



        /// <summary>
        /// Sends Enrollment Welcome Message To Customer
        /// Template:
        /// SendEnrollmentWelcomeTemplate
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public int SendEnrollmentWelcomeMessage(OrderPlacedEvent result)
        {
            var messageTemplate = GetActiveMessageTemplate(SendEnrollmentWelcomeTemplate, _store.Id);
            if (messageTemplate == null) return 0;

            if (result == null || result.Order == null || result.Order.CustomerId == 0) return 0;


            Order order = result.Order;
            Customer customer = _customerService.GetCustomerById(order.CustomerId);
            bool isEnrollment = order.OrderItems.Any(item => Utilities.IsEnrollment(item.ProductId));

            // make sure product was successfully ordered !!
            if (
                  !isEnrollment
                  || order.CaptureTransactionResult.ToLower() != "success"
                  || customer == null

                  ) return 0;


            return SendCustomerEmail(customer, messageTemplate);

        }



        ///// <summary>
        ///// EZLink Satellite Enrollment Invitation
        ///// Template:
        ///// SendSatelliteEzLinkInvitationTemplate
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public int SendSatelliteEzLinkInvitation(ISatelliteCenterViewModel model)
        //{
        //    if (model == null) return 0;

        //    var messageTemplate = GetActiveMessageTemplate(SendSatelliteEzLinkInvitationTemplate, _store.Id);
        //    if (messageTemplate == null) return 0;

        //    // redirect message to Satellite Leader's email address
        //    _dallasArtContext.ContactRequest.SelfIdentificationViewModel
        //        = new SelfIdentificationViewModel(_customerAttributeService)
        //        {
        //            Email = model.OriginalNotification,
        //            FirstName = "Center Enrollment Director",
        //            LastName = String.Empty
        //        };

        //    _dallasArtContext.IsSatellite = true;
        //    _dallasArtContext.Location =
        //            _enrollmentRequestService.GetSatelliteLocationByCenterName(model);

        //    return SendCustomerEmail(_workContext.CurrentCustomer, messageTemplate);
        //}




        /// <summary>
        /// Template:
        /// SendPrayerCoordinatorRegistationEzLinkInvitation
        /// </summary>
        /// <param name="addresses">Send message to addresses</param>
        /// <returns></returns>
        public int SendPrayerCoordinatorMembershipEzInvitation(List<string> addresses)
        {
            var messageTemplate = GetActiveMessageTemplate(SendPrayerCoordinatorMembershipEzInvitationTemplate, _store.Id);
            if (messageTemplate == null) return 0;

            if (addresses.Count > 0)
            {
                Customer customer = _workContext.CurrentCustomer;

           //     _dallasArtContext.Location =
           //         _enrollmentRequestService.GetEnrollmentLocationByOriginalCustomerId(customer.Id);


                return SendMemberListEmail(customer, messageTemplate, addresses);
            }
            return 0;
        }



        List<string> ParseEmailCollection(string emails)
        {
            string parsedList = emails
                  .Replace(Environment.NewLine, string.Empty)
                  .Replace(" ", ";")
                  .Replace(",",";")
                  .Replace(";;", ";")
                  ;


            return (from email in parsedList.Split(';') where email.Length > 0 select email.Trim()).ToList();
        }



        /// <summary>
        /// Template:
        /// SendMembershipEzInvitationTemplate
        /// </summary>
        /// <param name="model"></param>
        /// <param name="administrator"></param>
        /// <returns></returns>
        public int SendMembershipEzInvitation(IMembershipInstructionsViewModel model , Customer administrator )
        {
            var messageTemplate = GetActiveMessageTemplate(SendMembershipEzInvitationTemplate, _store.Id);
            if (messageTemplate == null || model == null || administrator == null) return 0;
           
            // reset to current administrator
           // _dallasArtContext.Location =
           //         _enrollmentRequestService.GetEnrollmentLocationByOriginalCustomerId(administrator.Id);

        //    MemberEnrollment location = _enrollmentRequestService.GetEnrollmentLocationByOriginalCustomerId(administrator.Id);



            if (   !string.IsNullOrWhiteSpace( model.EmailAddress))
            {
//                _logger.InsertLog(LogLevel.Information, "Membership Invitation From " + _dallasArtContext.Location.CenterGroupId,  "Raw Email's - " + model.EmailAddress, administrator);

 //               _logger.InsertLog(LogLevel.Information, "Membership Invitation From " + location.CenterGroupId, "Raw Email's - " + model.EmailAddress, administrator);


                var list = ParseEmailCollection(model.EmailAddress);

                StringBuilder b = new StringBuilder();

                foreach (var address  in list) { b.Append(address + ";" ) ;              }

 //              _logger.InsertLog(LogLevel.Information, "Parsed Membership Invitation From " + location.CenterGroupId , list.Count + " Parsed Email's - " + b.ToString(), administrator);

                
                return SendMemberListEmail(administrator, messageTemplate, list);
            }

            return 0;
        }




   

        #endregion Email Messages





    }
}
