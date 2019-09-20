using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.Classes.Enums;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.Services;
using Nop.Web.DallasArt.ViewModels.Customer;

namespace Nop.Web.DallasArt.Controllers.Customer
{

    //public enum CustomerAttribute
    //{
    //    SecondMember = 2,
    //    Url = 4,
    //    ArtWork = 5,
    //    Renewal = 6

    //}



    public class PaidMembershipController : Controller
    {
        #region fields

        private readonly IDallasArtContext _dallasArtContext;
        private readonly IStoreContext _storeContext;
        private readonly IContactRequestService _contactRequestServiceService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly ICustomerServices _customerServices;

        #endregion

        #region Ctor

        public PaidMembershipController(

            IDallasArtContext dallasArtContext,
            IStoreContext storeContext,
            IContactRequestService contactRequestServiceService,
            IEventPublisher eventPublisher,
            IWorkContext workContext,
            ICustomerService customerService,
            ICustomerServices customerServices

            )
        {
            this._storeContext = storeContext;
            this._dallasArtContext = dallasArtContext;
            this._storeContext = storeContext;
            this._contactRequestServiceService = contactRequestServiceService;
            this._eventPublisher = eventPublisher;
            this._workContext = workContext;
            this._customerService = customerService;
            this._customerServices = customerServices;
        }
        #endregion

        #region Utilities

        private string PaidMembershipViewPath(string path)
        {
            return $"~/Themes/{_storeContext.CurrentStore.Name}/Views/Customer/{path}.cshtml";
        }




        #endregion


        // GET: PaidMembership
        public ActionResult Index()
        {
            IList<MemberListViewModel> membersList = new List<MemberListViewModel>();

            int storeId = _storeContext.CurrentStore.Id;


            string rtnPath = PaidMembershipViewPath("Membership");

            try
            {
                int[] roleIds = Utilities.ValidMemberRoleIds(_customerService).ToArray();

                IPagedList<Core.Domain.Customers.Customer> members = _customerService.GetAllCustomers(customerRoleIds: roleIds, pageIndex: 0, pageSize: 500);


                var count = members.TotalCount;

                foreach (Core.Domain.Customers.Customer member in members)
                {



                    if (member.Id == 3689)

                    {

                       // var x = 5;


                    }

                    MemberListViewModel ml = new MemberListViewModel();


                    // add custom customer attributes
                    foreach (CustomerServices.AttributeValuePair customerAttr
                                  in Utilities.GetCustomerAttributes(member))
                    {

                        switch (customerAttr.ValueId)
                        {

                            case (int)CustomerCustomAttribute.SecondMember:
                                {
                                    ml.NonPayingMember = customerAttr.ValueText == "Yes";

                                    break;
                                }

                            case (int)CustomerCustomAttribute.Url:
                                {
                                    ml.Url = customerAttr.ValueText;

                                    break;
                                }

                            case (int)CustomerCustomAttribute.ArtWork:
                                {

                                    ml.ArtWork = customerAttr.ValueText;

                                    break;
                                }


                            case (int)CustomerCustomAttribute.Renewal:
                                {

                                    ml.Renewal = DateTime.Parse(customerAttr.ValueText);

                                    break;
                                }

                        }

                    }


                    ml.Email = member.Email;

                    ml.FirstName = member.GetAttribute<string>(SystemCustomerAttributeNames.FirstName, storeId);
                    ml.LastName = member.GetAttribute<string>(SystemCustomerAttributeNames.LastName, storeId);

                    ml.Company = member.GetAttribute<string>(SystemCustomerAttributeNames.Company, storeId);

                    ml.StreetAddress = member.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress, storeId);
                    ml.StreetAddress2 = member.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress2, storeId);

                    ml.City = member.GetAttribute<string>(SystemCustomerAttributeNames.City, storeId);
                    ml.ZipCode = member.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode, storeId);

                    ml.State  = member.GetAttribute<string>(SystemCustomerAttributeNames.StateProvinceId, storeId);

                    ml.Phone = member.GetAttribute<string>(SystemCustomerAttributeNames.Phone, storeId);


                    Debug.WriteLine(member.Email);
                }




            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }





            return View(rtnPath);
        }
    }
}