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

        public Task AddBookAsync(Book member)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBookAsync(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Task UpdateBookAsync(Book member)
        {
            throw new NotImplementedException();
        }
    }
}
