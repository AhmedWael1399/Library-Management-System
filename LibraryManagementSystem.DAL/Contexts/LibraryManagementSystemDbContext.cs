using LibraryManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.Contexts
{
    public class LibraryManagementSystemDbContext : DbContext
    {
        public LibraryManagementSystemDbContext(DbContextOptions<LibraryManagementSystemDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowingTransaction> BorrowingTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>()
                .HasIndex(a => a.FullName)
                .IsUnique();

            modelBuilder.Entity<Author>()
                .HasIndex(a => a.Email)
                .IsUnique();

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BorrowingTransaction>()
                .HasOne(bt => bt.Book)
                .WithOne(b => b.BorrowingTransaction)
                .HasForeignKey<BorrowingTransaction>(bt => bt.BookId);
        }

    }
}
