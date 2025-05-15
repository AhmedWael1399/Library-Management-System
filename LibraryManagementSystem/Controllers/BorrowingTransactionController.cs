using LibraryManagementSystem.BLL.DTOs;
using LibraryManagementSystem.BLL.Helpers;
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

            return RedirectToAction("Index", "BorrowingTransaction");
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(int bookId)
        {
            var success = await _borrowingTransactionService.ReturnBookAsync(bookId);
            if (!success)
                return BadRequest("Invalid return attempt.");

            return RedirectToAction("Index", "BorrowingTransaction");
        }

        public async Task<IActionResult> BookStatusReport(string status, DateTime? borrowDate, DateTime? returnDate, int page = 1)
        {
            var dtos = await _bookService.GetBookStatusReportAsync(); 

            if (!string.IsNullOrEmpty(status))
                dtos = dtos.Where(b => b.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

            if (borrowDate.HasValue)
                dtos = dtos.Where(b => b.BorrowedDate.HasValue && b.BorrowedDate.Value.Date >= borrowDate.Value.Date);

            if (returnDate.HasValue)
                dtos = dtos.Where(b => b.ReturnedDate.HasValue && b.ReturnedDate.Value.Date <= returnDate.Value.Date);

            int pageSize = 5;
            var total = dtos.Count();
            var pagedDtos = dtos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModels = pagedDtos.Select(dto => new BookStatusViewModel
            {
                BookId = dto.BookId,
                Title = dto.Title,
                AuthorName = dto.AuthorName,
                Status = dto.Status,
                BorrowedDate = dto.BorrowedDate,
                ReturnedDate = dto.ReturnedDate
            }).ToList();

            var paginatedVM = new PaginatedList<BookStatusViewModel>(viewModels, total, page, pageSize);

            ViewBag.Status = status;
            ViewBag.BorrowDate = borrowDate?.ToString("yyyy-MM-dd");
            ViewBag.ReturnDate = returnDate?.ToString("yyyy-MM-dd");

            return View(paginatedVM);
        }


    }
}
