using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Core;
using Nop.Web.DallasArt.Interfaces;
using PaaMember = Nop.Web.DallasArt.Models.PaaMember;
using DallasArt.Models.V_3_9.Models;
using Nop.Web.DallasArt.Classes;



namespace Nop.Web.DallasArt.Controllers.ConstantContaxt
{
    public class ConstantContactController : Controller
    {
        #region fields

        private readonly IConstantContactService _constantContactService;
        private readonly IStoreContext _storeContext;
        private readonly IMembershipService _membershipService;

        #endregion

        #region Ctor

        public ConstantContactController(
            IStoreContext storeContext,
            IConstantContactService constantContactService,
            IMembershipService membershipService
        )
        {
            this._constantContactService = constantContactService;
            this._storeContext = storeContext;
            this._membershipService = membershipService;
        }

        #endregion



        private async Task<ServiceResponse> ExportListToConstantContact(string name)
        {
            IList<PaaMember> members = new List<PaaMember>();

            switch (name)
            {
                case "Paa_Current":
                    {
                        members = _membershipService.Paa_Current_MemberList();
                        break;
                    }

                case "Paa_Expiring_Soon":
                    {
                        members = _membershipService.Paa_ExpiringSoonList();
                        break;
                    }
                case "Paa_Expired_Grace":
                    {
                        members = _membershipService.Paa_Expired_GraceList();
                        break;
                    }
                case "Paa_Expired_Recently":
                    {
                        members = _membershipService.Paa_Expired_RecentlyList();
                        break;
                    }
                case "Paa_Expired":
                    {
                        members = _membershipService.Paa_ExpiredList();
                        break;
                    }


                default:
                    {
                        return new ServiceResponse { ExceptionMessage = "List Not Found", Name=name , Message = "Failed" };
                    }

            }

            return await _constantContactService.ExportConstantContactList(members, name);
        }

        //private readonly List<string> _availableLists = new List<string>
        //{
        //    "Paa_Current",
        //    "Paa_Expiring_Soon",
        //    "Paa_Expired_Grace",
        //    "Paa_Expired_Recently",
        //    "Paa_Expired"
        //};

        public async Task<ActionResult> Export_All_Lists()
        {
            IList<ServiceResponse> model = new List<ServiceResponse>();

            model.Add(await ExportListToConstantContact("Paa_Current"));
         //   model.Add(await ExportListToConstantContact("PAA_Expiring_Soon"));   // fake fail
            model.Add(await ExportListToConstantContact("Paa_Expiring_Soon"));
            model.Add(await ExportListToConstantContact("Paa_Expired_Grace"));
            model.Add(await ExportListToConstantContact("Paa_Expired_Recently"));
            model.Add(await ExportListToConstantContact("Paa_Expired"));

            return View(Utilities.CorrectViewPath("Shared/_SyncConstContactList"), model);
        }


        public async Task<ActionResult> Export_Paa_Current_Members()
        {
            IList<ServiceResponse> model = new List<ServiceResponse>();


            model.Add(await ExportListToConstantContact("Paa_Current"));

            return View(Utilities.CorrectViewPath("Shared/_SyncConstContactList"), model);
        }


        public async Task<ActionResult> Export_Paa_Expiring_Soon_Members()
        {
            IList<ServiceResponse> model = new List<ServiceResponse>();

            model.Add(await ExportListToConstantContact("Paa_Expiring_Soon"));

            return View(Utilities.CorrectViewPath("Shared/_SyncConstContactList"), model);
        }


        public async Task<ActionResult> Export_Paa_Expired_Grace_Members()
        {
            IList<ServiceResponse> model = new List<ServiceResponse>();

            model.Add(await ExportListToConstantContact("Paa_Expired_Grace"));

            return View(Utilities.CorrectViewPath("Shared/_SyncConstContactList"), model);
        }


        public async Task<ActionResult> Export_Paa_Expired_Recently_Members()
        {
            IList<ServiceResponse> model = new List<ServiceResponse>();

            model.Add(await ExportListToConstantContact("Paa_Expired_Recently"));


            return View(Utilities.CorrectViewPath("Shared/_SyncConstContactList"), model);
        }


        public async Task<ActionResult> Export_Paa_Expired_Members()
        {
            IList<ServiceResponse> model = new List<ServiceResponse>();


            model.Add(await ExportListToConstantContact("Paa_Expired"));

            return View(Utilities.CorrectViewPath("Shared/_SyncConstContactList"), model);
        }


    }


















}