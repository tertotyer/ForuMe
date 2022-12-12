using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ForuMe.Services.BlogAPI.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
