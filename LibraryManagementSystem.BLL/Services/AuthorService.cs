﻿using LibraryManagementSystem.BLL.Helpers;
using LibraryManagementSystem.BLL.Interfaces;
using LibraryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _authorRepository.GetAuthorWithBooksAsync(id);
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

        public async Task<PaginatedList<Author>> SearchPaginatedAsync(string searchTerm, int pageIndex, int pageSize)
        {
            return await _authorRepository.SearchPaginatedAsync(searchTerm, pageIndex, pageSize);
        }


    }
}
