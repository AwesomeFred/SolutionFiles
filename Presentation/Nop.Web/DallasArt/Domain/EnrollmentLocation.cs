using System;
using System.Collections.Generic;
using Nop.Core;

namespace Nop.Web.DallasArt.Domain
{

    public class EnrollmentLocation : BaseEntity
    {

        private ICollection<EnrollmentPayments> _enrollmentPayments;

        public int EnrollmentLocationId { get; set; }


        public bool SatelliteCenter { get; set; }
        public bool SingleLocationCenter { get; set; }

        public string PrimaryCenterGroupId { get; set; }


        public string PrimaryCenterName { get; set; }


        public string CenterName { get; set; }


        public string CenterGroupId { get; set; }


        public int OriginalCustomerId { get; set; }

        public DateTime? OriginalEnrollmentDate { get; set; }


        public DateTime? RenewalDate { get; set; }


        public string OriginalNotification { get; set; }


        public ICollection<EnrollmentPayments> EnrollmentPayments
        {
            get
            {
                return _enrollmentPayments ??
               (_enrollmentPayments = new List<EnrollmentPayments>());
            }

            set { _enrollmentPayments = value; }
        }


    }
 
}