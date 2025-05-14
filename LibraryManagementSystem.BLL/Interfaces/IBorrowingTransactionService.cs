using LibraryManagementSystem.BLL.Helpers;
using LibraryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Interfaces
{
    public interface IBorrowingTransactionService 
    {
        Task<IEnumerable<BorrowingTransaction>> GetAllTransactionsAsync();
        Task<BorrowingTransaction?> GetTransactionByIdAsync(int id);
        Task<bool> BorrowBookAsync(BorrowingTransaction transaction);
        Task<bool> ReturnBookAsync(int bookId);
        Task<PaginatedList<BorrowingTransaction>> GetPaginatedTransactionsAsync(int pageIndex, int pageSize);
    }
}
