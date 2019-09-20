using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using Microsoft.Ajax.Utilities;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Web.Themes.PAA.Classes;
using Nop.Web.Themes.PAA.ViewModels;
using Nop.Web.Themes.PAA.Models;


namespace Nop.Web.Themes.PAA.Controllers
{
    public class ImageReviewController : Controller
    {

        private const string _repository = "App_Data"; 
        private readonly string _storeName  = EngineContext.Current.Resolve<IStoreContext>().CurrentStore.Name;
        private readonly IList<UserViewModel> _list = new List<UserViewModel>();

      


        #region Utilities

        public byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Jpeg);
            var rtnVal = ms.ToArray();
            ms.Close();
            ms.Dispose();

            return rtnVal;
        }

        public static Bitmap Resize(Bitmap imgPhoto, Size objSize, ImageFormat enuType)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = objSize.Width;
            int destHeight = objSize.Height;

            Bitmap bmPhoto;
            if (enuType == ImageFormat.Png)
                bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format32bppArgb);
            else if (enuType == ImageFormat.Gif)
                bmPhoto = new Bitmap(destWidth, destHeight); //PixelFormat.Format8bppIndexed should be the right value for a GIF, but will throw an error with some GIF images so it's not safe to specify.
            else
                bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);

            //For some reason the resolution properties will be 96, even when the source image is different, so this matching does not appear to be reliable.
            //bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //If you want to override the default 96dpi resolution do it here
            bmPhoto.SetResolution(72, 72);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        public static Bitmap SmartResize(string strImageFile, Size objMaxSize, ImageFormat enuType)
        {
            Bitmap objImage = null;
            try
            {
                objImage = new Bitmap(strImageFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (objImage.Width > objMaxSize.Width || objImage.Height > objMaxSize.Height)
            {
                Size objSize;
                int intWidthOverrun = 0;
                int intHeightOverrun = 0;
                if (objImage.Width > objMaxSize.Width)
                    intWidthOverrun = objImage.Width - objMaxSize.Width;
                if (objImage.Height > objMaxSize.Height)
                    intHeightOverrun = objImage.Height - objMaxSize.Height;

                double dblRatio;
                double dblWidthRatio = (double)objMaxSize.Width / (double)objImage.Width;
                double dblHeightRatio = (double)objMaxSize.Height / (double)objImage.Height;
                if (dblWidthRatio < dblHeightRatio)
                    dblRatio = dblWidthRatio;
                else
                    dblRatio = dblHeightRatio;
                objSize = new Size((int)((double)objImage.Width * dblRatio), (int)((double)objImage.Height * dblRatio));

                Bitmap objNewImage = Resize(objImage, objSize, enuType);

                objImage.Dispose();
                return objNewImage;
            }
            else
            {
                return objImage;
            }
        }

        private void BuildModel(XmlNode fc, string currentShow  , int count)
        {
            var Uvm = new UserViewModel { key = count };

           
           



            //  string path = Path.Combine( Server.MapPath("/"), "Themes", _storeName, _repository ,currentShow);







            foreach (XmlNode node in fc)
            {
                switch (node.Name)
                {
                    case "CustomerName":
                        {
                            Uvm.Name = node.InnerText;
                            break;
                        }

                    case "CustomerPhone":
                        {
                            Uvm.Phone = node.InnerText;
                            break;
                        }

                    case "CustomerEmail":
                        {
                            Uvm.Email = node.InnerText;
                            break;
                        }

                    case "TimeEntered":
                        {
                            Uvm.Entered = node.InnerText;
                            break;
                        }

                    case "ArtistName":
                        {
                            Uvm.ArtistName = node.InnerText;
                            break;
                        }

                    case "CountryofOrigin":
                        {
                            Uvm.CountryofOrigin = node.InnerText;
                            break;
                        }

                    case "ArtTitle":
                        {
                            Uvm.ArtTitle = node.InnerText;
                            break;
                        }


                    case "Dimensions":
                        {
                            Uvm.Dimensions = node.InnerText;
                            break;
                        }

                    case "Medium":
                        {
                            Uvm.Medium = node.InnerText;
                            break;
                        }

                    case "Price":
                        {
                            Uvm.Price = node.InnerText;
                            break;
                        }

                    case "ImagePath":
                    {

                        var filename = FixupHardPath(node.InnerText , currentShow);

                            if (System.IO.File.Exists(filename))
                            {
                                Size objMaxSize = new Size(150, 150);
                                Bitmap objNewImage = SmartResize(filename, objMaxSize, ImageFormat.Jpeg);
                                var byteArray = ImageToByteArray(objNewImage);

                                Uvm.ImageBytesArray = byteArray;
                            }

                        Uvm.Path =   node.InnerText;
                            break;
                        }

                }
            }

            _list.Add(Uvm);

        }



        private static string FixupHardPath( string path , string currentShow)
        {
            // if image was collected on a different server 
            // server hard path is invalid, replace with current 
            // hard path just in case

            int start = path.IndexOf(currentShow, StringComparison.Ordinal) + currentShow.Length;
            int stop = path.Length - start ;

            var finalPath = path.Substring(start, stop);

            return  ContestLocation.PathToShow(currentShow) + finalPath ;
        }

        private void GetEntries(string currentShow)
        {
            int Count = 0;

            string path =  ContestLocation.PathToShow(currentShow);


          //  string path = Path.Combine( Server.MapPath("/"), "Themes", _storeName, _repository ,currentShow);


            if ( Directory.Exists(path) )
            {

                string[] dirs = Directory.GetDirectories(path, "*");


                foreach (string entry in dirs)
                {

                    var directoryFiles = Directory.GetFiles(entry);

                    foreach (string filename in directoryFiles)
                    {



                        if (System.IO.File.Exists(filename) && Path.GetExtension(filename) == ".xml")
                        {


                            try
                            {

                                XmlDocument xmlDoc = new XmlDocument();
                                xmlDoc.Load(filename);
                                if (xmlDoc.FirstChild == null || !xmlDoc.FirstChild.HasChildNodes) continue;

                                BuildModel(xmlDoc.FirstChild, currentShow , Count++ );

                            }
                            catch (Exception )
                            {
                                continue;

                            }



                        }
                    }
                }
            }
        }

        #endregion
        
       
        public ActionResult Index()
        {
            //  GetEntries("members_contest");
            //  GetEntries("Eastern_Impressions");
            //  GetEntries("Global_Reflections_2018");
                GetEntries("Global_Reflections_2019");

            //  GetEntries("members_contest_2019");
            //  GetEntries("Bank_Art_Submissions");

            var entrysGrouped = from l in _list
                                group l by "Email: " + l.Email + " | Name: " + l.Name + " | Phone: " + l.Phone into g
                                // group l by l.Email into g

             select new EntryGroup<string, UserViewModel> { Key = g.Key, Values = g };

            return View( "Index" , entrysGrouped);



           // return View();
        }
    }
}