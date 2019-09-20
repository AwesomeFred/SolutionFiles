using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Web.Controllers;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.ViewModels;
using Nop.Web.Framework;
using Nop.Web.Framework.Security;
using Nop.Web.Framework.Security.Captcha;
using Nop.Web.Models.Checkout;
using Nop.Web.Themes.PAA.ViewModels;


namespace Nop.Web.Themes.PAA.Controllers
{
    public class RegisterCustomerController : BasePublicController
    {
        private readonly CustomerSettings _customerSettings;
        private readonly IWorkContext _workContext;
        private readonly IDallasArtContext _dallasArtContext;
        private readonly ICustomerService _customerService;
        private readonly IStoreContext _storeContext;


        private readonly ICustomerRegistrationService _customerRegistrationService;

        public RegisterCustomerController(
                                     CustomerSettings customerSettings,
                                     IWorkContext workContext,
                                     IDallasArtContext dallasArtContext,
                                     ICustomerService customerService,
                                     ICustomerRegistrationService customerRegistrationService,
                                     IStoreContext storeContext
                                  )
        {
            this._customerSettings = customerSettings;
            this._workContext = workContext;
            this._dallasArtContext = dallasArtContext;
            this._customerService = customerService;
            this._customerRegistrationService = customerRegistrationService;
            this._storeContext = storeContext;
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
            return $"~/Themes/{_storeContext.CurrentStore.Name}/Views/Register/{path}.cshtml";
        }
        #endregion

        /*
                Product Id     Name                  
               --------------------------------
                1    Student Membership  
                2    Adult Membership
                3    Senior Membership
                4    Family Membership
                5    Senior Family Membership
                6    Donation
       */

        public ActionResult Instructions()
        {
            ViewBag.Headline = "ENTRY INSTRUCTIONS";

            return View(ContactViewPath("Instructions"));
        }

        public ActionResult Register( string id)
        {
            ViewBag.Headline = "SUBMIT ENTRY(S)";

            // check whether registration is allowed
            if (_customerSettings.UserRegistrationType == UserRegistrationType.Disabled)
                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.Disabled });

            _dallasArtContext.IsRegistration = true;
            _dallasArtContext.NeedsPassword = true;

            RegistrationViewModel model = new RegistrationViewModel
            {
                SelfIdentificationViewModel =
                {
                    RequestPassword = false,
                    RequestPhoneNumber = true,
                    RequestEmail = true
                }
            };

            Customer customer = _workContext.CurrentCustomer;

            //model.SelfIdentificationViewModel.FirstName = "Fred";
            //model.SelfIdentificationViewModel.LastName = "Lauriello";
            //model.SelfIdentificationViewModel.Phone = "123-456-7890";
            //model.SelfIdentificationViewModel.ConfirmPhone = "123-456-7890";

            //model.SelfIdentificationViewModel.Email = "fred@dallasart.com";
            //model.SelfIdentificationViewModel.ConfirmEmail = "fred@dallasart.com";
            model.CurrentState = "Begin";
            return View(ContactViewPath("Register"), model);
        }

        [NonAction]
        private void PostExtraValidation(RegistrationViewModel model)
        {
            // check for existing email address
            //      if (model.SelfIdentificationViewModel.Email != null &&
            //       _customerService.GetCustomerByEmail(model.SelfIdentificationViewModel.Email.Trim()) != null)
            //        ModelState.AddModelError("Email", "Email Address Is Already In Use!");

            if (model.TotalCount < 1)
            {
                ModelState.AddModelError("TotalCount", "Please choose number of images to submit!");
            }
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
            if (_customerSettings.UserRegistrationType == UserRegistrationType.Disabled)
                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.Disabled });


            //// check for existing email address  this is a members only show;
            //if (model.SelfIdentificationViewModel.Email == null ||
            // _customerService.GetCustomerByEmail(model.SelfIdentificationViewModel.Email.Trim()) == null)
            //{
            //    model.CurrentState = "NonMember";
            //    model.DisplaySuccess = false;
            //    return View(ContactViewPath("Register"), model);
            //}



            // setup model for pure registration
            _dallasArtContext.IsRegistration = true;
            model.SelfIdentificationViewModel = identity;

            if (model.CurrentState == "Begin")
            {
                model.CurrentState = "Loop";
                //  PostExtraValidation(model);


                if (!ModelState.IsValid)
                {

                    // something failed validation
                    return PartialView(ContactViewPath("_RegisterRequest"), model);
                }

                else
                {
                    // add member
                    model.ImageUploadViewModel.Member =    identity.RadioButtionList.SelectedRole == 2  ;

                    model.ImageUploadViewModel.EmailAddress = identity.Email;
                    model.ImageUploadViewModel.CustomerName = identity.FirstName + " " + identity.LastName;
                    model.ImageUploadViewModel.Telephone = identity.Phone;
                    model.ImageUploadViewModel.RemainingImages = 2;
                    model.ImageUploadViewModel.ModelState = "Ready";

                    Customer customer = _customerService.GetCustomerByEmail(identity.Email);
                    IList<CustomerRole> x = _customerService.GetAllCustomerRoles();

                    // if (customer != null) model.CurrentState = "Member";

                    model.DisplaySuccess = true;

                    return PartialView(ContactViewPath("_RegisterRequest"), model);
                }

               // model.DisplaySuccess = false;
                // model.CurrentState = "Begin";
              //  return View(ContactViewPath("Register"), model);
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
                    return View("Register", model);
                }
            }

            // something failed validation
            return View("Register", model);
        }

    }
}



//if (customer != null && customer.IsGuest() && customer.HasShoppingCartItems)
//{
//    // check shopping cart for current valid registration order
//    var items = customer.ShoppingCartItems;

//    if (items.Count == 1) // one item in cart;
//    {
//        switch (items.First().ProductId)
//        {

//            case (int)CustomerRoleType.StudentMembership:
//                {
//                    break;
//                }
//            case (int)CustomerRoleType.AdultMembership:
//                {
//                    break;
//                }
//            case (int)CustomerRoleType.SeniorMembership:
//                {
//                    break;
//          }
//           }
//       return View("Register", model);
//     }
//  }