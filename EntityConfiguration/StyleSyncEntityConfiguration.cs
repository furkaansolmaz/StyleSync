using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncStyle.Model;

namespace SyncStyle.EntityConfiguraiton 
{
    public class StyleSyncEntityConfiguration : IEntityTypeConfiguration<StyleSyncProd>
    {
        public void Configure(EntityTypeBuilder<StyleSyncProd> builder)
        {
            builder.ToTable("StyleSyncProd");

            builder.HasKey(ci => ci.StyleSyncProdId);

            builder.Property(ci => ci.StyleSyncProdId)
                .IsRequired()
                .UseHiLo("style_sync_prod_id_hilo");

            builder.Property(ci => ci.UserId)
                .IsRequired(true);
            
            builder.HasIndex(ct=>ct.UserId)
                    .IsUnique();
    
            builder.Property(ct => ct.ImageUrl)
                .HasColumnType("varchar(160000)");

            builder.Property(r => r.IsActive)
                .IsRequired();
            
            builder.Property(r => r.CreateDate)
                .IsRequired(true);

             builder.Property(r => r.UpdateDate)
                .IsRequired(false); 
        }
    }
}