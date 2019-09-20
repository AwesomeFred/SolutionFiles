using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.DallasArt;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.Themes.PAA.ViewModels;


namespace Nop.Web.DallasArt.Services
{
   public class EnrollmentService : IEnrollmentRequestService
    {
  
        private readonly IRepository<EnrollmentLocation> _enrollmentRequestRepository;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;

        public EnrollmentService(
                                   IRepository<EnrollmentLocation> enrollmentRequestRepository, 
                                   IWorkContext workContext,
                                   IStoreContext storeContext


            )
        {
           this._enrollmentRequestRepository = enrollmentRequestRepository;
           this._workContext = workContext;
           this._storeContext = storeContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        public void CreateEnrollmentRecord(EnrollmentLocation record)
        {
               _enrollmentRequestRepository.Insert(record);
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="record"></param>
        public void UpdateEnrollmentRecord(EnrollmentLocation record)
        {
            _enrollmentRequestRepository.Update(record);
        }

       
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public virtual IList<EnrollmentLocation> GetAllEnrollmentLocations()
       {
           return (from e in _enrollmentRequestRepository.Table.Include(x=>x.EnrollmentPayments) 
                   orderby e.OriginalEnrollmentDate
                   select e).ToList();
       }

       public virtual EnrollmentLocation GetAllEnrollmentLocations(string email )
       {
           if (string.IsNullOrWhiteSpace(email)) return null;

           return (from e in _enrollmentRequestRepository.Table
                   orderby e.OriginalCustomerId
                   select e).FirstOrDefault();
       }

       public virtual EnrollmentLocation GetEnrollmentLocationByGroupId(string groupId)
       {
           if (string.IsNullOrWhiteSpace(groupId)) return null;

           return (from e in _enrollmentRequestRepository.Table
               where e.CenterGroupId  == groupId  //  .ToLower().Replace(" ","_").Trim()   
               select e).FirstOrDefault();
       }

       public virtual EnrollmentLocation GetEnrollmentLocationById(int id)
       {
           if (id == 0) return null;

           return (from e in _enrollmentRequestRepository.Table.Include(e=>e.EnrollmentPayments)
                   where e.EnrollmentLocationId ==  id   
                   select e).FirstOrDefault();
       }

       public virtual EnrollmentLocation GetEnrollmentLocationByOriginalCustomerId(int originalCustomerId)
       {
          
           return (from e in _enrollmentRequestRepository.Table
                   where e.OriginalCustomerId == originalCustomerId  
                   select e).FirstOrDefault();
       }

       public virtual IList<string> GetAllEnrollmentLocationCenterIds(bool lowercase )
       {
           return (from e in _enrollmentRequestRepository.Table
                   orderby e.OriginalCustomerId
                   select lowercase ? e.CenterGroupId.ToLower() : e.CenterGroupId).ToList();
       }

       public virtual IList<string> GetAllEnrollmentLocationCenterNames(bool lowercase )
       {
           return (from e in _enrollmentRequestRepository.Table
                   orderby e.OriginalCustomerId
                   select    lowercase ? e.CenterName.ToLower() : e.CenterName  ).ToList();
       }
 
       public virtual bool CenterIsEnrolled(string centerName)
       {
           
           return GetAllEnrollmentLocationCenterNames(true)
               .Contains(centerName.ToLower().Trim().Replace(" ", "_"));
       }

        private string GenerateGroupId(int id)
        {
            return $"{Utilities.TruncateLongString(_storeContext.CurrentStore.Name, 2).ToUpper()}-{id}";
        }

        public virtual EnrollmentLocation CreateEnrollmentRecordFromModel(RegistrationViewModel model)
        {
            // Generate new location
            var lc = new EnrollmentLocation();


            lc.CenterGroupId = GenerateGroupId(model.CurrentCustomer.Id);
            lc.OriginalCustomerId = model.CurrentCustomer.Id;
          //  lc.CenterName = model.CenterNameRequest.Trim().Replace(" ", "_");
            lc.OriginalEnrollmentDate = DateTime.Now;
            lc.OriginalCoordinator = model.SelfIdentificationViewModel.PrayerCoordinatorEmail;
            lc.RenewalDate = DateTime.Now.Add(TimeSpan.FromDays(365));
     

            try
            {
               CreateEnrollmentRecord(lc);
            }
            catch (Exception)
            {
                // create log event here!          
                throw new Exception("Enrollment Persistence Failed!");
            }
        
            return lc;
        }

    }
}
