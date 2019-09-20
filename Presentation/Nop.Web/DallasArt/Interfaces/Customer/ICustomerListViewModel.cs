using System.Collections.Generic;
using Nop.Admin.Models.Customers;


namespace Nop.Web.DallasArt.Interfaces.Customer
{
    public interface ICustomerListViewModel
    {

         bool ExecutiveVieu { get; set; }

        string CenterName { get; set; }
        
        string CenterGroupId { get; set; }
    
        bool IsPrimaryCenter { get; set; }


         IEnumerable<ICenter> Centers { get; set; }

         string SelectedCenter { get; set; }

          Core.Domain.Customers.Customer Customer { get; set; }
          bool ActiveCkd { get; set; }
          bool DirectorCkd { get; set; }
          bool AssistantDirectorCkd { get; set; }
          bool PrayerCoordinatorCkd { get; set; }
          bool AssistantPrayerCoordinatorCkd { get; set; }

           CustomerListModel CustomerList { get; set; }
           IList<Core.Domain.Customers.Customer> RegisteredCustomers { get; set; }
           IList<int> PrayerCoordinators { get; set; }
           IList<int> AssistantPrayerCoordinators { get; set; }
           IList<int> Director { get; set; }
           IList<int> AssistantDirectors { get; set; }
           string Message { get; set; }

    }
}