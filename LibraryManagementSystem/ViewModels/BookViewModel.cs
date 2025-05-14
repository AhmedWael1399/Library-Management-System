using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModels
{
    public class BookViewModel
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public int Genre { get; set; }  

        [MaxLength(300)]
        public string? Description { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public bool IsBorrowed { get; set; }

    }
}
