using Nop.Web.Framework.Themes;

namespace Nop.Plugin.DallasArt.PaidMembership.Infrastructure
{
    class CustomViewEngine   : ThemeableRazorViewEngine
    {

        public CustomViewEngine()
        {
            ViewLocationFormats = new[] { "~/Themes/PAA/views/Customer/{0}.cshtml" };
            
            PartialViewLocationFormats = new[] { "~/Themes/PAA/views/Customer/{0}.cshtml" };
        }

    }
}
