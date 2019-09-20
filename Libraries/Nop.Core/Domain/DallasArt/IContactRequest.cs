using System;


namespace Nop.Web.DallasArt.Interfaces
{
    public interface IContactRequest
    {
        int ContectRequestId { get; set; }
        string FirstName { get; set; }

        string LastName { get; set; }

        string Phone { get; set; }

        string Email { get; set; }

        int CustomerId { get; set; }

        bool IsRegistered { get; set; }

        int Store { get; set; }

        String IpAddress { get; set; }

        string Category { get; set; }

        string Question { get; set; }

        DateTime QuestionAsk { get; set; }

        DateTime? QuestionResolved { get; set; }

   //     string CenterName { get; set; }
   //     string CenterId { get; set; }

        string Notes { get; set; }
    }
}