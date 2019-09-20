using System;

namespace Nop.Core.Domain.DallasArt
{
    public interface IMemberEnrollmentPayments
    {
        int MemberPaymentId { get; set; }

        int MemberEnrollmentId { get; set; }

        DateTime? DatePlaced { get; set; }

        int? ProductId { get; set; }

        int? InvoiceId { get; set; }


        DateTime? RenewalDate { get; set; }

    }
}