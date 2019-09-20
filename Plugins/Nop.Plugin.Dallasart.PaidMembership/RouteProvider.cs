using System.Web.Mvc;
using System.Web.Routing;
using Nop.Plugin.DallasArt.PaidMembership.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.DallasArt.PaidMembership
{
    public class RouteProvider : IRouteProvider
    {
      

      

        public void RegisterRoutes(RouteCollection routes)
        {
              ViewEngines.Engines.Insert(0, new CustomViewEngine());

            routes.MapRoute("Plugin.Dallasart.PaidMembership",
                "Paid/{action}/{id}",
                new { controller = "PaidRegistion", action = "Index",  id = UrlParameter.Optional },
               new[] { "Plugin.DallasArt.PaidMembership.Controllers" });




        }

        public int Priority =>10;
    }
}
