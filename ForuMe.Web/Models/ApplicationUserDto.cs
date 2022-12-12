using Microsoft.AspNetCore.Identity;

namespace ForuMe.Web.Models
{
    public class ApplicationUserDto : IdentityUser
    {
        public double Level { get; set; }
    }
}
