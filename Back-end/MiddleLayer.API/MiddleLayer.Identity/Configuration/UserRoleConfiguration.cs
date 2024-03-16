using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MiddleLayer.Identity.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "ecb0aa14-0cab-48a4-a604-46d05c60c3cc",
                    UserId = "e993f47a-9c29-4e45-9ab6-d086d4dd3704"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "8752fca0-2b11-4087-8825-a50cf31cc200",
                    UserId = "704aea0a-20b0-4792-9aa7-b28f0b29e580"
                });
        }
    }
}
