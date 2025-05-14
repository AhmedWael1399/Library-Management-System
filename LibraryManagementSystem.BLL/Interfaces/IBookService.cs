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
    public interface IBookService 
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> GetAvailableAsync();
        Task<bool> AddBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
        Task<IEnumerable<BookStatusDto>> GetBookStatusReportAsync();
        Task<PaginatedList<Book>> GetPaginatedBooksAsync(int pageIndex, int pageSize);
        Task<PaginatedList<BookStatusDto>> GetBookStatusPaginatedAsync(int pageIndex, int pageSize);


    }
}
