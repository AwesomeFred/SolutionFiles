using Nop.Core.Domain.DallasArt;

namespace Nop.Data.Mapping.DallasArt
{
    public class MemberEnrollmentPaymentsMap : NopEntityTypeConfiguration<MemberEnrollmentPayments>
    {
   
      public MemberEnrollmentPaymentsMap()
        {
            ToTable("MemberEnrollmentPayments");

            this.Property(etc => etc.MemberPaymentId);

            // Map Primary Key
            this.HasKey(etc=>etc.MemberPaymentId);

            // Additional Properties 
     
            this.Property(etc => etc.MemberEnrollmentId);
            this.Property(etc => etc.DatePlaced);
            this.Property(etc => etc.ProductId);
            this.Property(etc => etc.InvoiceId);
            this.Property(etc => etc.RenewalDate);
            // remove unused base property 
            Ignore(etc => etc.Id);

            // new 
            this.HasRequired(etc => etc.MemberEnrollment)
                .WithMany(etc=>etc.MemberEnrollmentPayments)
                .HasForeignKey(etc => etc.MemberEnrollmentId);

      }
    }
}