using System.Web.Mvc;
using Nop.Core.Domain.DallasArt;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.ViewModels;


namespace Nop.Web.DallasArt.Context
{
    public class DallasArtContext : IDallasArtContext
    {
        public DallasArtContext()
        {
            NeedMembershipStatus = false;
            IsEnrollment = false;
            IsRegistration = false;
            IsContact = false;
            IsPresetEnrollment = false;
            IsPresetRegistration = false;
            IsFreeRegistration = false;
            NeedsOption = true;
            NeedsPhone = true;
            NeedsEmail = true;
            NeedsPassword = false;
            ContactRequest = new ContactRequestViewModel();
            Location = new MemberEnrollment();
            SelfIdentification = new SelfIdentificationViewModel();
        }


        public bool NeedMembershipStatus { get; set; }
        public bool NeedsPhone { get; set; }
        public bool NeedsEmail { get; set; }
        public bool NeedsPassword { get; set; }
        public bool NeedsOption { get; set; }
        public bool IsContact { get; set; }
        public bool IsRegistration { get; set; }
        public bool IsEnrollment { get; set; }
        public bool IsPresetEnrollment { get; set; }
        public bool IsFreeRegistration { get; set; }
        public bool IsPresetRegistration { get; set; }
        public ContactRequestViewModel ContactRequest { get; set; }

        public SelfIdentificationViewModel SelfIdentification { get; set; }

        public MemberEnrollment Location { get; set; }
        public SelectListItem SelectedCategoryItem { get; set; }
    }
}
