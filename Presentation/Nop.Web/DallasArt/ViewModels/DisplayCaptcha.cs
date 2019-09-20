using System.ComponentModel;
using System.Web.Mvc;
using Nop.Web.DallasArt.Interfaces;

namespace Nop.Web.DallasArt.ViewModels
{
   

    public class DisplayCaptchaViewModel : IDisplayCaptchaViewModel
    {

        public DisplayCaptchaViewModel()
        {
            this.DisplayCaptcha = false;           
        }
        
        public DisplayCaptchaViewModel(bool display )
        {
            this.DisplayCaptcha = display;
        }

        [DisplayName("Click In Square Box")]
        public bool DisplayCaptcha { get; set; }


        public static void CaptchaValidation
                                              (bool currentState,
                                               bool displayCaptcha,
                                               ModelStateDictionary modelState
                                             ) 
        {
            if (displayCaptcha && !currentState)
            {
                modelState.AddModelError("SquareBoxError", ("You Must Click The Square Box!"));
            }
        }
    }

}