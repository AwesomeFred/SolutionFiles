using System;
using System.Collections.Generic;

namespace Nop.Core.Domain.DallasArt
{
    public interface IMemberEnrollment
    {
        int MemberEnrollmentId { get; set; }

        int EnrollmentType { get; set; }

        int CustomerId { get; set; }

        int CustomerIdFamily { get; set; }


        DateTime? OriginalEnrollmentDate { get; set; }


        DateTime? LatestRenewalDate { get; set; }

        ICollection<MemberEnrollmentPayments> MemberEnrollmentPayments { get; set; }
    }
}