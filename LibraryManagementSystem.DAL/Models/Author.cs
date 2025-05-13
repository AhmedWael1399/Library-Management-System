using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DAL.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }  

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }    

        [MaxLength(20)]
        public string? Website { get; set; }

        [MaxLength(100)]
        public string? Bio { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
