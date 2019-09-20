using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
using FluentValidation.Attributes;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Infrastructure;
using Nop.Services.Common;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.Interfaces.Contact;
using Nop.Web.DallasArt.Services;
using Nop.Web.DallasArt.Validators.Contact;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Web.DallasArt.ViewModels
{

    public class MembershipRoll : IMembershipRoll
    {
        public int Id { get; set; }
        public string RollName { get; set; }
    }

    public class  RadioButtionList : IRadioButtionList
    {
        public RadioButtionList()
        {

            Roles = new List<IMembershipRoll>
            {
                new MembershipRoll {Id = 1, RollName = "No"},
                new MembershipRoll {Id = 2, RollName = "Yes"}

               
            };
        }

        public string Name { get; set; } = "PAAMembership";


        [Required(ErrorMessage = "Please Select Member or Non-Member Status")]
        public int SelectedRole { get; set; }

        public List<IMembershipRoll> Roles { get; set; }

    }




    [Validator(typeof(SelfIdentificationValidator))]
    public class SelfIdentificationViewModel : BaseNopModel, ISelfIdentificationViewModel
    {
        private readonly List<DropDownOption> _options = new List<DropDownOption>();


        public bool ResetDefaults(SelfIdentificationViewModel s)
        {
            if (s.Customer == null)
            {
                var workContext = EngineContext.Current.Resolve<IWorkContext>();
                if (workContext != null)
                {
                    s.Customer = workContext.CurrentCustomer;
                    return true;
                }
            }

            return false;
        }


        public SelfIdentificationViewModel()
        {



            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            if (workContext == null) return;

            // if the customer is known lets get his info for him;
            if (!workContext.CurrentCustomer.IsGuest())
            {
                var customer = workContext.CurrentCustomer;
                this.Customer = customer;
                this.Email = customer.Email;
                //  this.Password = customer.Password;

                this.ConfirmPhone = this.Phone;
                this.ConfirmEmail = this.Email;
                this.ConfirmPassword = this.Password;


                this.FirstName = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName);
                this.LastName = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName);
                this.Phone = customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);

                this.ConfirmPhone = this.Phone;
                this.ConfirmEmail = this.Email;

                this.Password = string.Empty;
                this.ConfirmPassword = string.Empty;

                this.CenterName = CustomerServices.GetCustomerAttribute(Customer, CustomerServices.Center);
                this.CenterId = CustomerServices.GetCustomerAttribute(Customer, CustomerServices.CenterId);
                this.Member = true;
                this.DisplayOption = true;
            }
            else
            {
                this.Member = false;
            }
        }

        public bool VerifyEmailAddress { get; set; }
        public bool VerifyUsersPassword { get; set; }
        public bool VerifyPhoneNumber { get; set; }


        public string CenterName { get; set; }

        public string CenterId { get; set; }

        [DisplayName("Center Prayer Coordinator")]
        public string SelectedAnswer { get; set; }

        public List<DropDownOption> Options
        {
            get { return _options; }
        }

        public bool DisplayOption { get; set; }

        [Description("Please enter your First Name ")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Description("Please enter your Last Name ")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Description("Please enter your Phone Number ")]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }

        [Description("Please reenter your Phone Number ")]
        [DisplayName("Confirm Phone Number")]
        public string ConfirmPhone { get; set; }

        [Description("Please enter your Email Address ")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Description("Please reenter your Email Address ")]
        [DisplayName("Confirm Email")]
        public string ConfirmEmail { get; set; }

        [DataType(DataType.Password)]
        [NopResourceDisplayName("Account.Fields.Password")]
        [AllowHtml]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [NopResourceDisplayName("Account.Fields.ConfirmPassword")]
        [AllowHtml]
        public string ConfirmPassword { get; set; }


        public string Option { get; set; }

        public bool RequestPhoneNumber { get; set; }

        public bool RequestPassword { get; set; }
        public bool RequestEmail { get; set; }

        public Core.Domain.Customers.Customer Customer { get; set; }

        public bool Member { get; set; }

        public int Store { get; set; }

        public IRadioButtionList RadioButtionList { get; set; } = new RadioButtionList();


        public  IEnumerable<SelectListItem> RedioList { get; set; }




        public  bool AskForMembershipStatus { get; set; }



        [Description("Please enter your Email Address ")]
        [DisplayName("Prayer Coordinator Email")]
        public virtual string PrayerCoordinatorEmail { get; set; }

        [Description("Please reenter your Email Address ")]
        [DisplayName("Prayer Coordinator Confirm Email")]
        public virtual string PrayerCoordinatorConfirmEmail { get; set; }

       

        public new void PostInitialize()
        {
            throw new System.NotImplementedException();
        }
    }
}