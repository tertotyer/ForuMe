using ForuMe.Services.BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ForuMe.Services.BlogAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasOne<Category>(x => x.Category)
                .WithMany(x => x.Blogs)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<Article>()
                .HasOne<Blog>(x => x.Blog)
                .WithMany(x => x.Articles)
                .HasForeignKey(x => x.BlogId);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}
