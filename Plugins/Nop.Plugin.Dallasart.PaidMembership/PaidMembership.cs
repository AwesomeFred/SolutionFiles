using System;
using Nop.Core.Plugins;
using Nop.Web.Framework.Menu;
using System.Web.Routing;
using System.Linq;
using DallasArt.Models.Models;
using Nop.Core.Data;

namespace Nop.Plugin.DallasArt.PaidMembership
{
    class PaidMembership : BasePlugin, IAdminMenuPlugin
    {

        //  private readonly SampleDownloadDbContext _context;

        //  public DownloadCounter(SampleDownloadDbContext context, IRepository<SampleDownload> repo)
        //  {
        //      _context = context;
        // }





        public override void Install()
        {
//            _context.Install();
            base.Install();
        }

        public override void Uninstall()
        {
            // set true to remove tables
            // for testing
            //  _context.Uninstall(false); ;

            base.Uninstall();
        }




        public void ManageSiteMap(SiteMapNode rootNode)
        {
            var menuItem = new SiteMapNode()
            {
                SystemName = "Paid.Membership",
                Title = "Paid Membership",
                ControllerName = "Test",
                ActionName = "Index",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", null } }
            };


         
            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            if (pluginNode != null)
                pluginNode.ChildNodes.Add(menuItem);
            else
                rootNode.ChildNodes.Add(menuItem);
        }



    }





}
