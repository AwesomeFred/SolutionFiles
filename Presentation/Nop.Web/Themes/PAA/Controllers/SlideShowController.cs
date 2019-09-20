using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Web.Themes.PAA.Classes;

namespace Nop.Web.Themes.PAA.Controllers
{
    public class SlideShowController : Controller
    {


        public IEnumerable<ArtWork> IndexFaiding()
        {

            List<ArtWork> artworks = null ;




            artworks.Add(new ArtWork { });







            return(artworks);
        }



        // GET: SlideShow
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult FadingShow()
        {

          

            return View ();
        }


    }
}