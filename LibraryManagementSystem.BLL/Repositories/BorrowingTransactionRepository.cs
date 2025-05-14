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
    public class BorrowingTransactionRepository : GenericRepository<BorrowingTransaction>, IBorrowingTransactionRepository
    {
        private readonly LibraryManagementSystemDbContext _dbContext;

        public BorrowingTransactionRepository(LibraryManagementSystemDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BorrowingTransaction?> GetActiveBorrowByBookIdAsync(int bookId)
        {
            return await _dbContext.BorrowingTransactions
            .FirstOrDefaultAsync(t => t.BookId == bookId && t.ReturnedDate == null);
        }
    }
}
