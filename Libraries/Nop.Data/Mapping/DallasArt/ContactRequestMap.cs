using Nop.Core.Domain.DallasArt;

namespace Nop.Data.Mapping.DallasArt
{
    public partial class ContactRequestMap : NopEntityTypeConfiguration<ContactRequest>
    {

        public ContactRequestMap()
        {
            ToTable("ContactRequest");

            Property(m => m.ContectRequestId);

            // Map Primary Key
            HasKey(m => m.ContectRequestId);

            // Additional Properties 
            Property(m => m.FirstName).HasMaxLength(100);
            Property(m => m.LastName).HasMaxLength(200);
            Property(m => m.Phone).IsOptional().HasMaxLength(50);
            Property(m => m.Email).IsOptional().HasMaxLength(50);
            Property(m => m.CustomerId).IsOptional();
            Property(m => m.IsRegistered);
            Property(m => m.Store);
            Property(m => m.IpAddress).HasMaxLength(50);
            Property(m => m.Category).IsRequired().HasMaxLength(50);
            Property(m => m.Question).HasMaxLength(1000);
            Property(m => m.QuestionAsk);
            Property(m => m.QuestionResolved).IsOptional();
 //           Property(m => m.CenterName).HasMaxLength(100);
 //           Property(m => m.CenterId).HasMaxLength(100);
            Property(m => m.Notes).IsOptional().HasMaxLength(1000);

            // remove unused base property 
            this.Ignore(m => m.Id);

        }
    }
}