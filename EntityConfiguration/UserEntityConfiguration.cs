using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncStyle.EnumType;
using SyncStyle.Model;

namespace SyncStyle.EntityConfiguraiton 
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(ci => ci.UserId);

            builder.Property(ci => ci.UserId)
                .IsRequired()
                .UseHiLo("user_id_hilo");

            builder.Property(ct => ct.FirstName)
                .HasColumnType("varchar(50)");  // nvarchar -> varchar

            builder.Property(ct => ct.LastName)
                .HasColumnType("varchar(50)");  // nvarchar -> varchar

            builder.Property(ct => ct.UserName)
                .HasColumnType("varchar(50)");  // nvarchar -> varchar

            builder.Property(ct => ct.Password)
                .HasColumnType("varchar(25)");  // nvarchar -> varchar

            builder.Property(ct => ct.DateOfBirth)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(ci => ci.CreateDate)
                .IsRequired(true);          

            builder.Property(ct => ct.Role)
                .IsRequired()
                .HasColumnType("varchar(10)")
                .HasConversion(
                    v => v.ToString(),
                    s => (RoleName)Enum.Parse(typeof(RoleName), s));

            builder.Property(ct => ct.Gender)
                .IsRequired()
                .HasColumnType("varchar(10)")
                .HasConversion(
                    v => v.ToString(),
                    s => (GenderStatus)Enum.Parse(typeof(GenderStatus), s));

            builder.Property(r => r.IsActive)
                .IsRequired();
            
            builder.Property(r => r.CreateDate)
                .IsRequired(true);

             builder.Property(r => r.UpdateDate)
                .IsRequired(false); 
        }
    }
}