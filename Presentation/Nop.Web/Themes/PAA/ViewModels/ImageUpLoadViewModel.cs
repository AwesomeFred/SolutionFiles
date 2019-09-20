using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Nop.Web.Themes.PAA.ViewModels
{
    public class ImageUploadViewModel
    {

        public string ArtistName { get; set; }

      //  public string CountryofOrigin { get; set; }

        public string ArtTitle { get; set; }

        public string Dimensions { get; set; }

        public string Medium { get; set; }

        public string Price { get; set; }


        public string FilePath { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string Telephone { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public bool Member { get; set; } = false;

        public int RemainingImages { get; set; } = 3;

        public string ModelState { get; set; } = "Initial";

    }
}