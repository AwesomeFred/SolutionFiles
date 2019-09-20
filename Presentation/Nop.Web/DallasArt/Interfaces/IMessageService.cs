
using System.Collections.Generic;
using Nop.Core.Domain.Orders;
using Nop.Web.DallasArt.Interfaces.Customer;
using Nop.Web.DallasArt.Interfaces.DashBoard;
using Nop.Web.DallasArt.ViewModels;


namespace Nop.Web.DallasArt.Interfaces
{
    public interface IMessageService
    {
        
        /// <summary>
        /// Template:
        /// Contact.AutoResponseMessage
        /// 
        /// </summary>
         /// <param name="request"></param>
        /// <returns></returns>
        int ContactAutoResponseMessage( ContactRequestViewModel request );

        /// <summary>
        /// Template:
        /// Contact.RequestMessage
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        int ContactRequestMessage(ContactRequestViewModel request);

        /// <summary>
        /// Template:
        /// Enrollment.RequestMessage
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        int SendEnrollmentWelcomeMessage(OrderPlacedEvent request );

        ///// <summary>
        ///// Template:
        ///// Enrollment.Satellite.Notification
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //int SendSatelliteEzLinkInvitation(ISatelliteCenterViewModel model);

        /// <summary>
        /// Template:
        /// Registation.WelcomeMessage
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>
        int SendRegistationWelcomeMessage(Core.Domain.Customers.Customer customer, int languageId);

        /// <summary>
        /// Template:
        /// Registation.InstructionMessage
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
     //   string GetInstructionsForMembership(Core.Domain.Customers.Customer customer);

        /// <summary>
        /// Template:
        /// Evantell.Membership.EzInvitation
        /// </summary>
        /// <param name="model"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        int SendMembershipEzInvitation(IMembershipInstructionsViewModel model, Core.Domain.Customers.Customer customer);


        /// <summary>
        /// Template:
        /// Evantell.SendPrayerCoordinatorRegistationEzLinkInvitation
        /// </summary>
        /// <param name="addresses">Send to addresses</param>
        /// <returns></returns>
        int SendPrayerCoordinatorMembershipEzInvitation(List<string> addresses);




        /// <summary>
        /// Template:
        /// Evantell.Membership.CenterEmail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
      //  int SendCenterLocationGroupEmail(IMembershipInstructionsViewModel model);

    }
}
