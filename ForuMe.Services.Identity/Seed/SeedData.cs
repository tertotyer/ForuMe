using ForuMe.Services.Identity.DbContexts;
using ForuMe.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ForuMe.Services.Identity.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context =
                new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (context == null || context.Roles == null)
            {
                throw new ArgumentNullException("Null context");
            }

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new IdentityRole { Name = SD.Admin, NormalizedName = SD.Admin.ToUpper() },
                    new IdentityRole { Name = SD.Customer, NormalizedName = SD.Customer.ToUpper() }
                    );
            }

            context.SaveChanges();
        }
    }
}
