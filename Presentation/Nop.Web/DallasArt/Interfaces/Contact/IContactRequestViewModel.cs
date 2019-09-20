using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Core;
using Nop.Services.Customers;
using Nop.Web.DallasArt.ViewModels;
using Nop.Web.Framework.Security.Captcha;

namespace Nop.Web.DallasArt.Interfaces.Contact
{
    public interface IContactRequestViewModel
    {
        ///// <summary>
        /////  Setup ContactRequestViewModel for Customer Defaults
        ///// </summary>
        ///// <param name="workContext"></param>
        ///// <param name="customerService"></param>
        ///// <param name="captchaSettings"></param>
        ///// <param name="form"></param>
        //void SetIdDefaults(
        //    IWorkContext workContext,
        //    ICustomerService customerService,
        //    ICustomerAttributeService customerAttributeService,
        //    CaptchaSettings captchaSettings,
        //    FormCollection form
        //    );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="form"></param>
        /// <param name="workContext"></param>
        /// <param name="customerService"></param>
        /// <param name="customerAttributeService"></param>
        /// <param name="captchaSettings"></param>
        void PostProcessing(
            ContactRequestViewModel model,
            FormCollection form,
            IWorkContext workContext,
            ICustomerService customerService,
            ICustomerAttributeService customerAttributeService,
            CaptchaSettings captchaSettings
            );

        SelfIdentificationViewModel SelfIdentificationViewModel { get; set; }
        
        Core.Domain.Customers.Customer CurrentCustomer { get; set; }
        
        bool Member { get; set; }
        
        int Store { get; set; }
        
        SelectListItem SelectedOption { get; set; }

        int SelectedCategoryId { get; set; }
        IEnumerable<SelectListItem> CategorySelectListItems { get;  }
 
        string Question { get; set; }
        
        DateTime QuestionAsk { get; set; }
        
        DateTime QuestionResolved { get; set; }
        
        bool DisplayCaptcha { get; set; }
        
        bool DisplaySuccess { get; set; }

        DisplayCaptchaViewModel Captcha { get; }




    }
}