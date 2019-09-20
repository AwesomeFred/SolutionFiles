using System.Collections.Generic;

namespace Nop.Web.DallasArt.Interfaces.Reporting
{
    public interface IReportingViewModel
    {
        Core.Domain.Customers.Customer Customer { get; set; }
        string ErrorMessage { get; set; }
        IList<int> PrayerCoordinators { get; set; }
        IList<int> AssistantPrayerCoordinators { get; set; }
        IList<int> Director { get; set; }
        IList<int> AssistantDirectors { get; set; }
        ICenterViewModel Centers { get; set; }
    }
}