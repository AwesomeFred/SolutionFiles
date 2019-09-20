using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core;
using Nop.Services.Customers;
using Nop.Web.DallasArt.ViewModels.Customer;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.Services;



namespace Nop.Web.DallasArt.Controllers.Customer
{
    public class CustomerListController : Controller
    {

        #region fields
        private readonly IStoreContext _storeContext;
        private readonly ICustomerService _customerService;

        #endregion

        #region Ctor
        public CustomerListController(
            IStoreContext storeContext,
            ICustomerService customerService
            
            )
        {

            this._storeContext = storeContext;
            this._customerService = customerService;

        }
        #endregion


        private string ContactViewPath(string path)
        {
            return $"~/Themes/{_storeContext.CurrentStore.Name}/Views/MemberList/{path}.cshtml";
        }



        // GET: CustomerList
        public ActionResult Expired()
        {
              DateTime expDate = DateTime.Today.AddDays(-45) ;
            
              int storeId = _storeContext.CurrentStore.Id;

             


               


            PaaMemberService membersList =  new PaaMemberService();

             IList<MemberListViewModel> members =
                 membersList.AllMembers(_customerService, 0);


            IList<MemberListViewModel> expMembers = 
                
                (from member in members let renewal = member.Renewal where renewal < expDate select member).ToList();

            

                return View(ContactViewPath("ExecMembersListing"), expMembers.OrderByDescending(x=>x.Renewal).ToList() );
        }



        public ActionResult NewThisYear()
        {
            DateTime firstOfYear2019;

            DateTime.TryParse("01/01/2019" , out firstOfYear2019);

            PaaMemberService membersList = new PaaMemberService();

            IList<MemberListViewModel> members =
                membersList.AllMembers(_customerService, 0);


            IList<MemberListViewModel> newMembers =

                (from member in members let renewal = member.Renewal where renewal > firstOfYear2019 select member).ToList();
            

            return View(ContactViewPath("MembersListing2018"), newMembers.OrderByDescending(x => x.Renewal).ToList());
       
            
        }



        public ActionResult ExpiredBy45Days()
        {
   
           DateTime fortyFiveDayAgo = DateTime.Today.AddDays(-45);


            DateTime FifteenDayAgo = DateTime.Today.AddDays(-15);




            PaaMemberService membersList = new PaaMemberService();

            IList<MemberListViewModel> members =
                membersList.AllMembers(_customerService, 0);
            

            IList<MemberListViewModel> newMembers =

                (from member in members let renewal = member.Renewal where renewal < fortyFiveDayAgo   select member).ToList();


            return View(ContactViewPath("MembersListing2018"), newMembers.OrderByDescending(x => x.Renewal).ToList());


        }


    }
}