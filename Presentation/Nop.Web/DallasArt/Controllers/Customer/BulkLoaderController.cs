using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Web.Extensions;
using Nop.Web.Framework.Controllers;
using Nop.Web.Models.Checkout;
using Nop.Web.Models.Customer;
using DallasArt.Data;
using Nop.Admin.Models.Customers;
using Nop.Core.Domain.Common;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Stores;
using Nop.Web.DallasArt.Classes;


namespace Nop.Web.DallasArt.Controllers.Customer
{
    public class BulkLoaderController : BaseController
    {

        //private  readonly    = EngineContext.Current.Resolve<CustomerSettings>();
        //private  readonly IStoreContext _storeContext          = EngineContext.Current.Resolve<IStoreContext>();
        //private readonly ICustomerRegistrationService          _customerRegistrationService = EngineContext.Current.Resolve<ICustomerRegistrationService>();
        //private readonly IWorkContext _workContext = EngineContext.Current.Resolve<IWorkContext>();
        //private readonly ICustomerService _customerService = EngineContext.Current.Resolve<ICustomerService>();

        private readonly CustomerSettings _customerSettings;
        private readonly IStoreContext _storeContext;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;


        private readonly ILocalizationService _localizationService;
        private readonly IStoreService _storeService;
        private readonly IDateTimeHelper _dateTimeHelper;











        public BulkLoaderController
            (
              CustomerSettings customerSettings,
              IStoreContext storeContext,
              ICustomerRegistrationService customerRegistrationService,
              IWorkContext workContext,
              ICustomerService customerService,
              IStoreService storeService,
              ILocalizationService localizationService,
              IDateTimeHelper dateTimeHelper
            )
        {
            this._customerSettings = customerSettings;
            this._storeContext = storeContext;
            this._customerRegistrationService = customerRegistrationService;
            this._workContext = workContext;
            this._customerService = customerService;
            this._storeService = storeService;
            this._localizationService = localizationService;
            this._dateTimeHelper = dateTimeHelper;




        }


        private const string path = @"D:\websites\PlanoArt\Members\PAA_Member_List_1_11_18.xlsx";
        private const string sheet = "Sheet1";

        public static int GoodCount, BadCount;


        private string ContactViewPath(string path)
        {
            return $"~/Themes/{_storeContext.CurrentStore.Name}/{path}.cshtml";
        }


        private DataTable GetSpreadsheetData(string filePath, string sheetName)
        {
            DataTable table = SpreadSheet.ImportToDataTable(filePath, sheetName);

            foreach (DataRow dr in table.Rows)
            {
                var r = dr.ItemArray;
            }
            return table;
        }


        //if (GoodCount == 2) return;
        //var newAddress = new Address
        //{
        //    FirstName = (string)row[(int)Header.First],
        //    LastName = (string)row[(int)Header.Last],
        //    Email = email,
        //    CountryId = 1,
        //    Address1 = row[(int)Header.Address].ToString().Replace("?", string.Empty),
        //    City = row[(int)Header.City].ToString().Replace("?", string.Empty),
        //    ZipPostalCode = row[(int)Header.Zip].ToString().Replace("?", string.Empty),
        //    PhoneNumber = row[(int)Header.Phone].ToString().Replace("?", string.Empty),
        //    StateProvinceId = 54, //23
        //    CreatedOnUtc = DateTime.UtcNow

        //};



        private void AddCustomerBillingAddress(DataRow row, string email)
        {
            CheckoutBillingAddressModel model = new CheckoutBillingAddressModel();

            Address address = model.NewAddress.ToEntity();

            // address.CustomAttributes = customAttributes;
            address.CreatedOnUtc = DateTime.UtcNow;
            address.CountryId = 1;
            address.StateProvinceId = 54;
            address.Email = email;

            address.FirstName = row[(int)SpreadSheet.Header.First].ToString().Replace("?", string.Empty);
            address.LastName = row[(int)SpreadSheet.Header.Last].ToString().Replace("?", string.Empty);
            address.Address1 = row[(int)SpreadSheet.Header.Address].ToString().Replace("?", string.Empty);
            address.City = row[(int)SpreadSheet.Header.City].ToString().Replace("?", string.Empty);
            address.ZipPostalCode = row[(int)SpreadSheet.Header.Zip].ToString().Replace("?", string.Empty);
            address.PhoneNumber = row[(int)SpreadSheet.Header.Phone].ToString().Replace("?", string.Empty);


            //some validation
            if (address.CountryId == 0)
                address.CountryId = null;
            if (address.StateProvinceId == 0)
                address.StateProvinceId = null;

            _workContext.CurrentCustomer.Addresses.Add(address);
            _workContext.CurrentCustomer.BillingAddress = address;
            _customerService.UpdateCustomer(_workContext.CurrentCustomer);


        }

        private CustomerRole AddMembershipRole(ref IList<CustomerRole> roles, string level)
        {
            CustomerRole role = null;

            if (level.Trim().Length > 0)
            {

                // get rid of cost
                string[] asc = level.Trim().ToLower().Split('(');

                switch (asc[0].Trim())
                {
                    case "student":
                        {
                            role = roles.FirstOrDefault(s => s.SystemName == "StudentMembership");

                            break;
                        }

                    case "adult":
                        {
                            role = roles.FirstOrDefault(s => s.SystemName == "AdultMembership");
                            break;
                        }
                    case "senior":
                        {
                            role = roles.FirstOrDefault(s => s.SystemName == "SeniorMembership");

                            break;
                        }
                    case "family":
                        {
                            role = roles.FirstOrDefault(s => s.SystemName == "FamilyMembership1");

                            break;
                        }
                    case "senior family":
                        {
                            role = roles.FirstOrDefault(s => s.SystemName == "SeniorFamilyMembership1");

                            break;
                        }
                    case "honorary":
                        {
                            role = roles.FirstOrDefault(s => s.SystemName == "HonoraryMembership");

                            break;
                        }

                    default:
                        {
                            break;
                        }

                }

            }
            return role;
        }





        private void InsertNewCustomers(DataTable data)
        {

            IList<CustomerRole> roles = _customerService.GetAllCustomerRoles();

            foreach (DataRow row in data.Rows)
            {
                var email = row[(int) SpreadSheet.Header.EMail].ToString();
                // empty row ??
                if (email.Length < 3)
                {
                    BadCount++;
                    continue;
                }

                // Existing customer ??

                var current = _customerService.GetCustomerByEmail(email);

                bool excludeProperties = current != null;

                _workContext.CurrentCustomer =  excludeProperties ? current : _customerService.InsertGuestCustomer();

                Core.Domain.Customers.Customer customer = _workContext.CurrentCustomer;
                CustomerModel model =  new CustomerModel();

                var v = new Import( _storeService , _localizationService , _workContext , _dateTimeHelper );

                v.PrepareCustomerModel( model,  customer, excludeProperties) ;
                
            }
        }



        private void AddCustomersFromData(DataTable data)
        {
            IList<CustomerRole> roles = _customerService.GetAllCustomerRoles();

            foreach (DataRow row in data.Rows)
            {
                var email = row[(int)SpreadSheet.Header.EMail].ToString();
                // empty row ??
                if (email.Length < 3)
                {
                    BadCount++;
                    continue;
                }

                var model = new RegisterModel
                {
                    FirstName = row[(int)SpreadSheet.Header.First].ToString(),
                    LastName = row[(int)SpreadSheet.Header.Last].ToString(),
                    Email = email,
                    ConfirmEmail = email,
                    Username = email,
                    CheckUsernameAvailabilityEnabled = true,
                    CountryId = 1, // usa
                    Password = "Passw0rd01",
                    ConfirmPassword = "Passw0rd01",

                    HoneypotEnabled = false,
                    DisplayCaptcha = false,
                    DisplayVatNumber = false,

                    StreetAddress = row[(int)SpreadSheet.Header.Address].ToString().Replace("?", string.Empty),
                    City = row[(int)SpreadSheet.Header.City].ToString().Replace("?", string.Empty),
                    ZipPostalCode = row[(int)SpreadSheet.Header.Zip].ToString().Replace("?", string.Empty),
                    Phone = row[(int)SpreadSheet.Header.Phone].ToString().Replace("?", string.Empty),
                    StateProvinceId = 54,   // texas  //23
                   
                    
                    
                    //CreatedOnUtc = DateTime.UtcNow
                };


                // var customer =  new Core.Domain.Customers.Customer();
                _workContext.CurrentCustomer = _customerService.InsertGuestCustomer();

                Core.Domain.Customers.Customer customer = _workContext.CurrentCustomer;

                bool isApproved = _customerSettings.UserRegistrationType == UserRegistrationType.Standard;

                var registrationRequest = new CustomerRegistrationRequest(customer,
                   model.Email,
                   _customerSettings.UsernamesEnabled ? model.Username : model.Email,
                   model.Password,
                   _customerSettings.DefaultPasswordFormat,
                   _storeContext.CurrentStore.Id,
                   isApproved);

                // register customer 
                var registrationResult = _customerRegistrationService.RegisterCustomer(registrationRequest);

                if (registrationResult.Success)
                {
                    // update customer role
                    string level = row[(int)SpreadSheet.Header.Level].ToString().Replace("?", string.Empty);

                    if (level.Trim().Length > 0)
                    {
                        CustomerRole role = AddMembershipRole(ref roles, level);

                        //  add customer membership level; 
                        if (role != null) registrationRequest.Customer.CustomerRoles.Add(role);
                    }


                    // add billing address

                   // AddCustomerBillingAddress(row, email);

                    GoodCount++;
                }
                else
                {
                    BadCount++;
                }

                //  if (GoodCount > 2) return;



            }
        }

























        // GET: BulkLoader
        public ActionResult GetMembers()
        {

            DataTable data = GetSpreadsheetData(path, sheet);

            // remove header
            data.Rows.RemoveAt(0);

            InsertNewCustomers(data) ;

//            AddCustomersFromData(data);

            return View(ContactViewPath("BulkLoader/Index")); ;
        }
    }
}

