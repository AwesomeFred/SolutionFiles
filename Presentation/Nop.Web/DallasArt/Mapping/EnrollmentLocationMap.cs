using Nop.Data.Mapping;
using Nop.Web.DallasArt.Domain;

namespace Nop.Web.DallasArt.Mapping
{
    public class EnrollmentLocationMap : NopEntityTypeConfiguration<EnrollmentLocation>
    {
        public EnrollmentLocationMap()
        {

            this.ToTable("EnrollmentLocation");

            this.Property(elg => elg.EnrollmentLocationId);
            this.HasKey(elg => elg.EnrollmentLocationId);  // Map Primary Key

            // Additional Properties 
            //    this.Property(elg => elg.EnrollmentId);

            this.Property(elg => elg.PrimaryCenterName).IsOptional(); ;
            this.Property(elg => elg.PrimaryCenterGroupId).IsOptional(); ;

            this.Property(elg => elg.SatelliteCenter);
            this.Property(elg => elg.SingleLocationCenter);
            this.Property(elg => elg.CenterName);
            this.Property(elg => elg.CenterGroupId);

            this.Property(elg => elg.OriginalCustomerId);
            this.Property(elg => elg.OriginalEnrollmentDate);

            this.Property(elg => elg.OriginalNotification).IsOptional();

            // remove unused base property 
            this.Ignore(elg => elg.Id);


            //        this.HasMany(e => e.EnrollmentPayments)
            //             .WithOptional(e => e.EnrollmentLocationGroup)
            //             .HasForeignKey(e => e.EnrollmentLocationId);

        }


    }
}