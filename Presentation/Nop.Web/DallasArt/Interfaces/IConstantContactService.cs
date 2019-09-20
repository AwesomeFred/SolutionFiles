using DallasArt.Models.V_3_9.Models;
using InContactSdk.ContactLists;
using Nop.Web.DallasArt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Nop.Web.DallasArt.Services.ConstantContactService;

namespace Nop.Web.DallasArt.Interfaces
{
    public interface IConstantContactService
    {

        Task<ConstantContactModel> CreateAConstantContactList(string listName);

        Task<ConstantContactBulkResponse> ClearConstantContactList(ContactList currentList);

        Task<ServiceResponse> ExportConstantContactList(IList<PaaMember> members, string listName);

    }
}
