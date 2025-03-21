using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SyncStyle.ChatGpts;
using SyncStyle.EntityConfiguraiton;
using SyncStyle.Model;
using SyncStyle.Services.Users;
using SyncStyle.Services.StyleSyncProds;

namespace SyncStyle.DbContexts
{
    public class StyleSyncContext : DbContext
    {

        public StyleSyncContext(DbContextOptions<StyleSyncContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<StyleSyncProd> StyleSyncProds { get; set; }
        public DbSet<ContactMechanism> ContactMechanisms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StyleSyncEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ContactMechanismEntityConfiguration());

        }

    }
}