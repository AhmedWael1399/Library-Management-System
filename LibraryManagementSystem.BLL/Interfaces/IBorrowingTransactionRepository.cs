using LibraryManagementSystem.BLL.Helpers;
using LibraryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Interfaces
{
    public interface IBorrowingTransactionRepository : IGenericRepository<BorrowingTransaction>
    {
        Task<IEnumerable<BorrowingTransaction>> GetAllWithBooksAsync();
        Task<BorrowingTransaction?> GetActiveBorrowByBookIdAsync(int bookId);
        Task<PaginatedList<BorrowingTransaction>> GetPaginatedTransactionsAsync(int pageIndex, int pageSize);
    }
}
