using System.Collections.Generic;
using static Nop.Web.DallasArt.Services.ConstantContactService;

namespace Nop.Web.Themes.PAA.ViewModels
{
    public class ConstantContactViewModel
    {
        public IEnumerable<ConstantContactMember> ActiveMembers { get; set; }
        public IEnumerable<ConstantContactMember> ExpiringMembers { get; set; }
        public IEnumerable<ConstantContactMember> ExpiredMembers { get; set; }
        public IEnumerable<ConstantContactMember> InactiveMembers { get; set; }

    }
}