using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.Models
{
    public class Book
    {
        public enum BGenre
        {
            Unknown, Adventure, Mystery, Thriller, Romance, SciFi, Fantasy,
            Biography, History, SelfHelp, Children, YoungAdult, Poetry, Drama, NonFiction
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public BGenre Genre { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }

        public bool IsBorrowed { get; set; }

        public BorrowingTransaction? BorrowingTransaction { get; set; }
    }
}
