namespace LibraryManagementSystem.ViewModels
{
    public class BookStatusViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Status { get; set; } 
        public DateTime? BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
