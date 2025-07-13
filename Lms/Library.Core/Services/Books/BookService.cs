using Library.Core.Models;
using Library.Core.Repositories.Interfaces;
using Library.Core.SharedResource;
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
                throw new ApplicationException(SharedResources.ErrorWhileAddingBook, ex);
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
                throw new ApplicationException($"{SharedResources.ErrorWhileDeletingingBook} {id}.", ex);
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
                throw new ApplicationException(SharedResources.ErrorWhileFetchingBooks, ex);
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
                throw new ApplicationException($"{SharedResources.ErrorWhileFetchingBookWithId} {id}.", ex);
            }
        }

        /// <summary>
        /// Ovedues book.
        /// </summary>
        public Task<List<OverdueBook>> GetOverdueBooksAsync()
        {
            try
            {
                return _bookRepository.GetOverdueBooksAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(SharedResources.ErrorWhileOverdueBooks, ex);
            }
        }

        /// <summary>
        /// All Issue book.
        /// </summary>
        public async Task<IEnumerable<Issue>> GetUnreturnedIssuesAsync()
        {
            try
            {
                return await _bookRepository.GetUnreturnedIssuesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(SharedResources.ErrorWhileFetchingUnretrnedIssuesBoks, ex);
            }
        }

        /// <summary>
        /// Issue book.
        /// </summary>
        public async Task<string> IssueBookAsync(int bookId, int memberId)
        {
            try
            {
                return await _bookRepository.IssueBookAsync(bookId, memberId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{SharedResources.ErrorWhileIssueBook} {bookId} to member with ID {memberId}.", ex);
            }
        }

        /// <summary>
        /// return book.
        /// </summary>
        public async Task<string> ReturnBookAsync(int issueId)
        {
            try
            {
                return await _bookRepository.ReturnBookAsync(issueId);
            }
            catch (Exception ex)
            {
                return $"{SharedResources.ErrorWhileReturnBook} {ex.Message}";
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
                throw new ApplicationException(SharedResources.ErrorWhileupdatingBook, ex);
            }
        }

        /// <summary>
        /// Book history.
        /// </summary>
        public async Task<List<BookHistory>> GetBookHistoryAsync(int bookId)
        {
            try
            {
                return await _bookRepository.GetBookHistoryAsync(bookId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{SharedResources.ErrorWhileFetchingHistoryBooks} {bookId}.", ex);
            }
        }

    }
}
