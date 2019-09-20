using System;
using System.Linq;
using Nop.Admin.Models.Customers;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Tax;
using Nop.Services.Common;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Stores;

namespace Nop.Web.DallasArt.Classes
{
    public   class Import
    {
        private readonly ILocalizationService _localizationService;
        private readonly IStoreService _storeService;
        private readonly IWorkContext _workContext;
        private readonly IDateTimeHelper _dateTimeHelper;



        public Import(
                      IStoreService storeService ,  
                      ILocalizationService localizationService,
                      IWorkContext workContext,
                      IDateTimeHelper dateTimeHelper 
            )
        {
            _storeService = storeService;
            _localizationService = localizationService;
            _workContext = workContext;
            _dateTimeHelper = dateTimeHelper;
        }



        public   void   PrepareCustomerModel(   CustomerModel model, Core.Domain.Customers.Customer customer,
            bool excludeProperties)
        {







            if (customer != null)
            {

                var allStores = _storeService.GetAllStores();
                model.Id = customer.Id;
                if (!excludeProperties)
                {
                    model.Email = customer.Email;
                    model.Username = customer.Username;
                    model.VendorId = customer.VendorId;
                    model.AdminComment = customer.AdminComment;
                    model.IsTaxExempt = customer.IsTaxExempt;
                    model.Active = customer.Active;


                    if (customer.RegisteredInStoreId == 0 || allStores.All(s => s.Id != customer.RegisteredInStoreId))
                        model.RegisteredInStore = string.Empty;
                    else
                        model.RegisteredInStore = allStores.First(s => s.Id == customer.RegisteredInStoreId).Name;


                    model.TimeZoneId = customer.GetAttribute<string>(SystemCustomerAttributeNames.TimeZoneId);
                    model.VatNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.VatNumber);
                    model.VatNumberStatusNote = ((VatNumberStatus)customer.GetAttribute<int>(SystemCustomerAttributeNames.VatNumberStatusId))
                        .GetLocalizedEnum(_localizationService, _workContext);
                    model.CreatedOn = _dateTimeHelper.ConvertToUserTime(customer.CreatedOnUtc, DateTimeKind.Utc);
                    model.LastActivityDate = _dateTimeHelper.ConvertToUserTime(customer.LastActivityDateUtc, DateTimeKind.Utc);
                    model.LastIpAddress = customer.LastIpAddress;
                    model.LastVisitedPage = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastVisitedPage);

                    model.SelectedCustomerRoleIds = customer.CustomerRoles.Select(cr => cr.Id).ToList();


                }



            }







                }
            }

        }