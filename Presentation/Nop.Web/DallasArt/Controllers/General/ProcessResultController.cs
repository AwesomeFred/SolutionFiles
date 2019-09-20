using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Services.Common;
using Nop.Services.Messages;
using Nop.Web.DallasArt.Classes;

namespace Nop.Web.DallasArt.Controllers.General
{
    public class ProcessResultController  : Controller
    {

        #region Fields

        private readonly CustomerSettings _customerSettings;
        private readonly IWorkContext _workContext;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IWebHelper _webHelper;
      
        private readonly string _storeName;


        #endregion

        public ProcessResultController(
            CustomerSettings customerSettings,
            IWorkContext workContext,
            IStoreContext storeContext,
            IGenericAttributeService genericAttributeService,
            IWorkflowMessageService workflowMessageService,
            IWebHelper webHelper
              )
        {
            this._customerSettings = customerSettings;
            this._workContext = workContext;
            this._genericAttributeService = genericAttributeService;
            this._workflowMessageService = workflowMessageService;
            this._webHelper = webHelper;
            this._storeName = storeContext.CurrentStore.Name;
            
        }


                
         const  string empty = "";


        public   ActionResult ProcessRoute(Core.Domain.Customers.Customer customer, string returnUrl )
        {
            var registerResult =     Utilities.ViewPath(_storeName, "RegisterResult");


            switch (  _customerSettings.UserRegistrationType  )
               
            {
                case UserRegistrationType.EmailValidation:
                    {
                        //email validation message
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.AccountActivationToken,
                            Guid.NewGuid().ToString());
                        _workflowMessageService.SendCustomerEmailValidationMessage(customer, _workContext.WorkingLanguage.Id);

                        //result
                        return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.EmailValidation });
                    }
                case UserRegistrationType.AdminApproval:
                    {
                        return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.AdminApproval });
                    }
                case UserRegistrationType.Standard:
                    {
                        //send customer welcome message
                        _workflowMessageService.SendCustomerWelcomeMessage(customer, _workContext.WorkingLanguage.Id);

                        var redirectUrl =          Url.RouteUrl(registerResult, new { resultId = (int)UserRegistrationType.Standard });

                        if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            redirectUrl = _webHelper.ModifyQueryString(redirectUrl, "returnurl=" + HttpUtility.UrlEncode(returnUrl), null);

                        return Redirect(redirectUrl);
                    }
                default:
                    {
                        return RedirectToRoute("HomePage");
                    }
            }

        }
    }
}