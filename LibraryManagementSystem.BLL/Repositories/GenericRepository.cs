using LibraryManagementSystem.BLL.Interfaces;
using LibraryManagementSystem.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LibraryManagementSystemDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(LibraryManagementSystemDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>(); 
        }

        public async Task AddAsync(T item)
        {
            await _dbSet.AddAsync(item);
        }

        //public async Task AddRangeAsync(IEnumerable<T> items)
        //{
        //    await _dbSet.AddRangeAsync(items);
        //}

        //public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        //{
        //    return await _dbSet.Where(predicate).ToListAsync();
        //}

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync(); 
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
        }

        //public void RemoveRange(IEnumerable<T> items)
        //{
        //    _dbSet.RemoveRange(items);
        //}

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public void Update(T item)
        {
            _dbSet.Update(item);
        }
    }
}
