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


        public async Task<IActionResult> Index(int page = 1)
        {
            var pagedTransactions = await _borrowingTransactionService.GetPaginatedTransactionsAsync(page, 5);
            return View(pagedTransactions);
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

        public async Task<IActionResult> BookStatusReport(string status, DateTime? from, DateTime? to, int page = 1)
        {
            var dtos = await _bookService.GetBookStatusReportAsync();

            if (!string.IsNullOrEmpty(status))
                dtos = dtos.Where(b => b.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

            if (from.HasValue)
                dtos = dtos.Where(b => b.BorrowedDate >= from.Value);

            if (to.HasValue)
                dtos = dtos.Where(b => b.ReturnedDate <= to.Value);

            var viewModels = dtos.Select(dto => new BookStatusViewModel
            {
                BookId = dto.BookId,
                Title = dto.Title,
                AuthorName = dto.AuthorName,
                Status = dto.Status,
                BorrowedDate = dto.BorrowedDate,
                ReturnedDate = dto.ReturnedDate
            });

            int pageSize = 5;
            var paged = viewModels.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(viewModels.Count() / (double)pageSize);

            return View(paged);
        }


    }
}
