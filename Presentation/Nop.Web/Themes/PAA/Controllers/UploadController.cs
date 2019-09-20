using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Web.Controllers;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.Themes.PAA.Infrastructure;
using Nop.Web.Themes.PAA.ViewModels;


namespace Nop.Web.Themes.PAA.Controllers
{
    public class UploadController : BasePublicController
    {
        private readonly CustomerSettings _customerSettings;
        private readonly IWorkContext _workContext;
        private readonly IDallasArtContext _dallasArtContext;
        private readonly ICustomerService _customerService;
        private readonly IStoreContext _storeContext;
        private readonly ICustomerRegistrationService _customerRegistrationService;


        public UploadController(
            CustomerSettings customerSettings,
            IWorkContext workContext,
            IDallasArtContext dallasArtContext,
            ICustomerService customerService,
            ICustomerRegistrationService customerRegistrationService,
            IStoreContext storeContext

         )
        {
            this._customerSettings = customerSettings;
            this._workContext = workContext;
            this._dallasArtContext = dallasArtContext;
            this._customerService = customerService;
            this._customerRegistrationService = customerRegistrationService;
            this._storeContext = storeContext;
        }

       // private const string BasePath = @"~\Themes\paa\A_Upload_Area\global_reflections_contest\";

        
     

        private string BuildString(string type, string data)
        {
            return @"<" + type + ">" + data + "</" + type + ">";
        }
        private string BuildXmlFile(ImageUploadViewModel model, string imagePath)
        {
            var entryTime = (DateTime.Now).ToShortDateString() + ":" + (DateTime.Now).ToShortTimeString();

            StringBuilder sb = new StringBuilder();
            sb.Append(@"<ImageInfo>");

            sb.Append(BuildString("CustomerName", model.CustomerName));
            sb.Append(BuildString("CustomerPhone", model.Telephone));
            sb.Append(BuildString("CustomerEmail", model.EmailAddress));
            sb.Append(BuildString("TimeEntered",  entryTime));

            sb.Append(BuildString("ArtistName", model.ArtistName));
         //   sb.Append(BuildString("CountryofOrigin", model.CountryofOrigin));
            sb.Append(BuildString("ArtTitle", model.ArtTitle));
            sb.Append(BuildString("Dimensions", model.Dimensions));
            sb.Append(BuildString("Medium", model.Medium));
            sb.Append(BuildString("Price", model.Price));
            sb.Append(BuildString("ImagePath", imagePath));

            sb.Append(@"</ImageInfo>");

            return sb.ToString();
        }

  

        private const string ImageLibrary = "Uploaded_Image_Libraries";

        private const string CurrentShow = "Global_Reflections_2019";   // "Bank_Art_Submissions";  //   "members_contest_2019";    // "Global_Reflections_2018";  // "Eastern_Impressions";

        private const int MaxCount = 3;

   public ActionResult UploadFile(string name, string phone, string email, string member)
        {
            ImageUploadViewModel model = new ImageUploadViewModel
            {
                CustomerName = name,
                Telephone = phone,
                EmailAddress = email,
                RemainingImages = 0,
                Member =    int.Parse(member) == 2
            };

            return PartialView("_FormDesign", model);
        }

        [HttpPost]
        public FineUploaderResult UploadFile(FineUpload upload, ImageUploadViewModel model, FormCollection form)
        {
            int counter = 1;
  
           //   var hardPath = Server.MapPath("~/").Replace("httpdocs\\Presentation\\Nop.Web\\", "uploaded_image_libraries\\");

            var hardPath = Path.Combine(Server.MapPath("~/App_Data/") , ImageLibrary  ) ; 
            
            if (!Directory.Exists(hardPath))
            {
                try
                {
                    Directory.CreateDirectory(hardPath);
                }
                catch (Exception)
                {
                    throw new Exception("Cannot Create BaseLine Config.");
                }
            }

            var currentShow = Path.Combine( hardPath, CurrentShow);

            // var dir = @"c:\upload\path";
            // var filePath = Path.Combine(dir, upload.Filename);

            if (!Directory.Exists(currentShow))
            {
                try
                {
                    Directory.CreateDirectory(currentShow);
                }
                catch (Exception )
                {
                    throw new Exception("Cannot Create BaseLine Show File");
                }

            }
           
            var uploadFileName = Path.GetFileName(upload.Filename);

            if (uploadFileName == null)
            {
                return new FineUploaderResult(false, error: "BadInput");
            }
            
            var owner = model.EmailAddress.Replace("@", ".");
            var xmlFileName = model.ArtTitle  + ".xml";

            //  var dir = Server.MapPath(currentShow) + owner;
            // var dir = OffsetFromBase + owner;

            var dir = currentShow + "\\" + owner;
            

            var filePath = Path.Combine(dir, uploadFileName);
            var infoPath = Path.Combine(dir, xmlFileName);

            string xmlFile = BuildXmlFile(model, filePath);
            

            if (Directory.Exists(dir))
            {
                var directoryFiles = Directory.GetFiles(dir);

                counter += directoryFiles.Count(path => Path.GetExtension(path).ToLower() == ".xml");
            }

            if (counter  <= MaxCount  )
            {

                try
                {
                  if ( System.IO.File.Exists(filePath) ) 
                    {
                        throw new Exception( "This image previously submitted!"  );
                    }

                    upload.SaveAs(filePath);


                }
                catch (Exception ex)
                {
                    return new FineUploaderResult(false, error: ex.Message);
                }

                try
                {
                    if (System.IO.File.Exists(infoPath))
                    {
 
                        throw new Exception("This image previously submitted!");          
                    }

                    System.IO.File.WriteAllText(infoPath, xmlFile);
                }
                catch (Exception ex)
                {
                    // remove uploaded file
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete( filePath  );
                    }
                    
                    return new FineUploaderResult(false, error: ex.Message);
                }


                model.ModelState = "Uploaded";
              
            }
         //   var jresult = (counter >= MaxCount) ? "Limit Reached, Pay Fee Below" : "Submit New Entry or Pay Fee Below";

            var jresult =  (counter >= MaxCount   )? "Limit Reached - Submitted Count: " + counter : "Submit New Entry - Submitted Count: " + counter ;
           
                // the anonymous object in the result below will be convert to json and set back to the browser
                // return values  bool success, object otherData = null, string error = null, bool? preventRetry = null

                return new FineUploaderResult(true, new {extraInformation = jresult });
           
        }
    }
}