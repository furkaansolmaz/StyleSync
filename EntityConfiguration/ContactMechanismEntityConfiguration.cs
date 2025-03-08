using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncStyle.EnumType;
using SyncStyle.Model;

namespace SyncStyle.EntityConfiguraiton
{
        public class ContactMechanismEntityConfiguration : IEntityTypeConfiguration<ContactMechanism>
    {
        public void Configure(EntityTypeBuilder<ContactMechanism> builder)
        {
            builder.ToTable("ContactMechanism");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .IsRequired()
                .UseHiLo("contact_mechanism_id_hilo");

            builder.Property(ci => ci.UserId)
                .IsRequired(true);
            
            builder.HasIndex(ct=>ct.UserId)
                    .IsUnique();
    
            builder.Property(ct => ct.ContactMechanismInfo)
                .HasColumnType("varchar(100)");
            
            builder.Property(ct => ct.ContactMechanismType)
                .IsRequired()
                .HasColumnType("varchar(10)")
                .HasConversion(
                    v => v.ToString(),
                    s => (ContactMechanismType)Enum.Parse(typeof(ContactMechanismType), s));

            builder.Property(r => r.IsActive)
                .IsRequired();

            builder.Property(r => r.CreateDate)
                .IsRequired(true);

             builder.Property(r => r.UpdateDate)
                .IsRequired(false); 
        }
    }
}