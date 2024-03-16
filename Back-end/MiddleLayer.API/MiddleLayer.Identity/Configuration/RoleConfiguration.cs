using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MiddleLayer.Identity.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "ecb0aa14-0cab-48a4-a604-46d05c60c3cc",
                    Name = RoleConstants.User,
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = "8752fca0-2b11-4087-8825-a50cf31cc200",
                    Name = RoleConstants.Administrator,
                    NormalizedName = "ADMINISTRATOR"
                });
        }
    }
}
