namespace Nop.Web.DallasArt.Interfaces.Reporting
{
    public interface ICenterViewModel
    {
        string ErrorMessage { get; set; }
        string CenterName { get; set; }
        string CenterType { get; set; }
        string CenterId { get; set; }
        string Enrolled { get; set; }
        string Expires { get; set; }
        string Director { get; set; }
        string Coordinator { get; set; }
    }
}