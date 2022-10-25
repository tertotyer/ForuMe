using ForuMe.Services.BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ForuMe.Services.BlogAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Blog> Blogs { get; set; }
    }
}
