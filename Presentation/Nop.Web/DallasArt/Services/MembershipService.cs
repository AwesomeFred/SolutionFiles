using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.Models;
using Nop.Services.Directory;
using Nop.Core.Domain.Directory;
using System.ComponentModel.DataAnnotations;
using Nop.Web.DallasArt.Classes.Enums;

namespace Nop.Web.DallasArt.Services
{
    public class MembershipService : IMembershipService
    {

        private readonly ICustomerService _customerService;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;


        private readonly DateTime _today = DateTime.Today;
        private readonly DateTime _fortyFiveDayAgo = DateTime.Today.AddDays(-45);
        private readonly DateTime _fifteenDaysFromNow = DateTime.Today.AddDays(15);
        private readonly DateTime _longExpired = DateTime.Today.AddDays(-60);


        public  enum SelectedRole
        {
            [Display(Name = "Select Membership Type")]
            Select_Membership_Type = 0,

            [Display(Name = "Adult Member")]
            AdultMembership = 7,

            [Display(Name = "Family Member")]
            FamilyMembership1 = 9,

            [Display(Name = "Family Member Partner")]
            FamilyMembership2 = 14,

            [Display(Name = "Honorary Member")]
            HonoraryMembership = 13,
            
            [Display(Name = "Senior Family Member")]
            SeniorFamilyMembership1 = 10,

            [Display(Name = "Senior Family Member Partner")]
            SeniorFamilyMembership2 = 15,

            [Display(Name = "Senior Member")]
            SeniorMembership = 8,

            [Display(Name = "Student Member")]
            StudentMembership = 6
        }


        public enum StatusCode
        {
            expired_no_grace = 0,
            expired_no_grace_less_then_fifteen_days = 1,
            expired_grace_less_than_fortyfive_days = 2,
            current = 3,
            current_expiring_less_than_fifteen_days = 4,
            current_honorary = 10
        }


        public MembershipService(
                                    ICustomerService customerService,
                                    ICountryService countryService,
                                    IStateProvinceService stateProvinceService
                                )
        {
            _countryService = countryService;
            _stateProvinceService = stateProvinceService;
            _customerService = customerService;
        }

        private int count = 0;

        //public IList<Customer> Customers(PasswordFormat passwordFormat)
      
        public IList<PaaMember> MembersList(int storeId = 0)
        {
            IList<PaaMember> members = new List<PaaMember>();

           

            foreach (Customer customer in GetCustomersWhoAreMembers())
            {

                PaaMember member = BuildPaaMember(customer, storeId);

                if (member.CountryId == 0) count++;

                SetPaaMemberStatus(ref member);

                members.Add(member);
            }

            return members;
        }

        public PaaMember GetMemberById(int memberId, int storeId = 0)
        {

            Customer customer = _customerService.GetCustomerById(memberId);
            PaaMember member = BuildPaaMember(customer, storeId);

            if (member != null)
            {
                SetPaaMemberStatus(ref member);
                return member;

            }
            return null;
        }


        public PaaMember CreateMember(PaaMember member)
        {

            return member;
        }

        public PaaMember UpdateMember(PaaMember member)
        {

            return member;
        }



        public IList<PaaMember> Paa_Current_MemberList(int storeId = 0)
        {
            IList<PaaMember> allMembers = MembersList(storeId = 0);
            List<PaaMember> members = new List<PaaMember>();

            members.AddRange(allMembers.Where(x => x.Status.Key.Equals((int)StatusCode.current_honorary)));
            members.AddRange(allMembers.Where(x => x.Status.Key.Equals((int)StatusCode.current)));
            members.AddRange(allMembers.Where(x => x.Status.Key.Equals((int)StatusCode.current_expiring_less_than_fifteen_days)));
            members.AddRange(allMembers.Where(x => x.Status.Key.Equals((int)StatusCode.expired_grace_less_than_fortyfive_days)));
            return members;
        }

        public IList<PaaMember> Paa_ExpiringSoonList(int storeId = 0)
        {
            IList<PaaMember> allMembers = MembersList(storeId = 0);
            List<PaaMember> members = new List<PaaMember>();

            members.AddRange(allMembers.Where(x => x.Status.Key.Equals((int)StatusCode.current_expiring_less_than_fifteen_days)));
            return members;
        }

        public IList<PaaMember> Paa_Expired_GraceList(int storeId = 0)
        {
            IList<PaaMember> allMembers = MembersList(storeId = 0);
            List<PaaMember> members = new List<PaaMember>();

            members.AddRange(allMembers.Where(x => x.Status.Key.Equals((int)StatusCode.expired_grace_less_than_fortyfive_days)));
            return members;
        }

        public IList<PaaMember> Paa_Expired_RecentlyList(int storeId = 0)
        {
            IList<PaaMember> allMembers = MembersList(storeId = 0);
            List<PaaMember> members = new List<PaaMember>();

            members.AddRange(allMembers.Where(x => x.Status.Key.Equals((int)StatusCode.expired_no_grace_less_then_fifteen_days)));
            return members;

        }

        public IList<PaaMember> Paa_ExpiredList(int storeId = 0)
        {
            IList<PaaMember> allMembers = MembersList(storeId = 0);
            List<PaaMember> members = new List<PaaMember>();

            members.AddRange(allMembers.Where(x => x.Status.Key.Equals((int)StatusCode.expired_no_grace)));
            return members;
        }

        private List<Customer> GetCustomersWhoAreMembers()
        {
            int[] roleIds = ValidMemberRoleIds(_customerService).ToArray();
            IPagedList<Customer> members =
               _customerService.GetAllCustomers(customerRoleIds: roleIds, pageIndex: 0, pageSize: 500);

            return members.ToList();
        }

        private void GetCustomerCountryState(Customer customer, ref PaaMember member, int storeId = 0)
        {

            string countryId = customer.GetAttribute<string>(SystemCustomerAttributeNames.CountryId, storeId);
            string stateId = customer.GetAttribute<string>(SystemCustomerAttributeNames.StateProvinceId, storeId);

            int stateid = 0;
            int countryid = 0;

            bool validC = Int32.TryParse(countryId, out countryid);
            bool validS = Int32.TryParse(stateId, out stateid);

            if (validC && validS)
            {
                member.CountryId = countryid;
                member.StateProvinceId = stateid;

                StateProvince state = _stateProvinceService.GetStateProvinceById(stateid);
                member.State = (state != null) ? state
                                                : new StateProvince { Name = "", Abbreviation = "??" };

            }
        }

        private PaaMember BuildPaaMember(Customer customer, int storeId)
        {
            if (customer == null) return null;

            PaaMember m = new PaaMember
            {
                PaaMemberId = customer.Id,
                Email = customer.Email,
                FirstName = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName, storeId),
                LastName = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName, storeId),
                StreetAddress = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress, storeId),
                City = customer.GetAttribute<string>(SystemCustomerAttributeNames.City, storeId),
                ZipCode = customer.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode, storeId),
                Phone = customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone, storeId),
                CustomerRoles = customer.CustomerRoles.ToList(),

               
            };

            GetCustomerCountryState(customer, ref m);

  

            // role selected & helper flags
            foreach (CustomerRole role in m.CustomerRoles)
            {
       
                if (role.Name.ToLower().Contains("member"))  // should be only 1 !!
                {
                    // set membership
                    m.Membership = role;

                    // set membership drop-down  selected value                     
                    m.SelectedRole = (SelectedRole) m.Membership.Id;
                }

                if (role.Name.ToLower().Contains("family"))
                {
                    m.PartnerRequired = true;
                }
            }


            // custom Attributes
            foreach (CustomerServices.AttributeValuePair customerAttr
                          in Utilities.GetCustomerAttributes(customer))
            {
                switch (customerAttr.ValueId)
                {
                    case (int)CustomerCustomAttribute.Renewal:
                        {
                            DateTime expires;

                            if (DateTime.TryParse(customerAttr.ValueText, out expires))
                            {
                                m.Renewal = expires;
                            }
                            break;
                        }

                    case (int)CustomerCustomAttribute.Url:
                        {
                            m.Url = customerAttr.ValueText;
                            break;
                        }

                    case (int)CustomerCustomAttribute.PartnerEmail:
                        {
                            m.PartnerEmail = customerAttr.ValueText;

                            if (m.PartnerRequired)
                            {
                                if( !string.IsNullOrEmpty(m.PartnerEmail) )
                                {
                                    Customer  partner = _customerService.GetCustomerByEmail(m.PartnerEmail);

                                    if (partner != null)
                                    {
                                        m.PartnerFirstName  = partner.GetAttribute<string>(SystemCustomerAttributeNames.FirstName, storeId);
                                        m.PartnerLastName    = partner.GetAttribute<string>(SystemCustomerAttributeNames.LastName, storeId);
                                    }

                                }

                            }
                            break;
                        }

                    case (int)CustomerCustomAttribute.ArtWork:
                        {
                            m.ArtWork = customerAttr.ValueText;
                            break;
                        }


                    case (int)CustomerCustomAttribute.HomePageSlideTitle:
                        {
                            m.ArtWorkTitle = customerAttr.ValueText;
                            break;
                        }


                    //case (int)Utilities.CustomerAttribute.ArtworkApproval:
                    //    {
                    //        m.HomeSlideApproved = (customerAttr.ValueText.ToLower() == "yes") 
                    //            ? Classes.Enums.Approved.Yes 
                    //            : Classes.Enums.Approved.No;    //HomePageApproval 
                    //        break;
                    //    }

                    case (int)CustomerCustomAttribute.HomePageApprovalYes:
                        {
                            m.HomeSlideApproved = Approved.Yes;
                            break;
                        }

                    case (int)CustomerCustomAttribute.HomePageApprovalNo:
                        {
                            m.HomeSlideApproved = Approved.No;
                            break;
                        }

                }
            }
            return m;
        }

        public void SetPaaMemberStatus(ref PaaMember member)
        {
            DateTime renewal = member.Renewal;

            // special case  
            if (member.Membership != null && member.Membership.Name.ToLower() == "honorary membership")
            {
                member.Status = new KeyValuePair<int, string>((int)StatusCode.current, "Current");
                return;
            }

            if (renewal == DateTime.MinValue)
            {
                member.Status = new KeyValuePair<int, string>(-1, "Error");
                return;
            }

            if (renewal >= _today)
            {
                if (renewal <= _fifteenDaysFromNow)
                {
                    member.Status = new KeyValuePair<int, string>((int)StatusCode.current_expiring_less_than_fifteen_days, "Expiring Soon");
                    return;
                }
                else
                {
                    member.Status = new KeyValuePair<int, string>((int)StatusCode.current, "Current");
                    return;
                }
            }

            if (renewal <= _fortyFiveDayAgo)
            {
                if (renewal <= _longExpired)
                {
                    member.Status = new KeyValuePair<int, string>((int)StatusCode.expired_no_grace, "Expired");
                    return;
                }
                else
                {
                    member.Status = new KeyValuePair<int, string>((int)StatusCode.expired_no_grace_less_then_fifteen_days, "Expired Recently");
                    return;
                }
            }

            if (renewal >= _fortyFiveDayAgo)
            {
                member.Status = new KeyValuePair<int, string>((int)StatusCode.expired_grace_less_than_fortyfive_days, "Expired Grace");
                return;
            }
        }

        public static IList<int> ValidMemberRoleIds(ICustomerService customerService)
        {
            IList<int> validRoleIds = new List<int>();

            foreach (CustomerRole role in customerService.GetAllCustomerRoles())
            {
                switch (role.SystemName)
                {

                    case "AdultMembership":
                    case "FamilyMembership1":
                    case "FamilyMembership2":
                    case "HonoraryMembership":
                    case "SeniorFamilyMembership1":
                    case "SeniorFamilyMembership2":
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






    }
}