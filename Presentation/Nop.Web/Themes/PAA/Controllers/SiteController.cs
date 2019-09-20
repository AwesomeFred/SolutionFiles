using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Web.Controllers;
using Nop.Services.Customers;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.Services;
using Nop.Web.DallasArt.ViewModels;
using Nop.Web.DallasArt.ViewModels.Customer;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.Models;
using static Nop.Web.DallasArt.Services.MembershipService;

namespace Nop.Web.Themes.PAA.Controllers
{
    public class SiteController : BasePublicController
    {
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly IStoreContext _storeContext;
        private readonly IMembershipService _membershipService; 

        public SiteController(
            IWorkContext workContext,
            IStoreContext storeContext,
            ICustomerService customerService,
            IMembershipService membershipService

        )
        {
            _workContext = workContext;
            _storeContext = storeContext;
            _customerService = customerService;
            _membershipService = membershipService;
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            ViewBag.Headline = "ABOUT";



            return View("About");
        }

        public ActionResult Board()
        {
            ViewBag.Message = "Your application description page.";

            ViewBag.Headline = "BOARD AND OFFICERS";


            return View("Board");
        }

        public ActionResult Bylaws()
        {
            ViewBag.Message = "Your application description page.";

            ViewBag.Headline = "CONSTITUTION AND BYLAWS";

            return View("ByLaws");
        }

        public ActionResult Benefits()
        {
            ViewBag.Message = "Your application description page.";

            ViewBag.Headline = "MEMBER BENEFITS";

            return View("Benefits");
        }

        public ActionResult Join()
        {
            ViewBag.Message = "Your application description page.";

            ViewBag.Headline = "JOIN or RENEW";

            return View();
        }

        [HttpPost]
        public ActionResult Join(FormCollection form)
        {
            ViewBag.Headline = "JOIN or RENEW";

            if (form?.AllKeys[0] == null)
            {
                return View();
            }

            switch (form.AllKeys[0].ToLower())
            {

                case "senior":
                case "adult":
                case "family":
                case "senior-family":
                case "student":
                {
                     return RedirectToAction("Register", "RegisterCustomer", new { @id = form.AllKeys[0].ToLower()  }); ;
                }

                case "donate":
                {
                  return View("Donate" );
                  
                }

                default:
                {
                    return View();
                }

            }
        }





//public ActionResult Membership()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    ViewBag.Headline = "JOIN PAA";


        //    return Redirect("join");
        //}





        public ActionResult MembersListing()
        {
            ViewBag.Message = "Your application description page.";

         
            IList<PaaMember> members = new List<PaaMember>();   


            foreach( PaaMember member in  _membershipService.MembersList(0) )
            {
                switch(member.Status.Key)
                {
                    case (int)StatusCode.current:
                    case (int)StatusCode.current_expiring_less_than_fifteen_days:
                    case (int)StatusCode.expired_grace_less_than_fortyfive_days:
                        {
                            members.Add(member);
                            break;
                        }

                    default: continue;
                }
            }



       //     PaaMemberService membersList = new PaaMemberService();


          //  IList<MemberListViewModel> memberList = membersList.AllMembers(_customerService, 0);


            ViewBag.Headline = "MEMBERS LISTING";

//            return View("MembersListing", memberList.OrderBy(x => x.LastName).ToList());

            return View("MembersListing" , members.OrderBy(x=>x.LastName).ToList() );
        }

        public ActionResult Donate()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.Headline = "DONATE";

            return View("Donate");
        }

        public ActionResult Happenings()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.Headline = "HAPPENINGS";

            return View("Happenings");
        }

        public ActionResult Partners()
        {
            ViewBag.Message = "Your application description page.";

          
            ViewBag.Headline = "SPONSORS & PARTNERS";


            return View("Partners");
        }

        public ActionResult Sponsors()
        {
            ViewBag.Message = "Your application description page.";

            ViewBag.Headline = " PARTNERS & SPONSORS";

            return View("Sponsors");
        }

        public ActionResult NewsLetters()
        {
            ViewBag.Message = "Your application description page.";

            ViewBag.Headline = "PAA NEWSLETTERS";

            return View("newsletters");
        }

        public ActionResult Events()
        {
            ViewBag.Message = "Your application description page.";

            ViewBag.Headline = "Upcoming Events";

            return View("events");
        }

        // [ChildActionOnly]
        public PartialViewResult CheckForMembershipPurchase()
        {
          
            var model = new SelfIdentificationViewModel();

            model.RequestPhoneNumber = false;
            model.RequestPassword = false;
      //      model.FirstName = "Fred";

      //      model.LastName = "Lauriello";
      //      model.Email = "ecomda@gmail.com";
      //      model.ConfirmEmail = "ecomda@gmail.com";


            //if (
            //    customer.HasShoppingCartItems
            //    && customer.ShoppingCartItems.Count == 1
            //    && customer.ShoppingCartItems.First().ProductId <= 5)
            //{
            //    isMembershipOrder = true;
            //}

            return PartialView("_CheckForMembershipPurchase" , model);
        }











        [HttpPost]
        // public ActionResult MembershipValidation(SelfIdentificationViewModel model , string membership  )
        public ActionResult CheckForMembershipPurchase( FormCollection form)

        {
            Customer customer = _workContext.CurrentCustomer;
          //  Customer existing =  _customerService.GetCustomerByEmail(model.Email);



            var type = form["membershiptype"];



            customer.AdminComment =type;


            return  RedirectToAction("OnePageCheckOut" , "Checkout" , new { id = type.Replace(" " , string.Empty)     }   );


            //    if (membershiptype.ToLower() == "new membership"  && existing != null)
            //    {
            //        // wrong button
            //        ModelState.AddModelError("Email", "Email Address Is Already In Use!");
            //        return PartialView("_CheckForMembershipPurchase", model);

            //    }
            //    else if( 
            //             membershiptype.ToLower() == "renewal" 
            //             && existing != null
            //             && existing.Email == model.Email )
            //    {


            //            string comment = $@"{model.FirstName},{model.LastName}";
            //            customer.AdminComment = comment;
            //            customer.Email = model.Email;


            //            return Redirect ToAction(   "Index"  , "Checkout"     );



            //    }


            //        // not found
            //        ModelState.AddModelError("Email", "Member Email Address Not Found");
            //        return PartialView("_CheckForMembershipPurchase", model);


        }




    }
}