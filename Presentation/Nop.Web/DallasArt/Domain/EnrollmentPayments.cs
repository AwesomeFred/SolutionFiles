using System;
using Nop.Core;

namespace Nop.Web.DallasArt.Domain
{
    public class EnrollmentPayments : BaseEntity
    {

        public int SiteLocationInvoiceId { get; set; }

        public DateTime? DatePlaced { get; set; }

        public int? ProductId { get; set; }

        public int? InvoiceId { get; set; }

        public int? EnrollmentLocationId { get; set; }

        public DateTime? RenewalDate { get; set; }


        public virtual EnrollmentLocation EnrollmentLocation { get; set; }
    }
}