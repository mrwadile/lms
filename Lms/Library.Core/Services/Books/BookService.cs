using Library.Core.Models;
using Library.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Task AddBookAsync(Book book)
        {
            try
            {
                return _bookRepository.AddAsync(book);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error adding book.", ex);
            }
        }

        public Task DeleteBookAsync(int id)
        {
            try
            {
                return _bookRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error deleting book with ID {id}.", ex);
            }
        }

        public Task<IEnumerable<Book>> GetAllBookAsync()
        {
            try
            {
                return _bookRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching books.", ex);
            }

        }

        public Task<Book> GetBookByIdAsync(int id)
        {
            try
            {
                return _bookRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error fetching book with ID {id}.", ex);
            }
        }

        public Task UpdateBookAsync(Book book)
        {
            try
            {
                return _bookRepository.UpdateAsync(book);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error updating book.", ex);
            }
        }
    }
}
