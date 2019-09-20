using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Profile;
using System.Web.Routing;

namespace Nop.Web.DallasArt.Interfaces
{
    public interface IBaseController
    {
        ActionResult PageNotFound();

        /// <summary>
        /// Access denied view
        /// </summary>
        /// <returns>Access denied view</returns>
        ActionResult AccessDeniedView();

        void Dispose();
        IDependencyResolver Resolver { get; set; }
        AsyncManager AsyncManager { get; }
        IActionInvoker ActionInvoker { get; set; }
        HttpContextBase HttpContext { get; }
        ModelStateDictionary ModelState { get; }
        ProfileBase Profile { get; }
        HttpRequestBase Request { get; }
        HttpResponseBase Response { get; }
        RouteData RouteData { get; }
        HttpServerUtilityBase Server { get; }
        HttpSessionStateBase Session { get; }
        ITempDataProvider TempDataProvider { get; set; }
        UrlHelper Url { get; set; }
        IPrincipal User { get; }
        ViewEngineCollection ViewEngineCollection { get; set; }
        ControllerContext ControllerContext { get; set; }
        TempDataDictionary TempData { get; set; }
        bool ValidateRequest { get; set; }
        IValueProvider ValueProvider { get; set; }
        dynamic ViewBag { get; }
        ViewDataDictionary ViewData { get; set; }
    }

}