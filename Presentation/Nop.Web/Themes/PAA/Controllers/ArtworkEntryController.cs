using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Web.Controllers;
using Nop.Web.DallasArt.Interfaces;
 
using Nop.Web.Themes.PAA.ViewModels;
using Nop.Web.DallasArt.ViewModels;
using Nop.Web.Framework;
using Nop.Web.Framework.Security;
using Nop.Web.Framework.Security.Captcha;
using Nop.Web.Models.Checkout;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.Models;
using System.Linq;

namespace Nop.Web.Themes.PAA.Controllers
{
    public class ArtworkEntryController : BasePublicController
    {

        private readonly CustomerSettings _customerSettings;
        private readonly IWorkContext _workContext;
        private readonly IDallasArtContext _dallasArtContext;
        private readonly ICustomerService _customerService;
        private readonly IStoreContext _storeContext;
        private readonly ICustomerRegistrationService _customerRegistrationService;

        private readonly IMembershipService _membershipService;


        public ArtworkEntryController(
            CustomerSettings customerSettings,
            IWorkContext workContext,
            IDallasArtContext dallasArtContext,
            ICustomerService customerService,
            ICustomerRegistrationService customerRegistrationService,
            IStoreContext storeContext,
            IMembershipService membershipService
            )
        {
            this._customerSettings = customerSettings;
            this._workContext = workContext;
            this._dallasArtContext = dallasArtContext;
            this._customerService = customerService;
            this._customerRegistrationService = customerRegistrationService;
            this._storeContext = storeContext;
            this._membershipService = membershipService;
        }


        #region utilities


        private void EnrollmentPostValidation(RegistrationViewModel model)
        {

            if (model.SelfIdentificationViewModel.Email != null
                && _customerService
                    .GetCustomerByEmail(model.SelfIdentificationViewModel.Email.Trim()) != null
            )
            {
                ModelState.AddModelError("Email", "The Entered Email Address Is Already In Use!");
            }

        }




        private string ContactViewPath(string path)
        {
            return $"~/Themes/{_storeContext.CurrentStore.Name}/Views/ImageSubmission/{path}.cshtml";
        }
        #endregion



        // index
        public ActionResult Instructions()
        {
            ViewBag.Headline = "ENTRY INSTRUCTIONS";


          // return View(Utilities.CorrectViewPath("ImageSubmission/Instructions"));

            return View(ContactViewPath("Instructions"));
        }


        public ActionResult Register()
        {
            ViewBag.Headline = "SUBMIT ENTRY(S)";

            // check whether registration is allowed
            if (_customerSettings.UserRegistrationType == UserRegistrationType.Disabled)
                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.Disabled });

            _dallasArtContext.IsRegistration = true;
            _dallasArtContext.NeedsPassword = true;
            _dallasArtContext.NeedMembershipStatus = true;


            RegistrationViewModel model = new RegistrationViewModel
            {
                SelfIdentificationViewModel =
                {
                    RequestPassword = false,
                    RequestPhoneNumber = true,
                    RequestEmail = true,
                    AskForMembershipStatus = false
                }
            };

         //  Customer customer = _workContext.CurrentCustomer;

            
           

           // model.SelfIdentificationViewModel.RadioButtons.Add(new SelectListItem {Text = "Yes", Value = "1" }); 
          //   model.SelfIdentificationViewModel.RadioButtons.Add(new SelectListItem {Text = "No",  Value = "2" });


          // model.SelfIdentificationViewModel.FirstName = "Fred";
          // model.SelfIdentificationViewModel.LastName = "Lauriello";
          // model.SelfIdentificationViewModel.Phone = "123-456-7890";
          // model.SelfIdentificationViewModel.ConfirmPhone = "123-456-7890";
          //// 
          // model.SelfIdentificationViewModel.Email = "fred@dallasart.com";
          // model.SelfIdentificationViewModel.ConfirmEmail = "fred@dallasart.com";
            model.CurrentState = "Begin";

           // return View(Utilities.CorrectViewPath("ImageSubmission/Register") , model );

             return View(ContactViewPath("Register"), model);
        }


        [HttpPost]
        [CaptchaValidator]
        [PublicAntiForgery(true)]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [ValidateInput(false)]
        //available even when navigation is not allowed
        [PublicStoreAllowNavigation(true)]
        public ActionResult Register(
            RegistrationViewModel model,
            SelfIdentificationViewModel identity,
            bool captchaValid
            )
        {

            //check whether registration is allowed
        //    if (_customerSettings.UserRegistrationType == UserRegistrationType.Disabled)
        //         return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.Disabled });

             


            // setup model for pure registration
            _dallasArtContext.IsRegistration = true;
            model.SelfIdentificationViewModel = identity;


            if (model.CurrentState == "Begin")
            {
                model.CurrentState = "Loop";
                //  PostExtraValidation(model);
 


                if (!ModelState.IsValid)
                {
                    model.DisplaySuccess =false;
                    // something failed validation


                   // return View(Utilities.CorrectViewPath("ImageSubmission/_RegisterRequest"), model);
                    return PartialView(ContactViewPath("_RegisterRequest"), model);
                }

                else
                {
 
                    model.ImageUploadViewModel.EmailAddress = identity.Email;
                    model.ImageUploadViewModel.CustomerName = identity.FirstName + " " + identity.LastName;
                    model.ImageUploadViewModel.Telephone = identity.Phone;
                    model.ImageUploadViewModel.RemainingImages = 3;
                    model.ImageUploadViewModel.ModelState = "Ready";


                    model.ImageUploadViewModel.Member = model.ImageUploadViewModel.Member;
                    model.DisplaySuccess = true;


                    PaaMember currentMember = 
                        _membershipService.Paa_Current_MemberList().Where(x => x.Email == identity.Email).SingleOrDefault();

                    model.ImageUploadViewModel.Member = ( currentMember != null) ;


                   // model.ImageUploadViewModel.Member =  // member is 2
                   //     model.SelfIdentificationViewModel.RadioButtionList.SelectedRole == 2;

                   //return View(Utilities.CorrectViewPath("ImageSubmission/_RegisterRequest"), model);
                    return PartialView(ContactViewPath("_RegisterRequest"), model);
                }

               // model.DisplaySuccess = false;
                // model.CurrentState = "Begin";
             //   return View(ContactViewPath("Register"), model);
            }

            // security check
            // DisplayCaptchaViewModel.CaptchaValidation(captchaValid, model.DisplayCaptcha, ModelState);

            // check input values not handled by validation
            EnrollmentPostValidation(model);

            if (ModelState.IsValid)
            {
                bool isApproved = _customerSettings.UserRegistrationType == UserRegistrationType.Standard;

                model.CurrentCustomer = _workContext.CurrentCustomer;
                model.SelfIdentificationViewModel.Customer = model.CurrentCustomer;


                var registrationRequest = new CustomerRegistrationRequest(
                    model.CurrentCustomer,
                    model.SelfIdentificationViewModel.Email,
                    _customerSettings.UsernamesEnabled ? model.Username : model.SelfIdentificationViewModel.Email,
                    model.SelfIdentificationViewModel.Password,
                    _customerSettings.DefaultPasswordFormat,
                    _storeContext.CurrentStore.Id,
                    isApproved);

                var registrationResult = _customerRegistrationService.RegisterCustomer(registrationRequest);

                if (registrationResult.Success)
                {

                    //  var loginResult =
                    // _customerRegistrationService.ValidateCustomer( model.SelfIdentificationViewModel.Email, model.SelfIdentificationViewModel.Password);

                    var guestRole = _customerService.GetCustomerRoleBySystemName(SystemCustomerRoleNames.Guests);

                    _workContext.CurrentCustomer.CustomerRoles.Add(guestRole);

                    var control = new OnePageCheckoutModel { ShippingRequired = false, DisableBillingAddressCheckoutStep = false };

                    return View("/Themes/PAA/Views/Checkout/onepagecheckout.cshtml", control);
                }
                else
                {
                    // something failed registration
                  //  return View(Utilities.CorrectViewPath("ImageSubmission/Register"), model);
                    return View("Register", model);
                }
            }

            // something failed validation

           // return View(Utilities.CorrectViewPath("ImageSubmission/Register"), model);
            return View("Register", model);
        }







    }
}