using Microsoft.AspNetCore.Identity;

namespace ForuMe.Services.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public double Level { get; set; }
    }
}
