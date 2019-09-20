using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.Mvc;
using FluentValidation.Attributes;
using System.Linq;
using Nop.Core.Infrastructure;
using Nop.Web.DallasArt.Validators.Contact;
using Nop.Web.Framework.Security.Captcha;



namespace Nop.Web.DallasArt.ViewModels
{

    [Validator(typeof(ContactRequestRegisterValidator))]
    public class ContactRequestViewModel     // : IContactRequestViewModel
    {
        private SelfIdentificationViewModel _identificationViewModel = new SelfIdentificationViewModel();
        private readonly DisplayCaptchaViewModel _displayCaptchaViewModel = new DisplayCaptchaViewModel(true);


        public ContactRequestViewModel()
        {
            ResetCategoryList();
            SetDefaults();
        }

        private SelfIdentificationViewModel BuildSelfId()
        {
            return _identificationViewModel = new SelfIdentificationViewModel()
            {
                // set options 
                RequestEmail = true,
                RequestPassword = true,
                RequestPhoneNumber = true
            };
        }

        private void SetDefaults()
        {
            CaptchaSettings captchaSettings = EngineContext.Current.Resolve<CaptchaSettings>();
            
            this._identificationViewModel = BuildSelfId();

            this.Member = _identificationViewModel.Member;
            this.CurrentCustomer = _identificationViewModel.Customer;
            this.DisplayCaptcha = captchaSettings.Enabled;
        }



        public   void ResetCategoryList()
        {
            var items = CategoryItems().OrderBy(o => o.Text).ToList();

            // bottom Item
            items.Insert(items.Count, new SelectListItem { Value = "-1", Text = "Other" });
            // top item
            items.Insert(0, new SelectListItem { Value = string.Empty, Text = "Please choose from this list." });

            CategorySelectListItems = new SelectList(items, "Value", "Text", 0);
        }


        private static IEnumerable<SelectListItem> CategoryItems()
        {
            return new Collection<SelectListItem>
            {
                    new SelectListItem { Value = "10", Text = "Video Studies"},
                    new SelectListItem { Value = "9",  Text = "In-Service Videos"},
                    new SelectListItem { Value = "8",  Text = "Down-loadable Documents"},
                    new SelectListItem { Value = "7",  Text = "Check Out"},
                    new SelectListItem { Value = "6",  Text = "Credit Card"},
                    new SelectListItem { Value = "5",  Text = "Shopping Cart"},
                    new SelectListItem { Value = "4",  Text = "Products"},
                    new SelectListItem { Value = "3",  Text = "Website"},
                    new SelectListItem { Value = "2",  Text = "Membership"},
                    new SelectListItem { Value = "1",  Text = "Enrollment"},
            };
        }


        public SelfIdentificationViewModel SelfIdentificationViewModel
        {
            get { return _identificationViewModel; }
            set { _identificationViewModel = value; }
        }

        public Core.Domain.Customers.Customer CurrentCustomer { get; set; }

        public virtual bool Member { get; set; }

        public virtual int Store { get; set; }

        public virtual SelectListItem SelectedOption { get; set; }

        [DisplayName("Choose Topic")]
        public int SelectedCategoryId { get; set; }

        public IEnumerable<SelectListItem> CategorySelectListItems { get; set; }

        [Description("Please enter your question")]
        public virtual string Question { get; set; }

        public virtual DateTime QuestionAsk { get; set; }

        public virtual DateTime QuestionResolved { get; set; }

        public virtual bool DisplayCaptcha { get; set; }

        public virtual bool DisplaySuccess { get; set; }

        public DisplayCaptchaViewModel Captcha { get { return _displayCaptchaViewModel; } }

        
    }
}
