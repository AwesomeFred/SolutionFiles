using System;
using System.Collections.Generic;

namespace Nop.Core.Domain.DallasArt
{

    public class MemberEnrollment : BaseEntity , IMemberEnrollment    
    {
        
        public int MemberEnrollmentId { get; set; }

        public int EnrollmentType { get; set; }

        public int  CustomerId { get; set; }

        public int CustomerIdFamily { get; set; }


        public DateTime? OriginalEnrollmentDate { get; set; }


        public DateTime? LatestRenewalDate { get; set; }


        public ICollection<MemberEnrollmentPayments> MemberEnrollmentPayments
        {
            get; set; 
        } =  new List<MemberEnrollmentPayments>();

    }
 
}