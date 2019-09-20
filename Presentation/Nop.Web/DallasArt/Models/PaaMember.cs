using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.Validators.PaaMember;
using static Nop.Web.DallasArt.Services.MembershipService;

namespace Nop.Web.DallasArt.Models
{

    [Validator(typeof(PaaMemberValidator))]
    public class PaaMember :  IPaaMember 
    {

        public int CountryId { get;  set; }
        public int StateProvinceId { get; set; }


        public int PaaMemberId { get; set; }


        //[PlaceHolder("Email")]


        public string Email { get; set; }


        [DisplayName("First Name")]
        public string FirstName { get; set; }


        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Company { get; set; }


        [DisplayName("Address")]
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }


        public string City { get; set; }

        
        public string StateAbbreviation { get; set; }


        public StateProvince State { get; set; }



        [DisplayName("ZipCode")]
        public string ZipCode { get; set; }



        public string Phone { get; set; }



        public DateTime Renewal { get; set; }




        [DisplayName("Website Url")]
        public string Url { get; set; }



        public string ArtWork { get; set; }




        [DisplayName("Title")]
        public string ArtWorkTitle { get; set; }



        public KeyValuePair<int, string> Status { get; set; }
        public CustomerRole Membership { get; set; }


        public List<CustomerRole> CustomerRoles { get; set; }


        public bool PartnerRequired { get; set; }


        [DisplayName("Email")]
        public string PartnerEmail { get; set; }

        [DisplayName("First Name")]
        public string PartnerFirstName { get; set; }
        [DisplayName("Last Name")]
        public string PartnerLastName { get; set; }


        [DisplayName("Membership Type")]
        public SelectedRole SelectedRole { get; set; }

        public  Classes.Enums.States SelectState { get;  set; }

        [DisplayName("Home Slider Image Approved")]
        public Classes.Enums.Approved HomeSlideApproved { get; set; }

    }
}