using LibraryManagementSystem.BLL.DTOs;
using LibraryManagementSystem.BLL.Helpers;
using LibraryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetAvailableBooksAsync();
        Task<IEnumerable<Book>> GetBorrowedBooksAsync();
        Task<IEnumerable<Book>> GetBooksWithAuthorAndTransactionAsync();
        Task<PaginatedList<Book>> GetPaginatedAsync(int pageIndex, int pageSize);
        Task<IEnumerable<BookStatusDto>> GetBookStatusDTOsAsync();
        Task<PaginatedList<BookStatusDto>> GetBookStatusPaginatedAsync(int pageIndex, int pageSize);




    }
}
