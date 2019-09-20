using Nop.Data.Mapping;
using Nop.Web.DallasArt.Domain;

namespace Nop.Web.DallasArt.Mapping
{
    public class EnrollmentPaymentsMap : NopEntityTypeConfiguration<EnrollmentPayments>
    {
   
      public EnrollmentPaymentsMap()
        {
            ToTable("EnrollmentPayments");

            this.Property(etc => etc.SiteLocationInvoiceId);

            // Map Primary Key
            this.HasKey(etc=>etc.SiteLocationInvoiceId);

            // Additional Properties 
     
            this.Property(etc => etc.EnrollmentLocationId);
            this.Property(etc => etc.DatePlaced);
            this.Property(etc => etc.RenewalDate);
            this.Property(etc => etc.ProductId);
            this.Property(etc => etc.InvoiceId);
  
            // remove unused base property 
            Ignore(etc => etc.Id);

            // new 
            this.HasRequired(etc => etc.EnrollmentLocation)
              .WithMany(etc => etc.EnrollmentPayments)
              .HasForeignKey(etc => etc.EnrollmentLocationId);

      }
    }
}