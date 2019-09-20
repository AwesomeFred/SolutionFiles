using System.IO;
using System.Web;

namespace Nop.Web.Themes.PAA.Classes
{
    public static class ContestLocation
    {
       private const string ImageLibrary = "Uploaded_Image_Libraries";

        public static string PathToShow (string currentShow)
        {
            // todo 
            
            var hardPath = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/"), ImageLibrary);

             return  Path.Combine(hardPath, currentShow);
        }

    }
}