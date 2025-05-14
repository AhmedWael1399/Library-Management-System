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
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly LibraryManagementSystemDbContext _dbContext;
        public AuthorRepository(LibraryManagementSystemDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Author?> GetAuthorWithBooksAsync(int id)
        {
            return await _dbContext.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
