using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.DallasArt;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Infrastructure;
using Nop.Services.Events;
using Nop.Services.Messages;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.Services;

namespace Nop.Web.DallasArt.Events.Responders
{
    public class EntityTokensAddedEventConsumerOrder : IConsumer<EntityTokensAddedEvent<Order, Token>>
    {

        private const string EnrollmentMessage = "Thank you for enrolling your Center in the Gift of Prayer program. You will see billing and shipping information listed below:";
        private const string PurchaseMessage = "Thank you for your order from the Gift of Prayer program. You will see billing and shipping information listed below:";


        public void HandleEvent(EntityTokensAddedEvent<Order, Token> eventMessage)
        {
            String orderType = "Standard Order";
            String customerMessage = PurchaseMessage;


            Customer customer = eventMessage.Entity.Customer;
            Order order = eventMessage.Entity;

            if (customer == null || order == null) return;

            var storeUrl = Utilities.GetStoreUrl();


            // fulfillment vendor  acknowledgments
            var receivedPath = string.Format("{2}Taylor/OrderAcknowledgment/{0}/{1}", customer.Id, order.Id, storeUrl);
            var sendPath = string.Format("{2}Taylor/OrderShipped/{0}/{1}", customer.Id, order.Id, storeUrl);

            bool isEnrollment = order.OrderItems.Any(item => Utilities.IsEnrollment(item.ProductId));

            if (isEnrollment)
            {

                IList<CustomerServices.AttributeValuePair> attributes = Utilities.GetCustomerAttributes(customer);

                // find the customers center group id
                var centerId = (from n in attributes
                                where n.AttributeName == "Center Enrollment Id"
                                select n.ValueText).SingleOrDefault();


 //               MemberEnrollment location = EngineContext.Current.Resolve<IEnrollmentRequestService>()
 //                   .GetEnrollmentLocationByGroupId(centerId); ;

 //               if (location != null)
 //               {
 //                   orderType = "Enrollment";
 //                   customerMessage = EnrollmentMessage;
 //               }

           }

            eventMessage.Tokens.Add(new Token("Evantell.OrderGreeting", customerMessage));


            eventMessage.Tokens.Add(new Token("Evantell.OrderType", orderType));

            eventMessage.Tokens.Add(new Token("Evantell.OrderFulfillment.Taylor", "Taylor Order Fulfillment--"));
            eventMessage.Tokens.Add(new Token("Evantell.Order.ReceivedAcknowledgment", receivedPath));
            eventMessage.Tokens.Add(new Token("Evantell.Order.ShippedAcknowledgment", sendPath));

        }
    }
}