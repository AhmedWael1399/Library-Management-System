using LibraryManagementSystem.BLL.Interfaces;
using LibraryManagementSystem.DAL.Models;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BorrowingTransactionController : Controller
    {
        private readonly IBorrowingTransactionService _borrowingTransactionService;
        private readonly IBookService _bookService;

        public BorrowingTransactionController(IBorrowingTransactionService borrowingTransactionService, IBookService bookService)
        {
            _borrowingTransactionService = borrowingTransactionService;
            _bookService = bookService;
        }


        public async Task<IActionResult> Index()
        {
            var transactions = await _borrowingTransactionService.GetAllTransactionsAsync();
            return View(transactions);
        }

        public async Task<IActionResult> BorrowBook()
        {
            ViewBag.AvailableBooks = await _bookService.GetAvailableAsync();
            return View(new BorrowingTransactionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> BorrowBook(BorrowingTransactionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AvailableBooks = await _bookService.GetAvailableAsync();
                vm = new BorrowingTransactionViewModel
                {
                    BorrowedDate = DateTime.Today
                };

                return View(vm);
            }

            var transaction = new BorrowingTransaction
            {
                BookId = vm.BookId,
                BorrowedDate = vm.BorrowedDate
            };

            var success = await _borrowingTransactionService.BorrowBookAsync(transaction);
            if (!success)
            {
                ModelState.AddModelError("", "Book already borrowed or invalid.");
                ViewBag.AvailableBooks = await _bookService.GetAvailableAsync();
                return View(vm);
            }

            return RedirectToAction("Index", "Book");
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(int bookId)
        {
            var success = await _borrowingTransactionService.ReturnBookAsync(bookId);
            if (!success)
                return BadRequest("Invalid return attempt.");

            return RedirectToAction("Index", "Book");
        }
    }
}
