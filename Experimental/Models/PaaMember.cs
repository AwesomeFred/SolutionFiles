using System;
using System.Collections.Generic;


namespace Experimental.Models
{
    public class PaaMember
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int PaaMemberId { get; set; }

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
        public string FamilyMemberEmail { get; set; }
        public bool NonPayingMember { get; set; }
        public bool HomePageApproval { get; set; }
        public string Phone { get; set; }

        public DateTime Renewal { get; set; }

        public KeyValuePair<int,string> Status { get; set; }
         
      
    }
}