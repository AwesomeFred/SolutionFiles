
namespace Nop.Web.Themes.PAA.Classes
{
    public class ArtWork
    {


        public string Name;
        public string Title;
        public string Path;
        public Dimension size;
        public decimal Price;


        public struct Dimension
        {
            public int height;  // z
            public int width;   // x
            public int length;  // h
        }


    }
}