using LibraryManagementSystem.BLL.Interfaces;
using LibraryManagementSystem.DAL.Contexts;
using LibraryManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly LibraryManagementSystemDbContext _dbContext;

        public BookRepository(LibraryManagementSystemDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _dbContext.Books
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
        {
           return await _dbContext.Books.Where(b => !b.IsBorrowed).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBorrowedBooksAsync()
        {
            return await _dbContext.Books.Where(b => b.IsBorrowed).ToListAsync();
        }
    }
}
