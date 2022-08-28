using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Calculator.Data.Entities
{
    public partial class CmsContext : DbContext
    {
        public CmsContext()
        {
        }

        public CmsContext(DbContextOptions<CmsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<tbl_Category> tbl_Categories { get; set; }
        public virtual DbSet<tbl_Post> tbl_Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<tbl_Category>(entity =>
            {
                entity.HasKey(e => e.GenreId)
                    .HasName("PK_BookGenre");
            });

            modelBuilder.Entity<tbl_Post>(entity =>
            {
                entity.HasKey(e => e.BookId)
                    .HasName("PK_Book");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.tbl_Posts)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Book_BookGenre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
