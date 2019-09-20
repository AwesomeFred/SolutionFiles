
using System.Web.Mvc;

namespace Nop.Web.DallasArt.Interfaces
{
    public interface ICustomViewEngine
    {
        ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache);
        ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache);
        void ReleaseView(ControllerContext controllerContext, IView view);
        string[] AreaMasterLocationFormats { get; set; }
        string[] AreaPartialViewLocationFormats { get; set; }
        string[] AreaViewLocationFormats { get; set; }
        string[] FileExtensions { get; set; }
        string[] MasterLocationFormats { get; set; }
        string[] PartialViewLocationFormats { get; set; }
        IViewLocationCache ViewLocationCache { get; set; }
        string[] ViewLocationFormats { get; set; }
    }
}