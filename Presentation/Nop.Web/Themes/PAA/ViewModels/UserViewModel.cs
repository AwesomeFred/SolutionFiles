using System.Drawing;

namespace Nop.Web.Themes.PAA.ViewModels
{
    public class UserViewModel
    {

        public int key { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Entered { get; set; }
        public string ArtistName { get; set; }
        public string CountryofOrigin { get; set; }
        public string ArtTitle { get; set; }
        public string Dimensions { get; set; }
        public string Medium { get; set; }
        public string Price { get; set; }
        public string Path { get; set; }
        public Image Image { get; set; }
        public byte[] ImageBytesArray { get; set; }

    }
}