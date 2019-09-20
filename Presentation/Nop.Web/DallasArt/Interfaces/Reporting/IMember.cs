using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.DallasArt.Interfaces.Reporting
{
    public interface IMember
    {
        int Id { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        bool Enrolled { get; set; }
        bool Active { get; set; }
        DateTime Joined { get; set; }
        bool EvantellDirector { get; set; }
        bool EvantellAssistantDirector { get; set; }
        bool EvantellPrayerCoordinator { get; set; }
        bool EvantellAssistantPrayerCoordinator { get; set; }
        int CartCount { get; set; }
        string CenterName { get; set; }
        string CenterId { get; set; }
        string Phone { get; set; }
    }
}