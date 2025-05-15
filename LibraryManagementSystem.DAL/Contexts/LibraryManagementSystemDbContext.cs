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
                .WithMany(b => b.BorrowingTransactions)
                .HasForeignKey(bt => bt.BookId);



            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    FullName = "John Michael Smith Doe",
                    Email = "john.doe@example.com",
                    Website = "https://johndoe.com",
                    Bio = "Author of thrilling mystery novels."
                },
                new Author
                {
                    Id = 2,
                    FullName = "Jane Mary Elizabeth Carter",
                    Email = "jane.carter@example.com",
                    Website = null,
                    Bio = "Writes science fiction and fantasy books."
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "The Great Mystery",
                    Genre = Book.BGenre.Mystery,
                    Description = "A thrilling mystery novel.",
                    AuthorId = 1,
                    IsBorrowed = true
                },
                new Book
                {
                    Id = 2,
                    Title = "The Future World",
                    Genre = Book.BGenre.SciFi,
                    Description = "Exploring the future of humanity.",
                    AuthorId = 2,
                    IsBorrowed = false
                }
            );

            modelBuilder.Entity<BorrowingTransaction>().HasData(
                new BorrowingTransaction
                {
                    Id = 1,
                    BookId = 1,
                    BorrowedDate = new DateTime(2024, 5, 1),
                    ReturnedDate = null
                }
            );




        }

    }
}
