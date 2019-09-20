namespace Nop.Web.DallasArt.Interfaces
{
    public interface IDropDownOption
    {
        int? Id { get; set; }
       
        string Name { get; set; }
       
        bool? Select { get; set; }

        string Value { get; set; }
    }
}