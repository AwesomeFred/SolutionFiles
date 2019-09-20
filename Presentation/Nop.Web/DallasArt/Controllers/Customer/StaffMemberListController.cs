using Nop.Core;
using System.Linq;
using Nop.Core.Domain.Customers;
using System.Web.Mvc;
using Nop.Web.Framework.Controllers;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.Models;
using System.Collections.Generic;
using Nop.Web.DallasArt.Classes;
using System.Net;
using System;

namespace Nop.Web.DallasArt.Controllers.Customer
{
    public class StaffMemberListController : BasePublicController
    {
        

        private IWorkContext _workContext;
        private IMembershipService _membershipService;
     //   private 

        // GET: Member
        public StaffMemberListController
            (
               IMembershipService membershipService, 
               IWorkContext workContext
             
            )
        {
           
            _workContext = workContext;
            _membershipService = membershipService;
        }


        private bool Authorized ()
        {
           return true;
         
           Core.Domain.Customers.Customer curCustomer = _workContext.CurrentCustomer;

            if (curCustomer.IsInCustomerRole("Administrators")) return true;
            if (curCustomer.IsInCustomerRole("PaaManagementAdministrators")) return true;

            return false;
        }


       
       // [NopHttpsRequirement(SslRequirement.Yes)]
        public ActionResult Index(int? id)
        {
            // hide from public
            if (!Authorized()) return InvokeHttp404(); 


            IEnumerable<PaaMember> members = _membershipService.MembersList();

            switch(id)
            {
                case 1:
                    {
                        ViewBag.SortSelector = 1;
                        return View(Utilities.CorrectViewPath("MemberList/Index"), members.OrderBy(x => x.Email));
                    }

                case 2:
                    {
                        ViewBag.SortSelector = 2;
                        return View(Utilities.CorrectViewPath("MemberList/Index"), members.OrderByDescending(x => x.Status.Key)  );
                    }

                case 3:
                    {
                        ViewBag.SortSelector = 2;
                        return View(Utilities.CorrectViewPath("MemberList/Index"), members.OrderByDescending(x => x.StreetAddress));
                    }

                default:
                    {
                        ViewBag.SortSelector = 0;
                        return View(  Utilities.CorrectViewPath("MemberList/Index")  ,  members.OrderBy(x=>x.LastName) );
                    }
            }
        }

        // GET: Member/Details/5
        public ActionResult Details(int? id)
        {
            // hide from public
            if (!Authorized()) return InvokeHttp404();

            PaaMember member =  _membershipService.GetMemberById((int) id); 
              if(member == null) return    InvokeHttp404();  // new HttpNotFoundResult();

            return View(Utilities.CorrectViewPath("MemberList/Details"), member) ;
        }


        // GET: Member/Create
        public ActionResult Create()
        {

            // hide from public
            if (!Authorized()) return InvokeHttp404();


            PaaMember member = new PaaMember( );
            member.Renewal =  DateTime.Now.AddDays(366);


            return View(Utilities.CorrectViewPath("MemberList/Create"), member);
        }

        // POST: Member/Create
        [HttpPost]
        public ActionResult Create(  /*FormCollection collection  ,*/ PaaMember member )
        {
            // hide from public
            if (!Authorized()) return InvokeHttp404();

            // TODO: Add insert logic here


            // if( )
            return View(Utilities.CorrectViewPath("MemberList/Create"), member);

        }




        // GET: Member/Edit/5
        public ActionResult Edit(int? id)
        {
            // hide from public
            if (!Authorized()) return InvokeHttp404();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PaaMember member = _membershipService.GetMemberById( (int) id   );

            if (member == null) return InvokeHttp404();  // new HttpNotFoundResult();


            if (member.Membership != null) ViewBag.MembershipSelector = member.Membership.Id;

            ViewBag.MemberStateId = member.StateProvinceId;

            return View(Utilities.CorrectViewPath("MemberList/Edit"), member);
        }

        // POST: Member/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PaaMember model  , FormCollection collection)
        {
            // hide from public
            if (!Authorized()) return InvokeHttp404();                   
          

            try
            {
                // TODO: Add update logic here


               if( ModelState.IsValid )
                    return RedirectToAction("Index");



                // something failed     
                return View(Utilities.CorrectViewPath("MemberList/Edit"), model);
            }
            catch
            {
                return View(Utilities.CorrectViewPath("MemberList/Edit"));
            }
        }


    
    }
}
