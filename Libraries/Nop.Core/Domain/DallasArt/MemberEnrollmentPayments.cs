using System;

namespace Nop.Core.Domain.DallasArt
{


    public class MemberEnrollmentPayments : BaseEntity, IMemberEnrollmentPayments
    {
        public int MemberPaymentId { get; set; }
       
        public int MemberEnrollmentId { get; set; }

        public DateTime? DatePlaced { get; set; }

        public int? ProductId { get; set; }

        public int? InvoiceId { get; set; }

 
        public DateTime? RenewalDate { get; set; }

        public virtual MemberEnrollment MemberEnrollment { get; set; }
    }
}