﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForuMe.Services.BlogAPI.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Text { get; set; }
        [Required]
        public string Author { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
