using Library.Core.Models;
using Library.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Core.Services.Books
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Adds a new book asynchronously.
        /// </summary>
        public async Task AddBookAsync(Book book)
        {
            try
            {
                await _bookRepository.AddAsync(book);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while adding the book.", ex);
            }
        }

        /// <summary>
        /// Deletes a book by ID asynchronously.
        /// </summary>
        public async Task DeleteBookAsync(int id)
        {
            try
            {
                await _bookRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occurred while deleting the book with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Retrieves all books asynchronously.
        /// </summary>
        public async Task<IEnumerable<Book>> GetAllBookAsync()
        {
            try
            {
                return await _bookRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while fetching all books.", ex);
            }
        }

        /// <summary>
        /// Retrieves a book by ID asynchronously.
        /// </summary>
        public async Task<Book> GetBookByIdAsync(int id)
        {
            try
            {
                return await _bookRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occurred while fetching the book with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Updates a book asynchronously.
        /// </summary>
        public async Task UpdateBookAsync(Book book)
        {
            try
            {
                await _bookRepository.UpdateAsync(book);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while updating the book.", ex);
            }
        }
    }
}
