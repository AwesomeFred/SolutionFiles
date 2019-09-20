using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core;
using Nop.Web.DallasArt.ViewModels;
using Nop.Web.Framework.Controllers;

namespace Nop.Web.DallasArt.Controllers.Customer
{
    public class FadingSlideShowController : BaseController
    {
        private readonly IStoreContext _storeContext;

        public FadingSlideShowController(
                                   IStoreContext storeContext
                                 )
        {
            _storeContext = storeContext;
        }

        private string ContactViewPath(string path)
        {
            return $"~/Themes/{_storeContext.CurrentStore.Name}/Views/{path}.cshtml";
        }

 
       [ChildActionOnly]
        public ActionResult FadingShow()
       {
            List<FadingSlide> model = FadingSlides.FadingSlidesList();

            // randomize order
           foreach (var item in model) {item.Guid = Guid.NewGuid(); }
           model = model.OrderBy(x => x.Guid).ToList();


            return PartialView(ContactViewPath("Home/Fader"), model);
        }
    }
}