using Nop.Web.DallasArt.Interfaces;

namespace Nop.Web.DallasArt.Classes
{
    public class DropDownOption : IDropDownOption
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool? Select{ get; set; }

        public string Value { get; set; }
    }
}