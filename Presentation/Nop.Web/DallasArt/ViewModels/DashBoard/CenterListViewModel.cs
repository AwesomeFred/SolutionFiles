using System.Collections.Generic;
using System.Linq;
using Nop.Core.Domain.DallasArt;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.ViewModels.Customer;
 

namespace Nop.Web.DallasArt.ViewModels.DashBoard
{
    public class CenterListViewModel
    {
        private readonly IEnrollmentRequestService _enrollmentRequestService;
  

        public CenterListViewModel( 
            IEnrollmentRequestService enrollmentRequestService
         )
        {
            _enrollmentRequestService = enrollmentRequestService;
  
       
        }
 

        public IEnumerable<CustomerListViewModel.Center> Centers
        {
            get
            {
               IList<EnrollmentLocation>  centers 
                 = _enrollmentRequestService.GetAllEnrollmentLocations();


                IEnumerable<CustomerListViewModel.Center> centerList
                  = centers.OrderBy(o=>o.CenterName).Select(c => new CustomerListViewModel.Center

                    {
                        CenterGroupId = c.CenterGroupId,
                        CenterName = string.Format("{0}  ({1})", 
                        c.CenterName.Replace("_", " "), 
                        c.CenterGroupId)

                    });

                return centerList;
            }
        }
    }
}