using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModels
{
    public class BorrowingTransactionViewModel
    {

        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BorrowedDate { get; set; }

        public DateTime? ReturnedDate { get; set; }

    }
}
