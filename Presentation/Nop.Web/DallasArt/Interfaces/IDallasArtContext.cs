using System.Web.Mvc;
using Nop.Core.Domain.DallasArt;
using Nop.Web.DallasArt.ViewModels;


namespace Nop.Web.DallasArt.Interfaces
{
    public interface IDallasArtContext
    {
      bool NeedMembershipStatus { get; set; }
      bool NeedsPhone { get; set; }
      bool NeedsEmail { get; set; }
      bool NeedsPassword { get; set; }
      bool NeedsOption { get; set; }
      bool IsContact { get; set; }
      bool IsRegistration { get; set; }
      bool IsEnrollment { get; set; }
      bool IsPresetEnrollment { get; set; }
      bool IsFreeRegistration { get; set; }
      bool IsPresetRegistration { get; set; }
      ContactRequestViewModel ContactRequest { get; set; }
      SelfIdentificationViewModel SelfIdentification { get; set; }
        MemberEnrollment Location { get; set; }
      SelectListItem SelectedCategoryItem { get; set; }
    }
}
