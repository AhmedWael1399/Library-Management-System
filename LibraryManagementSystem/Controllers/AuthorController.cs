using LibraryManagementSystem.BLL.Helpers;
using LibraryManagementSystem.BLL.Interfaces;
using LibraryManagementSystem.DAL.Models;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> Index(string searchTerm, int page = 1)
        {
            var paginated = await _authorService.SearchPaginatedAsync(searchTerm ?? "", page, 5);

            ViewBag.SearchTerm = searchTerm;
            return View(paginated);
        }


        public IActionResult CreateAuthor()
        {
            return View(new AuthorViewModel()); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var existing = await _authorService.GetAllAuthorsAsync();
            if (existing.Any(a => a.FullName == vm.FullName))
            {
                ModelState.AddModelError("FullName", "This full name is already taken.");
                return View(vm);
            }

            var author = new Author
            {
                FullName = vm.FullName,
                Email = vm.Email,
                Website = vm.Website,
                Bio = vm.Bio
            };

            await _authorService.AddAuthorAsync(author);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();

            var vm = new AuthorViewModel
            {
                Id = author.Id,
                FullName = author.FullName,
                Email = author.Email,
                Website = author.Website,
                Bio = author.Bio
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditAuthor(AuthorViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var author = new Author
            {
                Id = vm.Id,
                FullName = vm.FullName,
                Email = vm.Email,
                Website = vm.Website,
                Bio = vm.Bio
            };

            await _authorService.UpdateAuthorAsync(author);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.DeleteAuthorAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
