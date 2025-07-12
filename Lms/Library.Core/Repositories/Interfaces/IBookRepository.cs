using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int bookId);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int bookId);
    }
}
