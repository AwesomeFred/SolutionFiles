using System.Web.Mvc;
using Nop.Web.Themes.PAA.ViewModels;

namespace Nop.Web.DallasArt.Interfaces
{

    public interface IEnrollController
    {
        ActionResult EnrollCustomer(string action, string id);
        ActionResult EnrollCustomer(RegistrationViewModel model, string id, string returnUrl, bool captchaValid, FormCollection form);
    }

}