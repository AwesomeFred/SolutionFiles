using Nop.Web.Framework.Themes;

namespace Nop.Web.DallasArt.Infrastructure.CustomViewEngines
{
    public class DallasArtCustomViewEngine : ThemeableRazorViewEngine
    {


        public DallasArtCustomViewEngine()
        {
            PartialViewLocationFormats = new[]
            {
                   "~/Themes/PAA/Views/SitePages/{0}.cshtml",
                   "~/Themes/PAA/Views/Register/{0}.cshtml"


            };

            ViewLocationFormats = new[]
            {
                    "~/Themes/PAA/Views/SitePages/{0}.cshtml",
                   "~/Themes/PAA/Views/Register/{0}.cshtml"


            };

        }
    }
}