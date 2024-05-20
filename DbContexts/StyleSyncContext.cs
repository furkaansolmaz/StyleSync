using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SyncStyle.Model;

namespace SyncStyle.DbContexts
{
    public class StyleSyncContext : DbContext
    {

        public StyleSyncContext(DbContextOptions<StyleSyncContext> options) : base(options)
        {
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<StyleSyncProd> StyleSyncProds { get; set; }

    }
    public class StyleSyncContextDesignFactory : IDesignTimeDbContextFactory<StyleSyncContext>
    {
        private readonly IConfiguration configuration;
        public StyleSyncContextDesignFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public StyleSyncContext CreateDbContext(string[] args)
        {
        
            var optionsBuilder = new DbContextOptionsBuilder<StyleSyncContext>()
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"));



            return new StyleSyncContext(optionsBuilder.Options);
        }
    }
}