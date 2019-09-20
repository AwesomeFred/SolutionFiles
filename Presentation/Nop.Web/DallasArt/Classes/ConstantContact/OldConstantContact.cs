using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InContactSdk.BulkActivities;
using InContactSdk.ContactLists;
using InContactSdk.Contacts;
using InContactSdk.Helpers;
using Newtonsoft.Json;
using DallasArt.Service.ConstantContact;
using static Nop.Web.DallasArt.Services.ConstantContactService;

namespace DallasArt.Classes
{

    public class ConstantContactModelOld
    {


        public string ListName { get; set; }
        public string ListId { get; set; }
        public ContactListsStatus Status { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }

        public int ContactCount { get; set; }

    }

    public class ConstantContactBulkResponseOld
    {


        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("contact_count")]
        public int ContactCount { get; set; }
        [JsonProperty("error_count")]
        public int ErrorCount { get; set; }


    }

    public class ConstantContactMemberOld
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Expired { get; set; }
        public DateTime RenewalDate { get; set; }
        public string MembershipType { get; set; }
        public bool NonPayingMember { get; set; }

    }

    public class OldConstantContact
    {
        public static List<ContactList> AllContactLists = null;

        public OldConstantContact()
        {
        }




        private async Task<IEnumerable<ContactList>> Initialize()
        {
               return await   ConstantContactHelper.GetAllLists();
        }


        public static async Task<List<ConstantContactModel>> Create(List<string> ccLists)
        {

            List<ConstantContactModel> model = new List<ConstantContactModel>();


            var myClass = new OldConstantContact();
            var t = await myClass.Initialize();
            AllContactLists = t.ToList();

            foreach (string ccList in ccLists)
            {

                ContactList existingList = IdentifyList(ccList);

                // found in constant contact ?
                if (existingList == null)

                {
                    // no create it !!   
                    var newList = new ContactList { Name = ccList, Status = ContactListsStatus.Hidden };
                    ContactListsService service =    ConstantContactHelper.ListsService() ;
                    existingList = await service.CreateContactListAsync(newList);
                }


                if (existingList != null)
                {
                    var l = new ConstantContactModel
                    {
                        ListName = existingList.Name,
                        ListId = existingList.Id,
                        Status = existingList.Status,
                        CreatedDate = existingList.CreatedDate,
                        ModifiedDate = existingList.ModifiedDate,
                        ContactCount = existingList.ContactCount
                    };

                    model.Add(l);
                }

            }

            return model;
        }

        private static ContactList IdentifyList(string listName)
        {
            foreach (var list in AllContactLists)
            {
                if (list.Name != listName) continue;
                return list;
            }
            return null;
        }

        private static readonly List<ExportColumnName> _exportColumnNames = new List<ExportColumnName>
        {
            ExportColumnName.Email,
            ExportColumnName.FirstName,
            ExportColumnName.LastName,
            ExportColumnName.CustomField5
        };

        private static readonly List<ImportColumnName> _importColumnNames = new List<ImportColumnName>
        {
            ImportColumnName.FirstName,
            ImportColumnName.LastName,
            ImportColumnName.Email,
            ImportColumnName.CustomField5
        };

        private static ContactImport BuildContactImport(List<ConstantContactMember> members, List<string> listNames)
        {
            ContactImport import = new ContactImport
            {
                ImportData = BuildMemberImportData(members),
                Lists = listNames,
                ColumnNames = _importColumnNames
            };
            return import;
        }

        private static List<ImportData> BuildMemberImportData(List<ConstantContactMember> members)
        {
            List<ImportData> importData = new List<ImportData>();

            foreach (ConstantContactMember m in members)
            {
                importData.Add(new ImportData
                {
                    EmailAddresses = new List<string> { m.Email },
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    CustomFields = new List<CustomField>
                        {
                            new CustomField
                            {
                                Name =     CustomFieldValue.CustomField2,
                                Value = m.Expired,

                            }
                        }
                }

                );

            }
            return importData;
        }



        #region BulkResponse


        public static async Task<ConstantContactBulkResponse> ClearConstantContactList(string listId)
        {

            var work = new ContactLists { Lists = new string[] { listId } };

            var bulkActivitiesService = ConstantContactHelper.BulkListsService();
            BulkActivityResponse response = await bulkActivitiesService.ClearContactListsAsync(work);

            return new ConstantContactBulkResponse
            {
                Id = response.Id,
                ContactCount = int.Parse(response.ContactCount),
                ErrorCount = int.Parse(response.ErrorCount)
            };

        }



        public static async Task<ConstantContactBulkResponse> ExportConstantContactList(List<ConstantContactMember> members, string listId)
        {
            var listNames = new List<string> { listId };

            ContactImport import = BuildContactImport(members, listNames);

            var bulkActivitiesService = ConstantContactHelper.BulkListsService();

            BulkActivityResponse response = await bulkActivitiesService.ImportContactsAsync(import);


            return new ConstantContactBulkResponse
            {
                Id = response.Id,
                ContactCount = int.Parse(response.ContactCount),
                ErrorCount = int.Parse(response.ErrorCount)
            };
        }

        #endregion


        //  public static async Task<Con>

        public static async Task<string> GetListIdByName(string name, bool create = false)
        {
            IEnumerable<ContactList> allLists =
                await ConstantContactHelper.GetAllConstantContactsLists();

            var rtnval = (from list in allLists
                          where string.Equals(list.Name.ToLower(), name.ToLower(), StringComparison.Ordinal)
                          select list.Id).FirstOrDefault();



            if (rtnval == null && create)
            {




            }


            return rtnval;

        }
    }
}
