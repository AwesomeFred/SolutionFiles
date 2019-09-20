using System.Web.Mvc;
using Nop.Core;
using Nop.Services.Events;
using Nop.Web.DallasArt.Events.Register;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.ViewModels;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Security;
using Nop.Web.Framework.Security.Captcha;


namespace Nop.Web.DallasArt.Controllers.Contact
{
    public class ContactController : BaseController 
    {
        #region fields

        private readonly IDallasArtContext _dallasArtContext;
        private readonly IStoreContext _storeContext;
        private readonly IContactRequestService _contactRequestServiceService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public ContactController(
            IDallasArtContext dallasArtContext,
            IStoreContext storeContext,
            IContactRequestService contactRequestServiceService,
            IEventPublisher eventPublisher,
            IWorkContext workContext
            )
        {
            this._storeContext = storeContext;
            this._dallasArtContext = dallasArtContext;
            this._storeContext = storeContext;
            this._contactRequestServiceService = contactRequestServiceService;
            this._eventPublisher = eventPublisher;
            this._workContext = workContext;
        }

        #endregion

        #region Utilities

        private string ContactViewPath(string path)
        {
            return $"~/Themes/{_storeContext.CurrentStore.Name}/Views/Contact/{path}.cshtml" ;
        }

        private void ExtractValuesFromResponse(ContactRequestViewModel model, SelfIdentificationViewModel identity)
        {
            // restore state for tokenizer
            _dallasArtContext.IsContact = true;
            _dallasArtContext.NeedsPassword = false;

            // get the question
            _dallasArtContext.ContactRequest.Question = model.Question;

            // get the selected category
               _dallasArtContext.SelectedCategoryItem = model.SelectedOption;
              //  _dallasArtContext.SelectedCategoryItem = model.CategorySelectListItems
              //  .Single(x => x.Value == model.SelectedCategoryId.ToString());

            // get the  Identification
            _dallasArtContext.ContactRequest.SelfIdentificationViewModel = identity;
        }

        #endregion


        [HttpGet]
        public ActionResult CustomerContact(string id)
        {
            _dallasArtContext.IsContact = true;
            _dallasArtContext.NeedsPassword = false;

            var model = new ContactRequestViewModel { SelfIdentificationViewModel = { RequestPassword = false } };

            //model.SelfIdentificationViewModel.FirstName = "Fred";
            //model.SelfIdentificationViewModel.LastName = "Lauriello";
            //model.SelfIdentificationViewModel.Phone = "123-456-7890";
            //model.SelfIdentificationViewModel.ConfirmPhone = "123-456-7890";

            //model.SelfIdentificationViewModel.Email = "fred@dallasart.com";
            //model.SelfIdentificationViewModel.ConfirmEmail = "fred@dallasart.com";
            //model.Question = "Testing Contact - " + DateTime.Now.ToShortTimeString();
            
            // for testing
            //model.DisplaySuccess = true;

            return string.IsNullOrWhiteSpace(id)
                ? View(ContactViewPath("ContactRequest"), model)
                : View(ContactViewPath("ContectResponse"), model);  // for testing
        }


        [HttpPost]
        [CaptchaValidator]
        [PublicAntiForgery]
        [ValidateInput(false)]
        public ActionResult CustomerContact(
                                             ContactRequestViewModel model,
                                             SelfIdentificationViewModel identity,
                                             string returnUrl,
                                             bool captchaValid
                                            )
        {
            model.SelfIdentificationViewModel = identity;

            // prepare response
            if (model.CurrentCustomer == null)
                model.CurrentCustomer = _workContext.CurrentCustomer;
            
            // fake selection
            model.SelectedCategoryId = 0;
            model.SelectedOption = new SelectListItem {Value = "0" , Text = "Other" , Selected = true};

            ExtractValuesFromResponse (model, identity);

            // security check for non-member
            // if (!model.Member)
            //   DisplayCaptchaViewModel.CaptchaValidation(captchaValid, model.DisplayCaptcha, ModelState);

            if (ModelState.IsValid)
            {
                // save the record
             //   _contactRequestServiceService.CreateContactRecord(model);

                _eventPublisher.Publish(new ContactRequestCreatedEvent( model ));

                // return success;
                model.DisplaySuccess = true;

               // if (model.Member) return RedirectToRoute("greetings", new {id="contact"}  );
              
                return PartialView(ContactViewPath("_ContactResponse"), model);
            }

            // if we get here something failed!
            // return failure
            model.DisplaySuccess = false;

            return View(ContactViewPath("ContactRequest"), model);
        }

    }
}