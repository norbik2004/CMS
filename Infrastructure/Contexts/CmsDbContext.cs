using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class CmsDbContext : IdentityDbContext<AppUser>
    {
        public CmsDbContext(DbContextOptions<CmsDbContext> options) : base(options)
        {

        }
    }
}
