using System.Collections.Generic;
using Nop.Core.Domain.Customers;


namespace Nop.Web.DallasArt.Interfaces
{
    public interface ICustomerRoleService
    {

     
    
      //  List<Core.Domain.Customers.Customer>GetCustomersInRole(string systemName);
    
        bool SetRollUnique(Core.Domain.Customers.Customer customer, CustomerRole role);

      //  Dictionary<string, IEnumerable<int>> GetCurrentCenterMembersWithRoles(string centerId, string centerName);

        void SetRole(Core.Domain.Customers.Customer customer, CustomerRole role);
    }
}