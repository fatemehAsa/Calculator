using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Calculator.Data.Entities
{
    [Table("tbl_Category")]
    public partial class tbl_Category
    {
        public tbl_Category()
        {
            tbl_Posts = new HashSet<tbl_Post>();
        }

        [Key]
        public int GenreId { get; set; }
        [Required]
        [StringLength(150)]
        public string BookGenre { get; set; }
        [Required]
        [StringLength(500)]
        public string GenreDescription { get; set; }

        [InverseProperty(nameof(tbl_Post.Genre))]
        public virtual ICollection<tbl_Post> tbl_Posts { get; set; }
    }
}
