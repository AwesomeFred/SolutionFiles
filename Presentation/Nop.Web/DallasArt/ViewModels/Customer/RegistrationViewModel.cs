using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Core;
using Nop.Services.Customers;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.Validators.Customer;
using Nop.Web.Framework.Security.Captcha;

namespace Nop.Web.DallasArt.ViewModels.Customer
{
    [Validator(typeof(RegistrationValidation))]
    public class RegistrationViewModel
    {
        private SelfIdentificationViewModel _identificationViewModel;
        private readonly List<DropDownOption> _enrollmentOptions = new List<DropDownOption>();


        /// <summary>
        /// 
        /// </summary>
        public RegistrationViewModel()
        {
            HydrateCustomerAttribute();
        }


        public void SetIdDefaults
                                    (
                                       IWorkContext workContext,
                                       ICustomerService customerService,
                                       ICustomerAttributeService customerAttributeService,
                                       CaptchaSettings captchaSettings,
                                       FormCollection form
            )
        {

            bool showCaptcha = captchaSettings.Enabled; //&& captchaSettings.ShowOnContactUsPage;

            this.DisplayCaptcha = showCaptcha;

            SelfIdentificationViewModel self =
                  Utilities.SelfIdentificationViewModel(workContext, customerService, customerAttributeService, showCaptcha);

            self.RequestEmail = true;
            self.RequestPassword = true;
            self.RequestPhoneNumber = true;
    
            _identificationViewModel = self;

            this.IsPrayerCoordinatorRequest = false;
            this.Member = self.Member;
            this.CurrentCustomer = self.Customer;
            this.EzLinkRequest = false;

        }


        public void PostProcessing
                                    (
                                    RegistrationViewModel model,
                                    FormCollection form,
                                    IWorkContext workContext,
                                    ICustomerService customerService,
                                    ICustomerAttributeService customerAttributeService,
                                    CaptchaSettings captchaSettings
                                    )
        {

            SetIdDefaults(workContext, customerService, customerAttributeService, captchaSettings, form);
            SelfIdentificationViewModel self = model.SelfIdentificationViewModel;

            if (self.Member || form == null) return;

            // pick up form values
            self.FirstName = form["FirstName"];
            self.LastName = form["LastName"];
            self.Email = form["Email"];
            self.ConfirmEmail = form["ConfirmEmail"];
            self.Password = form["Password"];
            self.ConfirmPassword = form["ConfirmPassword"];
            self.Option = form["SelectedAnswer"];
            self.Phone = form["Phone"];
            self.PrayerCoordinatorEmail = form["PrayerCoordinatorEmail"];
            self.PrayerCoordinatorConfirmEmail = form["PrayerCoordinatorConfirmEmail"];


        }


        public SelfIdentificationViewModel SelfIdentificationViewModel { get { return _identificationViewModel; } }

        private void HydrateCustomerAttribute()
        {

            _enrollmentOptions.Insert(0, new DropDownOption { Value = "2", Name = "Satellite Location Center" });
            _enrollmentOptions.Insert(0, new DropDownOption { Value = "1", Name = "Primary Location Center" });
            _enrollmentOptions.Insert(0, new DropDownOption { Value = "0", Name = "Single Location Center" });

        }

        [DisplayName("Center Prayer Coordinator")]
        public CoordinatorViewModel CenterPrayerCoordinator { get; set; }


        public virtual bool EzLinkRequest { get; set; }
  
        public virtual bool DisplayCaptcha { get; set; }

        public virtual bool IsPrayerCoordinatorRequest { get; set; }
     

        // controls mode register/enrollment

        #region EnrollmentLocationGroup

        public virtual DateTime OriginalEnrollmenDate { get; set; }
        public virtual int SatelliteaId { get; set; }

        public virtual int OriginalCustomerId { get; set; }


        public virtual Core.Domain.Customers.Customer CurrentCustomer { get; set; }

        public virtual bool Satellite { get; set; }
        public virtual bool SingleLocationCenter { get; set; }

        public virtual bool Member { get; set; }


        #endregion

        #region Enrollment
        public bool IsEnrollment { get; set; }

        [DisplayName("Primary Center Name")]
        public virtual string PrimaryCenterName { get; set; }

        [DisplayName("Primary Center ID")]
        public virtual string PrimaryCenterGroupId { get; set; }


        [DisplayName("New Center Name")]
        public virtual string NewCenter { get; set; }

        [DisplayName("E-Mail Address To Notify")]
        public virtual string Notify { get; set; }


        #region Enrollment Type

        [DisplayName("Center Enrollment Type")]
        public virtual string EnrollmentType { get; set; }

        public List<DropDownOption> EnrollmentSelectList { get { return (_enrollmentOptions).OrderBy(o => o.Id).ToList(); } }


        //    public virtual SelectListItem SelectedOption { get; set; }


        #endregion

        #endregion
        // 


        public virtual string Username { get; set; }

        public bool AcceptPrivacyPolicyEnabled { get; set; }


        // attributes 


        [DisplayName("Center Name")]
        public virtual string CenterNameRequest { get; set; }


        [DisplayName("Center ID")]
        public virtual string CenterGroupIdRequest { get; set; }

    }
}