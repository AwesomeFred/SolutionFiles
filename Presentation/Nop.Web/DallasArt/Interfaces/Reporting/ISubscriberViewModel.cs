using System.Collections.Generic;


namespace Nop.Web.DallasArt.Interfaces.Reporting
{
    public interface ISubscriberViewModel
    {
        IList<IMember> Members { get; set; }
    }
   
}