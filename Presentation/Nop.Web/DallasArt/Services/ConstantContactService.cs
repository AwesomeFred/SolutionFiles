using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InContactSdk.BulkActivities;
using InContactSdk.ContactLists;
using InContactSdk.Helpers;
using Newtonsoft.Json;
using Nop.Web.DallasArt.Interfaces;

using DallasArt.Classes.ConstantContact;
using DallasArt.Service.ConstantContact;
using Nop.Web.DallasArt.Models;
using InContactSdk.Contacts;
using DallasArt.Models.V_3_9.Models;

namespace Nop.Web.DallasArt.Services
{
    public class ConstantContactService  : IConstantContactService
    {

        public class ConstantContactModel
        {


            public string ListName { get; set; }
            public string ListId { get; set; }
            public ContactListsStatus Status { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }

            public int ContactCount { get; set; }

        }

        public class ConstantContactBulkResponse
        {

            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("contact_count")]
            public int ContactCount { get; set; }
            [JsonProperty("error_count")]
            public int ErrorCount { get; set; }
        }


        public class ConstantContactMember
        {
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Expired { get; set; }
            public DateTime RenewalDate { get; set; }
            public string MembershipType { get; set; }
            public bool NonPayingMember { get; set; }

        }


        public ConstantContactService()
        {
        }



       public async Task<ContactList>FindExistingContactList(string contactListName)
        {
            
            IEnumerable<ContactList> allLists = await ConstantContact.GetAllLists();
            return (from cx in allLists where cx.Name == contactListName select cx ).FirstOrDefault()  ;  
        }

        public static async Task<ContactList> GetListByListName(string name, bool create = false)
        {
            
            IEnumerable<ContactList> allLists = await ConstantContact.GetAllLists();

            var rtnval = (from list in allLists
                          where string.Equals(list.Name.ToLower(), name.ToLower(), StringComparison.Ordinal)
                          select list).FirstOrDefault();


            if (rtnval == null && create)
            {
               ContactList newList = new ContactList { Name = name, Status = ContactListsStatus.Hidden };
                ContactListsService service = ConstantContactHelper.ListsService();
                ContactList y   = await service.CreateContactListAsync(newList);

                return y;
            }
             
            return rtnval;
         }

        private static async Task<List<ConstantContactModel>> Create(List<string> ccLists)
        {

            List<ConstantContactModel> model = new List<ConstantContactModel>();

            IEnumerable<ContactList>  allLists =  await   ConstantContact.GetAllLists(); 


            foreach (string ccList in ccLists)
            {

                ContactList existingList = IdentifyList(ccList);

                // found in constant contact ?
                if (existingList == null)

                {
                    // no create it !!   
                    var newList = new ContactList { Name = ccList, Status = ContactListsStatus.Hidden };
                    ContactListsService service = ConstantContact.ListsService();
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


            //foreach ( IEnumerable<ContactList> list in   GetAllLists()  )
            //{
            //    if (list.Name != listName) continue;
            //    return list;
            //}
             return null;
        }

        private static IEnumerable<object> GetAllLists()
        {
            throw new NotImplementedException();
        }

        private static readonly List<ExportColumnName> _exportColumnNames = new List<ExportColumnName>
        {
            ExportColumnName.Email,
            ExportColumnName.FirstName,
            ExportColumnName.LastName,
            ExportColumnName.CustomField2
        };

        private static readonly List<ImportColumnName> _importColumnNames = new List<ImportColumnName>
        {
            ImportColumnName.FirstName,
            ImportColumnName.LastName,
            ImportColumnName.Email,
            ImportColumnName.CustomField2
        };



        private static ContactImport BuildContactImport(IList<PaaMember> members, List<string> listIds)
        {
            ContactImport import = new ContactImport
            {
                ImportData = BuildMemberImportData(members),
                Lists = listIds,
                ColumnNames = _importColumnNames
            };
            return import;
        }

        // convert members to import data
        private static  IList<ImportData> BuildMemberImportData(IList<PaaMember> members)
        {
            List<ImportData> importData = new List<ImportData>();

            foreach (PaaMember m in members)
            {
                importData.Add(new ImportData
                    {
                        EmailAddresses = new List<string> { m.Email },
                        FirstName = m.   FirstName,
                        LastName = m.LastName,
                        CustomFields = new List<CustomField>
                        {
                            new CustomField
                            {
                                Name =     CustomFieldValue.CustomField2,
                                Value = m.Renewal.ToShortDateString(),
                            }
                        }
                }

                );
            }
            return importData;
        }



        #region BulkResponse


        private static async Task<ConstantContactBulkResponse> ClearConstantContactList(string listId)
        {
            
            var work = new ContactLists { Lists = new string[] { listId } };

            var bulkActivitiesService = ConstantContact.BulkListsService();
            BulkActivityResponse response = await bulkActivitiesService.ClearContactListsAsync(work);

            return new ConstantContactBulkResponse
            {
                Id = response.Id,
                ContactCount =  int.Parse(response.ContactCount),
                ErrorCount =   int.Parse(response.ErrorCount)
            };
 
        }




        /// <summary>
        /// Create or Update A Constant Contact List 
        /// </summary>
        /// <param name="members">List to Export </param>
        /// <param name="listName">Name Of List</param>
        /// <returns></returns>

        public async Task<ServiceResponse> ExportConstantContactList(IList<PaaMember> members, string listName)
        {
            // find existing list or create new one
            ContactList requestedList = await ConstantContactService.GetListByListName(listName, true);
            
            // build the export structure
            ContactImport import = BuildContactImport(members, new List<string> { requestedList.Id });

        
            var bulkActivitiesService = ConstantContact.BulkListsService();

            try
            {
                // clear any existing list
                ConstantContactBulkResponse clearResponse = await ClearConstantContactList(requestedList.Id);


                if (import.ImportData.Count() < 1)  // nothing to add
                {
                    return new ServiceResponse
                    {

                        Name = listName,
                        Success = true,
                        SuccessCount = 0,
                        ErrorCount = 0 ,
                        Message = "Success"
                    };

                }

                else
                {
                    BulkActivityResponse response = await bulkActivitiesService.ImportContactsAsync(import);

                    return new ServiceResponse
                    {

                        Name = listName,
                        Success = true,
                        SuccessCount = int.Parse(response.ContactCount),
                        ErrorCount = int.Parse(response.ErrorCount),
                        Message = "Success"
                    };
                }
            }

            catch(Exception ex)
            {
                return new ServiceResponse
                {
                    Name = listName, 
                    Success = false,
                    Message = "Failed",
                    ExceptionMessage = ex.Message
                };

            }

        }


        #endregion



        public  async Task<ConstantContactModel> CreateAConstantContactList(string listName)
        {
            List<string> names = new List<string>() { listName };
            List<ConstantContactModel> returnedList = await ConstantContactService.Create(names);
            return returnedList.FirstOrDefault();
        }


        public async Task<ConstantContactBulkResponse> ClearConstantContactList(ContactList currentList)
        {
            return await ClearConstantContactList(currentList.Id );
        }

      

    }
}
