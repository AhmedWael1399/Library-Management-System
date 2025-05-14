using LibraryManagementSystem.BLL.Interfaces;
using LibraryManagementSystem.BLL.Repositories;
using LibraryManagementSystem.DAL.Contexts;
using LibraryManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<bool> AddBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return false;
            _bookRepository.Remove(book);
            await _bookRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Book>> GetAvailableAsync()
        {
            return await _bookRepository.GetAvailableBooksAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
           return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            _bookRepository.Update(book);
            await _bookRepository.SaveChangesAsync();
            return true;
        }
    }
}
