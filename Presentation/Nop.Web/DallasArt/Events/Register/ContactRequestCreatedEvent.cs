
using Nop.Web.DallasArt.ViewModels;

namespace Nop.Web.DallasArt.Events.Register
{
    /// <summary>
    /// Contact request event
    /// </summary>
   public class ContactRequestCreatedEvent
    {
        public ContactRequestCreatedEvent(ContactRequestViewModel model)
        {
            this.Model = model;
        }

        public ContactRequestViewModel Model { get; private set; }

    }
}
