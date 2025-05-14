using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModels
{
    public class AuthorViewModel
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^(\w{2,}\s){3}\w{2,}$", ErrorMessage = "Must contain four names, each at least 2 characters.")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        [Url]
        public string? Website { get; set; }

        [MaxLength(300)]
        public string? Bio { get; set; }

    }
}
