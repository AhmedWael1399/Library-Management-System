using LibraryManagementSystem.BLL.Interfaces;
using LibraryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IGenericRepository<Author> _authorRepository;

        public AuthorRepository(IGenericRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _authorRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddAuthorAsync(Author author)
        {
            await _authorRepository.AddAsync(author);
            return await _authorRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            _authorRepository.Update(author);
            return await _authorRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) return false;

            _authorRepository.Remove(author);
            return await _authorRepository.SaveChangesAsync();
        }

    }
}
