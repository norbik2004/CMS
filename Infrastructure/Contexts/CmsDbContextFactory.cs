using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Contexts
{
    public class CmsDbContextFactory : IDesignTimeDbContextFactory<CmsDbContext>
    {
        public CmsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CmsDbContext>();

            // Connection string do PostgreSQL
            var connectionString = "Host=localhost;Port=5435;Database=postgres;Username=postgres;Password=postgres";

            optionsBuilder.UseNpgsql(connectionString, npgsqlOptions =>
            {
                // Migracje w tym samym projekcie co DbContext
                npgsqlOptions.MigrationsAssembly(typeof(CmsDbContextFactory).Assembly.FullName);
            });

            return new CmsDbContext(optionsBuilder.Options);
        }
    }
}
