using Nop.Services.Events;
using Nop.Web.DallasArt.Events.Register;
using Nop.Web.DallasArt.Interfaces;

namespace Nop.Web.DallasArt.Events.Responders
{

    class ContactRequestCreatedEventConsumer : IConsumer<ContactRequestCreatedEvent>
    {
        private readonly IMessageService _messageService;

        public ContactRequestCreatedEventConsumer(IMessageService messageService     )
        {
            _messageService = messageService;
        }
        
         
        public void HandleEvent(ContactRequestCreatedEvent publishEventMessage)
        {
        //    _messageService.ContactAutoResponseMessage(publishEventMessage.Model);

            _messageService.ContactRequestMessage(publishEventMessage.Model);

        }
    }
}
