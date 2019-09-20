using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.ViewModels;

namespace Nop.Web.DallasArt.Interfaces.Contact
{

       public interface IRadioButtionList
        {
            string Name { get; set; }


        [Required(ErrorMessage = "Please Select Member or Non-Member Status")]
        int SelectedRole { get; set; }

            List<IMembershipRoll> Roles { get; set; }
        }



        public interface IMembershipRoll
    {
            int Id { get; set; }
            string RollName { get; set; }
        }




    public interface ISelfIdentificationViewModel
    {


        IRadioButtionList RadioButtionList { get; set; }
     

          

        string CenterName { get; set; }
        string CenterId { get; set; }

        string SelectedAnswer { get; set; }
        List<DropDownOption> Options { get; }
        bool DisplayOption { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Phone { get; set; }
        string ConfirmPhone { get; set; }
        string Email { get; set; }
        string ConfirmEmail { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        string Option { get; set; }
        bool RequestPhoneNumber { get; set; }
        bool RequestPassword { get; set; }
        bool RequestEmail { get; set; }
        Core.Domain.Customers.Customer Customer { get; set; }
        bool Member { get; set; }
        int Store { get; set; }
        string PrayerCoordinatorEmail { get; set; }
        string PrayerCoordinatorConfirmEmail { get; set; }


        bool AskForMembershipStatus { get; set; }



        /// <summary>
        /// Use this property to store any custom value for your models. 
        /// </summary>
        Dictionary<string, object> CustomProperties { get; set; }

        void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext);

        /// <summary>
        /// Developers can override this method in custom partial classes
        /// in order to add some custom initialization code to constructors
        /// </summary>
        void PostInitialize();
    }

}