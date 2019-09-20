using Nop.Web.DallasArt.ViewModels;

namespace Nop.Web.DallasArt.Interfaces
{
    public interface IContactRequestService
    {

        bool CreateContactRecord(ContactRequestViewModel model);

    }
}
