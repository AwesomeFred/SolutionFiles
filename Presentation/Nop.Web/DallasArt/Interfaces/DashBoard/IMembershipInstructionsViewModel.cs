using System.ComponentModel.DataAnnotations;

namespace Nop.Web.DallasArt.Interfaces.DashBoard
{
    public interface IMembershipInstructionsViewModel
    {

        string SelectedGroupId { get; set; }

        string ErrorMessage { get; set; }

        string EmailInstructions { get; set; }

    //    [Display(Name = "Email Address of Person(s) (place ; between address) to send Invitation to")]
        string EmailAddress { get; set; }

        bool IsEmail { get; set; }

        bool IsResponse { get; set; }

    }
}