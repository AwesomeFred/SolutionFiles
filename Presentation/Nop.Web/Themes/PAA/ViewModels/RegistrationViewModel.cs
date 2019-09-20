using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Core.Domain.Customers;
using Nop.Core.Infrastructure;
using Nop.Web.DallasArt.ViewModels;
using Nop.Web.Framework.Security.Captcha;

namespace Nop.Web.Themes.PAA.ViewModels
{


    public enum CustomerRoleType
    {

        Administrators = 1,
        ForumModerators = 2,
        Registered = 3,
        Guests = 4,
        Vendors = 5,
        StudentMembership = 6,
        AdultMembership = 7,
        SeniorMembership = 8,
        FamilyMembership1 = 9,
        SeniorFamilyMembership1 = 10,
        Donor = 11,
        Officer = 12,
        HonoraryMembership = 13,
        FamilyMembership2 = 14,
        SeniorFamilyMembership2 = 15

    }



    public class RegistrationViewModel
    {




        private void SetDefaults()
        {
            CaptchaSettings captchaSettings = EngineContext.Current.Resolve<CaptchaSettings>();

            this.Member = SelfIdentificationViewModel.Member; // _identificationViewModel.Member;
            this.CurrentCustomer = SelfIdentificationViewModel.Customer;
            this.DisplayCaptcha = captchaSettings.Enabled;
            this.EzLinkRequest = false;
        }


        public ImageUploadViewModel ImageUploadViewModel { get; set; } 
           = new ImageUploadViewModel();


        public SelfIdentificationViewModel SelfIdentificationViewModel { get; set; }
            = new SelfIdentificationViewModel {RequestEmail = true , RequestPassword = true, RequestPhoneNumber = true};


        public DisplayCaptchaViewModel Captcha { get; } = new DisplayCaptchaViewModel(true);

        public virtual bool Member { get; set; }

        public virtual  Customer CurrentCustomer { get; set; }
        public virtual bool DisplayCaptcha { get; set; }

        public virtual bool EzLinkRequest { get; set; }


        public bool Donation { get; set; }

        public bool Family { get; set; }

        public int Type { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }


        public bool IsEnrollment { get; set; }


        public virtual string Username { get; set; }

        public bool AcceptPrivacyPolicyEnabled { get; set; }

        public virtual bool DisplaySuccess { get; set; } = false;


        public virtual string CurrentState { get; set; } = "Begin";


        public virtual int TotalCount { get; set; } = 0;


        public   string ErrorMessage { get; set; }  
        

    }
}