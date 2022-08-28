using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Calculator.Data.Entities
{
    [Table("tbl_Post")]
    public partial class tbl_Post
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        [StringLength(150)]
        public string BookTitle { get; set; }
        [Required]
        public string BookDescription { get; set; }
        public int GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        [InverseProperty(nameof(tbl_Category.tbl_Posts))]
        public virtual tbl_Category Genre { get; set; }
    }
}
