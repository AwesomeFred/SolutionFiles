using System.Collections.Generic;
using Nop.Admin.Models.Customers;

namespace Nop.Web.DallasArt.ViewModels.Customer
{
    public class CustomerListViewModel
    {
       
        public bool  ExecutiveVieu { get; set; } 
        
        public string CenterName { get; set; }
        public string CenterGroupId { get; set; }
        public bool IsPrimaryCenter { get; set; }


        public class Center
        {
            public string CenterName { get; set; }
            public string CenterGroupId { get; set; }
        }


          public IEnumerable<Center> Centers { get; set; }

          public string SelectedCenter { get; set; }

        public Core.Domain.Customers.Customer Customer { get; set; }
        public bool  ActiveCkd  { get; set; }
        public bool  DirectorCkd   { get; set; }
        public bool  AssistantDirectorCkd  { get; set; }
        public bool  PrayerCoordinatorCkd  { get; set; }
        public bool  AssistantPrayerCoordinatorCkd { get; set; }
       
         

        
        public CustomerListModel CustomerList { get; set; }
        public IList<Core.Domain.Customers.Customer> RegisteredCustomers { get; set; }
        public IList<int> PrayerCoordinators { get; set; }
        public IList<int> AssistantPrayerCoordinators { get; set; }
        public IList<int> Director { get; set; }
        public IList<int> AssistantDirectors { get; set; }
        public string Message { get; set; }

         







    }
}