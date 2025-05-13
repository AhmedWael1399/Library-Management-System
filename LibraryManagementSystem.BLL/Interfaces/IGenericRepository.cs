using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T item);
        Task AddRangeAsync(IEnumerable<T> items);

        void Update(T item);

        void Remove(T item);
        void RemoveRange(IEnumerable<T> items);

        Task<bool> SaveChangesAsync();
    }
}
