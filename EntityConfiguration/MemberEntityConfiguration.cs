using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncStyle.EnumType;
using SyncStyle.Model;

namespace SyncStyle.EntityConfiguraiton 
{
    public class MemberEntityConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Member");

            builder.HasKey(ci => ci.MemberId);

            builder.Property(ci => ci.MemberId)
                .IsRequired()
                .UseHiLo("member_id_hilo");

            builder.Property(ct => ct.Name)
                .HasColumnType("nvarchar(50)");

            builder.Property(ct => ct.LastName)
                .HasColumnType("nvarchar(50)");

            builder.Property(ct => ct.UserName)
                .HasColumnType("nvarchar(50)");

            builder.Property(ct => ct.Password)
                .HasColumnType("nvarchar(25)");

            builder.Property(ct => ct.DateOfBirth)
                .ValueGeneratedNever()
                .IsRequired();                

            builder.Property(ct => ct.Gender)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasConversion(
                    v => v.ToString().ToLower(),
                    s => (GenderStatus)Enum.Parse(typeof(GenderStatus), s));

            builder.Property(r => r.IsActive)
                .IsRequired();
        }
    }
}