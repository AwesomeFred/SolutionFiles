using Nop.Web.Framework.Themes;

namespace Nop.Web.Themes.PAA.Infrastructure.CustomViewEngines
{
    public class PaaCustomViewEngine : ThemeableRazorViewEngine
    {

        public PaaCustomViewEngine()
        {
            PartialViewLocationFormats = new[]
            {
                   "~/Themes/PAA/Views/SitePages/{0}.cshtml",
                   "~/Themes/PAA/Views/Upload/{0}.cshtml",
                    "~/Themes/PAA/Views/Register/{0}.cshtml",
                    "~/Themes/PAA/Views/ImageSubmission/{0}.cshtml"


            };

            ViewLocationFormats = new[]
            {
                    "~/Themes/PAA/Views/SitePages/{0}.cshtml",
                    "~/Themes/PAA/Views/Upload/{0}.cshtml",
                    "~/Themes/PAA/Views/Register/{0}.cshtml",
                    "~/Themes/PAA/Views/ImageSubmission/{0}.cshtml"

            };

        }
    }
}