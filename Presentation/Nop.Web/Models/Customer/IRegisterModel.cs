using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Nop.Web.Models.Customer
{
    public interface IRegisterModel
    {
        bool AcceptPrivacyPolicyEnabled { get; set; }
        bool AllowCustomersToSetTimeZone { get; set; }
        IList<SelectListItem> AvailableCountries { get; set; }
        IList<SelectListItem> AvailableStates { get; set; }
        IList<SelectListItem> AvailableTimeZones { get; set; }
        bool CheckUsernameAvailabilityEnabled { get; set; }
        string City { get; set; }
        bool CityEnabled { get; set; }
        bool CityRequired { get; set; }
        string Company { get; set; }
        bool CompanyEnabled { get; set; }
        bool CompanyRequired { get; set; }
        string ConfirmEmail { get; set; }
        string ConfirmPassword { get; set; }
        bool CountryEnabled { get; set; }
        int CountryId { get; set; }
        bool CountryRequired { get; set; }
        IList<CustomerAttributeModel> CustomerAttributes { get; set; }
        int? DateOfBirthDay { get; set; }
        bool DateOfBirthEnabled { get; set; }
        int? DateOfBirthMonth { get; set; }
        bool DateOfBirthRequired { get; set; }
        int? DateOfBirthYear { get; set; }
        bool DisplayCaptcha { get; set; }
        bool DisplayVatNumber { get; set; }
        string Email { get; set; }
        bool EnteringEmailTwice { get; set; }
        string Fax { get; set; }
        bool FaxEnabled { get; set; }
        bool FaxRequired { get; set; }
        string FirstName { get; set; }
        string Gender { get; set; }
        bool GenderEnabled { get; set; }
        bool HoneypotEnabled { get; set; }
        string LastName { get; set; }
        bool Newsletter { get; set; }
        bool NewsletterEnabled { get; set; }
        string Password { get; set; }
        string Phone { get; set; }
        bool PhoneEnabled { get; set; }
        bool PhoneRequired { get; set; }
        bool StateProvinceEnabled { get; set; }
        int StateProvinceId { get; set; }
        bool StateProvinceRequired { get; set; }
        string StreetAddress { get; set; }
        string StreetAddress2 { get; set; }
        bool StreetAddress2Enabled { get; set; }
        bool StreetAddress2Required { get; set; }
        bool StreetAddressEnabled { get; set; }
        bool StreetAddressRequired { get; set; }
        string TimeZoneId { get; set; }
        string Username { get; set; }
        bool UsernamesEnabled { get; set; }
        string VatNumber { get; set; }
        string ZipPostalCode { get; set; }
        bool ZipPostalCodeEnabled { get; set; }
        bool ZipPostalCodeRequired { get; set; }

        DateTime? ParseDateOfBirth();
    }
}