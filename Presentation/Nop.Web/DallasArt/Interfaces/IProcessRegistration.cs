using Nop.Web.Themes.PAA.ViewModels;

namespace Nop.Web.DallasArt.Interfaces
{
    public interface IProcessRegistration
    {
        bool RegisterUser(RegistrationViewModel model);
        bool AnnounceRegistation(Core.Domain.Customers.Customer customer);
    }
}