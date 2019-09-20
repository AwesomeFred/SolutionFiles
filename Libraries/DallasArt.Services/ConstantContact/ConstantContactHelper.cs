using System.Collections.Generic;
using System.Threading.Tasks;
using DallasArt.Services.ConstantContact.Classes;
using InContactSdk.BulkActivities;
using InContactSdk.ContactLists;
using InContactSdk.Models;

namespace DallasArt.Service.ConstantContact
{
    public class ConstantContactHelper
    {
        //https://bitbucket.org/scottlance/incontact/wiki/Home


        private const string ApiKey = "xjffaje77c3nn3cut56hjcdj";
        private const string ApiToken = "255ae9f4-3e0b-4d72-bfaa-c700b91e90d7";

        private static readonly InContactSettings Settings = new InContactSettings(ApiKey, ApiToken);

        public static ContactListsService ListsService()
        {
            return new ContactListsService(Settings);
        }





        public static BulkActivitiesService BulkListsService()
        {
            return new BulkActivitiesService(Settings);

        }


        public static async Task<IEnumerable<ContactList>> GetAllLists()
        {
            ContactListsService service = ListsService();
            return await service.GetContactListsAsync(null);
        }


        public static async Task<IEnumerable<ContactList>> GetAllConstantContactsLists()
        {
            ContactListsService service = ListsService();
            return await service.GetContactListsAsync(null);
        }



        public static async Task<string> GetContactListById(string name)
        {
            string rtnVal = string.Empty;
            Task<IEnumerable<ContactList>> t = GetAllConstantContactsLists();

            await t;

            foreach (var list in t.Result)
            {
                if (list.Name != name) continue;
                return list.Id;
            }

            return rtnVal;
        }



        public static async Task<BulkActivityResponse> ClearConstantContactListByName(string listName)
        {
            string listId = await GetContactListById(listName);
            var work = new ContactLists {Lists = new string[] {listId}};

            var bulkActivitiesService = BulkListsService();
            BulkActivityResponse response = await bulkActivitiesService.ClearContactListsAsync(work);

            return response;
        }


    }
}






































//public class ConstantContact
        //{
        //    private const string ApiKey = "xjffaje77c3nn3cut56hjcdj";
        //    private const string ApiToken = "255ae9f4-3e0b-4d72-bfaa-c700b91e90d7";

        //    private static readonly InContactSettings Settings = new InContactSettings(ApiKey, ApiToken);



        //    public static ContactListsService ListsService()
        //    {
        //        return new ContactListsService(Settings);
        //    }



        //    public static async Task<IEnumerable<ContactList>> GetAllLists()
        //    {
        //        ContactListsService service = ListsService();
        //        return await service.GetContactListsAsync(null);
        //    }


        //    public static async Task<string> GetContactListById(string name)
        //    {
        //        string rtnVal = string.Empty;

        //        ContactListsService service = ListsService();


        //        IEnumerable<ContactList> allList =
        //                await service.GetContactListsAsync(null);


        //        foreach (var list in allList)
        //        {
        //            if (list.Name != name) continue;

        //            return list.Id;
        //        }

        //        return rtnVal;
        //    }



        //    public static string MakeHeader(string name, Enum contactListStatus)
        //    {
        //        string rtnval = String.Empty;

        //        return rtnval;
        //    }


        //    public static string MakeColumnList(string name, List<string> columns)
        //    {
        //        string rtnval = String.Empty;
        //        return rtnval;
        //    }


        //    public static string MakeMemberList(string name, List<string> columns)
        //    {
        //        string rtnval = String.Empty;
        //        return rtnval;
        //    }

        //    /*

        //    public class MyDate
        //    {
        //        public int year;
        //        public int month;
        //        public int day;
        //    }

        //    public class Lad
        //    {
        //        public string firstName;
        //        public string lastName;
        //        public MyDate dateOfBirth;
        //    }

        //    class Program
        //    {
        //        static void Main()
        //        {
        //            var obj = new Lad
        //            {
        //                firstName = "Markoff",
        //                lastName = "Chaney",
        //                dateOfBirth = new MyDate
        //                {
        //                    year = 1901,
        //                    month = 4,
        //                    day = 30
        //                }
        //            };
        //            var json = new JavaScriptSerializer().Serialize(obj);
        //            Console.WriteLine(json);
        //        }
        //    }










        //     */



















        //}
 //   }
