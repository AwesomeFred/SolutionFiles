using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InContactSdk.BulkActivities;
using InContactSdk.ContactLists;
using InContactSdk.Models;

namespace DallasArt.Services.ConstantContact.Classes
{
    public class ExtContactList 
    {

         

        public string id { get; set; }
        public string list_id { get; set; }
        public int contact_count { get; set;  } 
        public string created_date { get; set; }
        public string modified_date { get; set; }
        public string name { get; set; }
        public string status { get; set; }

    }











}
