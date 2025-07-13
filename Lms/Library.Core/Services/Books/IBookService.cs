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
        Task<string> IssueBookAsync(int bookId, int memberId);
        Task<string> ReturnBookAsync(int issueId);
        Task<IEnumerable<Issue>> GetUnreturnedIssuesAsync();
        Task<List<OverdueBook>> GetOverdueBooksAsync();
        Task<List<BookHistory>> GetBookHistoryAsync(int bookId);
        bool HasIssuedRecords(int bookId);

    }
}
