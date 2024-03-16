using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiddleLayer.Identity.Configuration;
using MiddleLayer.Identity.Models;

namespace MiddleLayer.Identity
{
    public class MiddleLayerIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public MiddleLayerIdentityDbContext(DbContextOptions<MiddleLayerIdentityDbContext> options)
            : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
