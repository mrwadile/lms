using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Services.Books
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBookAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task AddBookAsync(Book member);
        Task UpdateBookAsync(Book member);
        Task DeleteBookAsync(int id);
    }
}
