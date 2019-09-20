using System.Collections.Generic;
using Nop.Core.Domain.DallasArt;
using Nop.Web.Themes.PAA.ViewModels;


namespace Nop.Web.DallasArt.Interfaces
{
    public interface IEnrollmentRequestService
    {
        void CreateEnrollmentRecord(MemberEnrollment record);       

        IList<MemberEnrollment> GetAllEnrollmentLocations();
        
        MemberEnrollment GetAllEnrollmentLocations(string email);
        
        IList<string> GetAllEnrollmentLocationCenterIds(bool lowercase);
        
        IList<string> GetAllEnrollmentLocationCenterNames( bool lowercase);

        MemberEnrollment GetEnrollmentLocationByGroupId(string groupId);
    
        bool CenterIsEnrolled(string centerName);

        MemberEnrollment GetEnrollmentLocationByOriginalCustomerId(int originalCustomerId);

        void UpdateEnrollmentRecord(MemberEnrollment record);

        MemberEnrollment GetEnrollmentLocationById(int id);

        MemberEnrollment CreateEnrollmentRecordFromModel(RegistrationViewModel model);


    }
}
