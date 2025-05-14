using LibraryManagementSystem.BLL.Helpers;
using LibraryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<Author?> GetAuthorWithBooksAsync(int id);
        Task<PaginatedList<Author>> SearchPaginatedAsync(string searchTerm, int pageIndex, int pageSize);
    }
}
