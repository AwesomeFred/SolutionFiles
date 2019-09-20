using System;
using System.Collections.Generic;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;

using static Nop.Web.DallasArt.Services.MembershipService;

namespace Nop.Web.DallasArt.Interfaces
{
    public interface IPaaMember
    {
        int CountryId { get; set; }
        int StateProvinceId { get; set; }

        string ArtWork { get; set; }
        string ArtWorkTitle { get; set; }

        string City { get; set; }
        string Company { get; set; }
        string Email { get; set; }


        string FirstName { get; set; }
      
        string LastName { get; set; }
        CustomerRole Membership { get; set; }
        int PaaMemberId { get; set; }

        bool PartnerRequired { get; set; }
        string PartnerEmail { get; set; }
        string PartnerFirstName { get; set; }
        string PartnerLastName { get; set; }

        string Phone { get; set; }
        DateTime Renewal { get; set; }
        List<CustomerRole> CustomerRoles { get; set; }
        SelectedRole SelectedRole   { get; set; }
        StateProvince State { get; set; }
        KeyValuePair<int, string> Status { get; set; }
        string StreetAddress { get; set; }
        string StreetAddress2 { get; set; }
        string Url { get; set; }
        string ZipCode { get; set; }

        Classes.Enums.States SelectState { get; set; }

        Classes.Enums.Approved HomeSlideApproved { get; set; }

    }
}