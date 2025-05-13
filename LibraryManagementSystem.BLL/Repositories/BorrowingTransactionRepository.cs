using LibraryManagementSystem.BLL.Interfaces;
using LibraryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Repositories
{
    public class BorrowingTransactionRepository : IBorrowingTransaction
    {
        private readonly IGenericRepository<BorrowingTransaction> _borrowingTransactionRepository;
        private readonly IGenericRepository<Book> _bookRepository;

        public BorrowingTransactionRepository(IGenericRepository<BorrowingTransaction> borrowingTransactionRepository, IGenericRepository<Book> bookRepo)
        {
            _borrowingTransactionRepository = borrowingTransactionRepository;
            _bookRepository = bookRepo;
        }

        public async Task<IEnumerable<BorrowingTransaction>> GetAllTransactionsAsync()
        {
            return await _borrowingTransactionRepository.GetAllAsync();
        }

        public async Task<BorrowingTransaction?> GetTransactionByIdAsync(int id)
        {
            return await _borrowingTransactionRepository.GetByIdAsync(id);
        }

        public async Task<bool> BorrowBookAsync(BorrowingTransaction transaction)
        {
            var book = await _bookRepository.GetByIdAsync(transaction.BookId);
            if (book == null || book.IsBorrowed) return false;

            book.IsBorrowed = true;
            await _borrowingTransactionRepository.AddAsync(transaction);
            _bookRepository.Update(book);

            return await _borrowingTransactionRepository.SaveChangesAsync();
        }

        public async Task<bool> ReturnBookAsync(int bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null || !book.IsBorrowed) return false;

            var transaction = (await _borrowingTransactionRepository.FindAsync(t => t.BookId == bookId && t.ReturnedDate == null)).FirstOrDefault();
            if (transaction == null) return false;

            transaction.ReturnedDate = DateTime.Now;
            book.IsBorrowed = false;

            _borrowingTransactionRepository.Update(transaction);
            _bookRepository.Update(book);

            return await _borrowingTransactionRepository.SaveChangesAsync();
        }
    }
}
