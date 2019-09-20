namespace Nop.Web.DallasArt.Interfaces.Member
{
    public interface IMemberViewModel
    {
        int Id { get; set; }
        string Email { get; set; }
        string FullName { get; set; }
        bool Active { get; set; }
        bool Prayer { get; set; }
        bool Admin { get; set; }
        bool Member { get; set; }
    }
}