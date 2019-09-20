using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using Nop.Web.DallasArt.Validators.DashBoard;


namespace Nop.Web.DallasArt.ViewModels.DashBoard
{

    [Validator(typeof(DashBoardValidation))]
    public class MembershipInstructionsViewModel
    {
        public MembershipInstructionsViewModel()
        {
            ErrorMessage = string.Empty;
            IsResponse = false;
            EmailAddress = string.Empty; 
            EmailInstructions = string.Empty;
        }


        public string ErrorMessage { get; set; }

        public string EmailInstructions { get; set; }

        [Display(Name = "Email Address of Person(s) (place ; between address) to send Invitation to")]
        public string EmailAddress { get; set; }

        public bool IsEmail { get; set; }

        public bool IsResponse { get; set; }
    }
}