using LibraryManagementSystem.BLL.Helpers;
using LibraryManagementSystem.BLL.Interfaces;
using LibraryManagementSystem.DAL.Models;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;

        public BookController(IBookService bookService, IAuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var paginatedBooks = await _bookService.GetPaginatedBooksAsync(page, 5);
            return View(paginatedBooks);
        }

        public async Task<IActionResult> CreateBook()
        {
            ViewBag.Authors = await _authorService.GetAllAuthorsAsync();
            return View(new BookViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(BookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _authorService.GetAllAuthorsAsync();
                return View(vm);
            }

            var book = new Book
            {
                Title = vm.Title,
                Genre = (Book.BGenre)vm.Genre,
                Description = vm.Description,
                AuthorId = vm.AuthorId,
                IsBorrowed = false
            };

            await _bookService.AddBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            var vm = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Genre = (int)book.Genre,
                Description = book.Description,
                AuthorId = book.AuthorId,
                IsBorrowed = book.IsBorrowed
            };

            ViewBag.Authors = await _authorService.GetAllAuthorsAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(BookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _authorService.GetAllAuthorsAsync();
                return View(vm);
            }

            var book = new Book
            {
                Id = vm.Id,
                Title = vm.Title,
                Genre = (Book.BGenre)vm.Genre,
                Description = vm.Description,
                AuthorId = vm.AuthorId,
                IsBorrowed = vm.IsBorrowed
            };

            await _bookService.UpdateBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> BookStatusReport()
        {
            var dtos = await _bookService.GetBookStatusReportAsync();

            var viewModels = dtos.Select(dto => new BookStatusViewModel
            {
                BookId = dto.BookId,
                Title = dto.Title,
                AuthorName = dto.AuthorName,
                Status = dto.Status,
                BorrowedDate = dto.BorrowedDate,
                ReturnedDate = dto.ReturnedDate
            });

            return View(viewModels);
        }

    }
}
