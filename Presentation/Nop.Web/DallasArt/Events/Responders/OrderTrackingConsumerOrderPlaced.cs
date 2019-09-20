using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Core.Domain.Customers;
//using AutoMapper;

using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Events;
using Nop.Services.Events;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.Services;
using Nop.Web.Themes.PAA.ViewModels;

namespace Nop.Web.DallasArt.Events.Responders
{
    public class OrderTrackingConsumerOrderPlaced : IConsumer<EntityInserted<Order>>
    {

        private    IOrderTrackingService _orderTrackingService { get; set; }
         private    IDallasArtContext  _dallasArtContext { get; set; }



        public OrderTrackingConsumerOrderPlaced( IOrderTrackingService  orderTrackingService, IDallasArtContext dallasArtContext)
        {
            _dallasArtContext = dallasArtContext;
            _orderTrackingService = orderTrackingService;
        }


        public void HandleEvent(EntityInserted<Order> eventMessage)
        {
            Order order = eventMessage.Entity;
            Customer customer = order.Customer;

            // membership order ??
            // for now only process memberships 
            if (order.OrderItems.Count != 1 || order.OrderItems.First().Id > 7)
            {
                return;
            }


            switch (order.OrderItems.First().Id)
            {
                case (int)CustomerRoleType.StudentMembership:
                    {
                        break;
                    }
                case (int)CustomerRoleType.AdultMembership:
                    {
                        break;
                    }
                case (int)CustomerRoleType.SeniorMembership:
                    {
                        break;
                    }
                case (int)CustomerRoleType.FamilyMembership1:
                    {
                       // model.Family = true;
                        break;
                    }
                case (int)CustomerRoleType.SeniorFamilyMembership1:
                    {
                       // model.Family = true;
                        break;
                    }
                case (int)CustomerRoleType.Donor:
                    {
                       // _dallasArtContext.IsRegistration = false;
                       // _dallasArtContext.NeedsPassword = false;
                       // model.SelfIdentificationViewModel.RequestPassword = false;
                        break;
                    }

            }


            if (order != null)
            {
                //var orderTracker = new OrderTracking
                //{
                //    OrderId = order.Id,
                //    PlacedDate = DateTime.Now,
                //    PendingDate = null,
                //    PaidDate = null,
                //    InProcessDate = null,
                //    ShippedDate = null,
                //    DeliveredDate = null

                //};

                //_orderTrackingService.Create(orderTracker);
            }
        }

    }


    public class OrderTrackingConsumerOrderUpdated : IConsumer<EntityUpdated<Order>>
    {
  
       private    IOrderTrackingService _orderTrackingService { get; set; }
   
        public OrderTrackingConsumerOrderUpdated( IOrderTrackingService  orderTrackingService      )
        {
            _orderTrackingService = orderTrackingService;
        }

        
        public void HandleEvent(EntityUpdated<Order> eventMessage)
        {

              Order order = eventMessage.Entity;

            if (order != null)
            {
              //  OrderTracking current = _orderTrackingService.GetByOrderId(order.Id);

                switch (order.OrderStatus)
                {
                    
                    case     OrderStatus.Pending :
                    {
                 //       if (current != null && current.PendingDate == null )
                        {
                            
                  //          current.PendingDate = DateTime.Now;
                  //          _orderTrackingService.Update(current);

                        }
                        
                        break;
                    }

                    case OrderStatus.Processing:
                    {
                        break;
                    }


                }

                switch (order.ShippingStatus)
                {
                
                    case ShippingStatus.NotYetShipped:
                    {
                        break;
                    }

                    case ShippingStatus.Shipped:
                    {
                        break;
                    }



                }


                switch (order.PaymentStatus)
                {
                
                  case PaymentStatus.Paid:
                    {
  //                      if (current != null && current.PaidDate == null)
                        {
  //                          current.PaidDate = DateTime.Now;
  //                          _orderTrackingService.Update(current);

                        }

                        break;
                    }

                }











            }


        }
    }
}


