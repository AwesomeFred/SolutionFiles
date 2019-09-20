using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Stores;
using Nop.Core.Infrastructure;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Stores;
using Nop.Web.DallasArt.Services;
using Nop.Web.DallasArt.ViewModels.Customer;
//using CustomerAttribute = Nop.Web.DallasArt.Controllers.Customer.CustomerAttribute;

namespace Nop.Web.DallasArt.Classes
{
    public  class MemberList
    {
        readonly ICustomerService _customerService = EngineContext.Current.Resolve<ICustomerService>();
   

        private IList<int> ValidMemberRoleIds()
        {
            IList<int> validRoleIds = new List<int>();
                

            foreach (CustomerRole role in _customerService.GetAllCustomerRoles())
            {
                switch (role.SystemName)
                {

                    case "AdultMembership":
                    case "FamilyMembership":
                    case "HonoraryMembership":
                    case "SeniorFamilyMembership":
                    case "SeniorMembership":
                    case "StudentMembership":
                    {
                        validRoleIds.Add(role.Id);
                        break;
                    }

                    default: continue;
                }

            }
            return validRoleIds;
        }


        public  IList<MemberListViewModel> GetMembers(int storeId )
        {




            IList<MemberListViewModel> membersList = new List<MemberListViewModel>();

            try
            {
                int[] roleIds = ValidMemberRoleIds().ToArray();

                IPagedList<Customer> members
                = _customerService.GetAllCustomers(customerRoleIds: roleIds, pageIndex: 0, pageSize: 500);

               
                foreach (Customer member in members)
                {

                    var ml = new MemberListViewModel();


                    // add custom customer attributes
                    foreach (CustomerServices.AttributeValuePair customerAttr in Utilities.GetCustomerAttributes(member))
                    {

                        switch (customerAttr.ValueId)
                        {

                            case (int)Utilities.CustomerAttribute.SecondMember:
                                {
                                    ml.NonPayingMember = customerAttr.ValueText == "Yes";

                                    break;
                                }

                            case (int)Utilities.CustomerAttribute.Url:
                                {
                                    ml.Url = customerAttr.ValueText;

                                    break;
                                }

                            case (int)Utilities.CustomerAttribute.ArtWork:
                                {

                                    ml.ArtWork = customerAttr.ValueText;

                                    break;
                                }


                            case (int)Utilities.CustomerAttribute.Renewal:
                                {

                                    DateTime reNewDate = DateTime.MinValue;

                                    DateTime.TryParse(customerAttr.ValueText, out reNewDate);

                                    if (reNewDate != DateTime.MinValue) ml.Renewal = reNewDate;


                                    break;
                                }

                        }

                    }

                    

                    ml.Email = member.Email;

                    ml.FirstName =    member.GetAttribute<string>(SystemCustomerAttributeNames.FirstName, storeId);
                    ml.LastName =     member.GetAttribute<string>(SystemCustomerAttributeNames.LastName, storeId);

                    ml.Company = member.GetAttribute<string>(SystemCustomerAttributeNames.Company, storeId);

                    ml.StreetAddress = member.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress, storeId);
                    ml.StreetAddress2 = member.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress2, storeId);

                    ml.City = member.GetAttribute<string>(SystemCustomerAttributeNames.City, storeId);
                    ml.ZipCode = member.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode, storeId);

                    ml.State = member.GetAttribute<string>(SystemCustomerAttributeNames.StateProvinceId, storeId);
                    
                    membersList.Add(ml);
                }

            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return membersList;
             
        }






    }
}