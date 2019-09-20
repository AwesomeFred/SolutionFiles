using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;
using Nop.Web.Themes.PAA.Infrastructure.CustomViewEngines;

namespace Nop.Web.Themes.PAA
{
    public class RouteProvider : IRouteProvider
    {


        public void RegisterRoutes(RouteCollection routes)
        {


            // Theme Path Custom View Paths   

            ViewEngines.Engines.Insert(0, new PaaCustomViewEngine());
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                "DallasArt.StaffMember.Index",
                 "StaffMemberList/{id}",
                 new { controller = "StaffMemberList", action = "Index", id = UrlParameter.Optional },
                 new[] { "Nop.Web.DallasArt.Controllers" }
                );


         


            routes.MapRoute(
                "DallasArt.ConstantContact.Index",
                 "ConstantContact/{action}/{id}",
                 new { controller = "ConstantContact",  id = UrlParameter.Optional },
                 new[] { "Nop.Web.DallasArt.Controllers" }
                );


            routes.MapRoute(
                "DallasArt.OldConstantContact.Index",
                 "ConTest/{id}",
                 new { controller = "OldConstantContact", action = "Index", id = UrlParameter.Optional },
                 new[] { "Nop.Web.DallasArt.Controllers" }
                );



            //////////////////////////////////////////////////////////////////////

            // entry 
            routes.MapRoute(
             "Enter.Instructions",
              "Submissions/Instructions",
              new { controller = "ArtworkEntry", action = "Instructions", id = UrlParameter.Optional },
              new[] { "Nop.Web.Themes.PAA.Controllers" }
                );

            //  entry
            routes.MapRoute(
              "Enter.Register",
               "Submissions/Register",
              new { controller = "ArtworkEntry", action = "Register", id = UrlParameter.Optional },
               new[] { "Nop.Web.Themes.PAA.Controllers" }
               );

            ////////////////////////////////////////////////////////////////////////////////

            //ExpiredBy45Days

            //routes.MapRoute(
            //    "DallasArt.FullMember.Expired4MoreThen45",
            //    "2018ExpiredMemberships1013/{id}",
            //    new { controller = "CustomerList", action = "ExpiredBy45Days", id = UrlParameter.Optional },
            //    new[] { "Nop.Web.Themes.PAA.Controllers" }
            //);




            //routes.MapRoute(
            //    "DallasArt.FullMember.NewThisYear",
            //    "2018NewMemberships1013/{id}",
            //    new { controller = "CustomerList", action = "NewThisYear", id = UrlParameter.Optional },
            //    new[] { "Nop.Web.Themes.PAA.Controllers" }
            //);



            //routes.MapRoute(
            //   "DallasArt.FullMember.List",
            //   "ExpiredMemberShips1013/{id}",
            //   new { controller = "CustomerList", action = "Expired", id = UrlParameter.Optional },
            //   new[] { "Nop.Web.Themes.PAA.Controllers" }
            //    );


            //routes.MapRoute(
            //    "Dallasart.BulkLoader",
            //    "LoadMembers/Members/{id}",
            //    new { controller = "BulkLoader", action = "GetMembers", id = UrlParameter.Optional }, 
            //    new[] { "Nop.Web.Themes.PAA.Controllers" }

            //    );

            // ==============================================================================


            // review
            routes.MapRoute(
                "Staff.ImageReview1013",
                "Staff/ImageReview1013/{id}",
                new { controller = "ImageReview", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Web.Themes.PAA.Controllers" }
            );

// ==============================================================================


            routes.MapRoute(
                "Site.Happenings",
                "Happenings/{id}",
                new { controller = "Site", action = "Happenings", id = UrlParameter.Optional },
                new[] { "Nop.Web.Themes.PAA.Controllers" }
            );

            

            routes.MapRoute(
                "Site.MembersListing",
                "MembersListing/{id}",
                new { controller = "Site", action = "MembersListing", id = UrlParameter.Optional },
                new[] { "Nop.Web.Themes.PAA.Controllers" }
            );


            routes.MapRoute(
               "Site.About",
               "About/{id}",
                 new { controller = "Site", action = "About", id = UrlParameter.Optional },
                 new[] { "Nop.Web.Themes.PAA.Controllers" }
            );



            routes.MapRoute(
              "Site.Board",
              "Board/{id}",
                new { controller = "Site", action = "Board", id = UrlParameter.Optional },
                new[] { "Nop.Web.Themes.PAA.Controllers" }
           );

            routes.MapRoute(
              "Site.Bylaws",
              "ByLaws/{id}",
                new { controller = "Site", action = "ByLaws", id = UrlParameter.Optional },
                new[] { "Nop.Web.Themes.PAA.Controllers" }
           );

            routes.MapRoute(
              "Site.Contact",
              "Contact/{id}",
                new { controller = "Contact", action = "CustomerContact", id = UrlParameter.Optional },
                new[] { "Nop.Web.Themes.PAA.Controllers" }
           );


            routes.MapRoute(
                "Site.ConstantContact",
                "ConstantContact1013/{action}/{id}",
                new { controller = "ConstantContact", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Web.Themes.PAA.Controllers" }
            );





            routes.MapRoute(
                "Site.Join",
                "Join/{id}",
                new { controller = "Site", action = "Join", id = UrlParameter.Optional },
                new[] { "Nop.Web.Themes.PAA.Controllers" }
            );





            routes.MapRoute(
                "Site.PaidMembership",
                "PaidMembership/{id}",
                new { controller = "PaidMembership", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Web.Themes.PAA.Controllers" }
            );




            routes.MapRoute(
              "Site.Donate",
              "Donate/{id}",
                new { controller = "Site", action = "Donate", id = UrlParameter.Optional },
                new[] { "Nop.Web.Themes.PAA.Controllers" }
           );






            routes.MapRoute(
                "Site.Benefits",
                "Benefits/{id}",
                new { controller = "Site", action = "Benefits", id = UrlParameter.Optional },
                new[] { "Nop.Web.Themes.PAA.Controllers" }
            );




            routes.MapRoute(
               "Site.Partners",
               "Partners/{id}",
                 new { controller = "Site", action = "Partners", id = UrlParameter.Optional },
                 new[] { "Nop.Web.Themes.PAA.Controllers" }
            );


          


            routes.MapRoute(
               "Site.Sponsors",
               "Sponsors/{id}",
                 new { controller = "Site", action = "Sponsors", id = UrlParameter.Optional },
                 new[] { "Nop.Web.Themes.PAA.Controllers." }
            );


            routes.MapRoute(
                "Site.News",
                "NewsLetters/{id}",
                    new { controller = "Site", action = "NewsLetters", id = UrlParameter.Optional },
                    new[] { "Nop.Web.Themes.PAA.Controllers." }
                );

            routes.MapRoute(
            "Site.Events",
            "Events/{id}",
                new { controller = "Site", action = "Events", id = UrlParameter.Optional },
                new[] { "Nop.Web.Themes.PAA.Controllers." }
            );

        }

        public int Priority => 500;
    }
}