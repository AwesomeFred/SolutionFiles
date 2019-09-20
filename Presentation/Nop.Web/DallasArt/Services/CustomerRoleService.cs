using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.DallasArt;
using Nop.Core.Domain.Stores;
using Nop.Services.Customers;

using Nop.Web.DallasArt.Interfaces;

namespace Nop.Web.DallasArt.Services
{


    /// <summary>
    ///  Set Roles for customer when enrolling or registering
    /// </summary>

    public class CustomerRoleService : ICustomerRoleService
    {
        private readonly CustomerSettings _customerSettings;
        private readonly IStoreContext _storeContext;
        private readonly ICustomerService _customerService;



        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<MemberEnrollment> _enrollmentLocation;
        private readonly IRepository<GenericAttribute> _genericAttributeRepository;



        public CustomerRoleService(
               CustomerSettings customerSettings,
               IStoreContext storeContext,
               ICustomerService customerService,
               IRepository<Customer> customerRepository,
               IRepository<MemberEnrollment> enrollmentLocation,
               IRepository<GenericAttribute> genericAttributeRepository
            //              IMessageService messageService
            )
        {
            this._customerSettings = customerSettings;
            this._storeContext = storeContext;
            this._customerService = customerService;
            this._customerRepository = customerRepository;
            this._enrollmentLocation = enrollmentLocation;
            this._genericAttributeRepository = genericAttributeRepository;
            //          this. _messageService =  messageService;

        }




        public void SetRole(Customer customer, CustomerRole role)
        {
            if (customer == null || role == null)
                return;

            if (_customerSettings.UserRegistrationType == UserRegistrationType.Disabled)
                return;

          //  Store store = _storeContext.CurrentStore;
          //  if (store.Name.ToLower() != _evantellStore)
         //       return;

            try
            {
                customer.CustomerRoles.Add(role);
            }
            catch (Exception ex)
            {
                throw new NopException(ex.Message);
            }

        }





        public List<Customer> GetCustomersInRole(string systemName)
        {
            List<Customer> inRole =
                _customerRepository.Table.Include(c => c.CustomerRoles)
                    .Where(d => d.CustomerRoles.Any(r => r.SystemName == systemName))
                       .ToList();

            return inRole;
        }


    


        ///// <summary>
        ///// Remove existing customers from role and add customer to that roll
        ///// </summary>
        ///// <param name="customer"></param>
        ///// <param name="role"></param>
        ///// <returns></returns>
        public bool SetRollUnique(Customer customer, CustomerRole role)
        {
        //    try
        //    {
        //         // remove all old customers from role
        //        foreach (Customer centerCustomer in GetCustomersByCenterLocation(customer))
        //        {
        //            var customer1 = centerCustomer;

        //               var oldRule =    _customerRepository.Table.Include(c => c.CustomerRoles)
        //                          .Where(cu => cu.Id == customer1.Id)
        //                          .Select(r => r.CustomerRoles.Where(z=>z.SystemName == role.SystemName )).FirstOrDefault()  ;

        //            if (oldRule != null)
        //            {
        //                customer1.CustomerRoles.Remove(oldRule.FirstOrDefault());
        //                _customerRepository.Update(customer1);
        //            }
        //        }
                
        //        // set customer to roll
        //        customer.CustomerRoles.Add(role);
        //        _customerRepository.Update(customer);

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }

            return false;

        }



    }



}
