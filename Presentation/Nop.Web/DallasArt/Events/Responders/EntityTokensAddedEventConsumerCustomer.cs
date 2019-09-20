
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Messages;
using Nop.Services.Events;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.Interfaces.Contact;

namespace Nop.Web.DallasArt.Events.Responders
{
    public interface IEntityTokensAddedEventConsumerCustomer
    {
    }

    public class EntityTokensAddedEventConsumerCustomer : IEntityTokensAddedEventConsumerCustomer, IConsumer<EntityTokensAddedEvent<Customer, Token>>
    {

        #region init

        private readonly IStoreContext _storeContext;
        private readonly IDallasArtContext _dallasArtContext;
      //  private readonly IEnrollmentRequestService _requestService;
        private readonly ILogger _logger;
        private readonly Customer _customer;
        private readonly IWorkContext _workContext;


        public EntityTokensAddedEventConsumerCustomer
            (
             IStoreContext storeContext,
             IDallasArtContext dallasArtContext,
             IWorkContext workContext,
       //      IEnrollmentRequestService requestService,
             ILogger logger
            )
        {
            this._storeContext = storeContext;
            this._dallasArtContext = dallasArtContext;
            this._workContext = workContext;
            this._customer = workContext.CurrentCustomer;
        //    this._requestService = requestService;
            this._logger = logger;
        }

        #endregion init


        public void HandleEvent(EntityTokensAddedEvent<Customer, Token> eventMessage)
        {
            ISelfIdentificationViewModel model = _dallasArtContext.ContactRequest.SelfIdentificationViewModel;

        //    const string gopp = "The Gift of Prayer Program";
            string contactPage = Utilities.StripppedHttp("Contact");
            string faqPage = Utilities.StripppedHttp("Help");
        //    EnrollmentLocation location = _dallasArtContext.Location;


            // common body tokens
            eventMessage.Tokens.Add(new Token("ContactLocationURL", contactPage));
            eventMessage.Tokens.Add(new Token("HelpLocationURL", faqPage));


            // additional tokens for contact
            if (_dallasArtContext.IsContact)
            {
               // SelectListItem selectedCategory = _dallasArtContext.SelectedCategoryItem;

               // eventMessage.Tokens.Add(new Token("Registered", (model.Member) ? "True" : "False"));
                eventMessage.Tokens.Add(new Token("Contact.Question", _dallasArtContext.ContactRequest.Question));
              //  eventMessage.Tokens.Add(new Token("Contact.TopicName", selectedCategory.Text));
              //  eventMessage.Tokens.Add(new Token("Contact.TopicNumber", selectedCategory.Value));
                eventMessage.Tokens.Add(new Token("Contact.FullName", model.FirstName + " " + model.LastName ));
                eventMessage.Tokens.Add(new Token("Contact.Email", model.Email));
                eventMessage.Tokens.Add(new Token("Contact.PhoneNumber", model.Phone));

                return;
            }

            // additional tokens for registration
            if (_dallasArtContext.IsRegistration)
            {

                return;
            }

            //  if (_dallasArtContext.IsEnrollment)
            //  {

  
            //string date = Utilities.DateTimeToShortDate(location.OriginalEnrollmentDate);
            //string expires = Utilities.DateTimeToShortDate(location.RenewalDate);

            //string enrollmentType = "Enrollment";

            ////           string enrollmentEzLink = 
            ////                  $"{Utilities.StripppedHttp("EnrollCenter/EnrollCustomer")}/{location.EnrollmentLocationId}";

            //string registationEzLink =
            //    $"{Utilities.StripppedHttp("IndividualRegistation/RegisterCustomer")}/{location.EnrollmentLocationId}";

            //string prayercoordinatorregistationEzLink =
            //    $"{Utilities.StripppedHttp("IndividualRegistation/RegisterPrayerCoordinator")}/{location.EnrollmentLocationId}";

            //// Enrollment Notification
            //eventMessage.Tokens.Add(new Token("Evantell.EnrollmentType", enrollmentType));
            //eventMessage.Tokens.Add(new Token("Evantell.CustomerName", _customer.GetFullName()));
            //eventMessage.Tokens.Add(new Token("Evantell.EnrollmentEmail", _customer.Email));

            //eventMessage.Tokens.Add(new Token("Evantell.EnrollmentCenterName", location.CenterName.Replace("_", " ")));
            //eventMessage.Tokens.Add(new Token("Evantell.EnrollmentCenterId", location.CenterGroupId));
            //eventMessage.Tokens.Add(new Token("Evantell.EnrollmentDate", date));
            //eventMessage.Tokens.Add(new Token("Evantell.EnrollmentDateExpires", expires));

            //// eventMessage.Tokens.Add(new Token("Evantell.PrimaryCenterName", location.PrimaryCenterName.Replace("_", " ")));
            //// eventMessage.Tokens.Add(new Token("Evantell.PrimaryCenterID", location.PrimaryCenterGroupId));

            //eventMessage.Tokens.Add(new Token("Evantell.Registation.EzLink", registationEzLink));
            //eventMessage.Tokens.Add(new Token("Evantell.PrayerCoordinatorRegistation.EzLink", prayercoordinatorregistationEzLink));
            //  }
        }
    }
}