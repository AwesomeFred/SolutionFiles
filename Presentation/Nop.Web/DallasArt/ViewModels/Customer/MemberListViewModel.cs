using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.DallasArt.ViewModels.Customer
{

    public class MemberListViewModel
    {
 

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Url { get; set; }
        
        public string ArtWork { get; set; }
    
        public string MembershipType { get; set; }
        
        public bool FamilyMemberEmail { get; set; }
        public bool NonPayingMember { get; set; }

        public string Phone { get; set; }

        public DateTime Renewal { get; set; }

    }
}