using Nop.Core.Domain.DallasArt;

namespace Nop.Data.Mapping.DallasArt
{
    public partial class MemberEnrollmentMap : NopEntityTypeConfiguration<MemberEnrollment>
    {
        public MemberEnrollmentMap()
        {

            this.ToTable("MemberEnrollment");

            this.Property(me => me.MemberEnrollmentId);
            this.HasKey(me => me.MemberEnrollmentId);  // Map Primary Key

            // Additional Properties 
           
            this.Property(me => me.EnrollmentType);
            this.Property(me => me.CustomerId);
            this.Property(me => me.CustomerIdFamily).IsOptional();
            this.Property(me => me.OriginalEnrollmentDate);
            this.Property(me => me.LatestRenewalDate);
           

            // remove unused base property 
            this.Ignore(elg => elg.Id);


                     this.HasMany(me => me.MemberEnrollmentPayments)
                         .WithMany() 
                         .Map(me => me.ToTable( "MemberEnrollmentPayments"   ) );
                        
        }


    }
}