using LibraryManagementSystem.BLL.DTOs;
using LibraryManagementSystem.BLL.Helpers;
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

        public async Task<IEnumerable<Book>> GetBooksWithAuthorAndTransactionAsync()
        {
            return await _dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.BorrowingTransaction)
                .ToListAsync();
        }

        public async Task<PaginatedList<Book>> GetPaginatedAsync(int pageIndex, int pageSize)
        {
            return await PaginatedList<Book>.CreateAsync(
                _dbContext.Books.Include(b => b.Author).AsNoTracking(), pageIndex, pageSize);
        }

        public async Task<IEnumerable<BookStatusDto>> GetBookStatusDTOsAsync()
        {
            return await _dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.BorrowingTransaction)
                .Select(b => new BookStatusDto
                {
                    BookId = b.Id,
                    Title = b.Title,
                    AuthorName = b.Author.FullName,
                    Status = b.IsBorrowed ? "Borrowed" : "Available",
                    BorrowedDate = b.BorrowingTransaction != null ? b.BorrowingTransaction.BorrowedDate : null,
                    ReturnedDate = b.BorrowingTransaction != null ? b.BorrowingTransaction.ReturnedDate : null
                })
                .ToListAsync();
        }

        public async Task<PaginatedList<BookStatusDto>> GetBookStatusPaginatedAsync(int pageIndex, int pageSize)
        {
            var source = _dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.BorrowingTransaction)
                .Select(b => new BookStatusDto
                {
                    BookId = b.Id,
                    Title = b.Title,
                    AuthorName = b.Author.FullName,
                    Status = b.IsBorrowed ? "Borrowed" : "Available",
                    BorrowedDate = b.BorrowingTransaction!.BorrowedDate,
                    ReturnedDate = b.BorrowingTransaction!.ReturnedDate
                });

            return await PaginatedList<BookStatusDto>.CreateAsync(source, pageIndex, pageSize); 
        }


    }
}
