using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Core;
using Nop.Services.Customers;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.ViewModels;
using Nop.Web.Framework.Security.Captcha;
using Nop.Web.Themes.Evantell.ViewModels.Customer;

namespace Nop.Web.DallasArt.Interfaces.Customer
{
    public interface IRegistrationViewModel
    {
        void SetIdDefaults
            (
            IWorkContext workContext,
            ICustomerService customerService,
            ICustomerAttributeService customerAttributeService,
            CaptchaSettings captchaSettings,
            FormCollection form
            );

        void PostProcessing
            (
            RegistrationViewModel model,
            FormCollection form,
            IWorkContext workContext,
            ICustomerService customerService,
            ICustomerAttributeService customerAttributeService,
            CaptchaSettings captchaSettings
            );

        SelfIdentificationViewModel SelfIdentificationViewModel { get; }
        CoordinatorViewModel CenterPrayerCoordinator { get; set; }
        bool EzLinkRequest { get; set; }
        bool DisplayCaptcha { get; set; }
        bool IsPrayerCoordinatorRequest { get; set; }
        DateTime OriginalEnrollmenDate { get; set; }
        int SatelliteaId { get; set; }
        int OriginalCustomerId { get; set; }
        Core.Domain.Customers.Customer CurrentCustomer { get; set; }
        bool Satellite { get; set; }
        bool SingleLocationCenter { get; set; }
        bool Member { get; set; }
        bool IsEnrollment { get; set; }
        string PrimaryCenterName { get; set; }
        string PrimaryCenterGroupId { get; set; }
        string NewCenter { get; set; }
        string Notify { get; set; }
        string EnrollmentType { get; set; }
        IList<IDropDownOption> EnrollmentSelectList { get; }
        string Username { get; set; }
        bool AcceptPrivacyPolicyEnabled { get; set; }
        string CenterNameRequest { get; set; }
        string CenterGroupIdRequest { get; set; }
        void HydrateCustomerAttribute();
    }
}