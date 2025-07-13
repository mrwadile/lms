using Library.Core.Data;
using Library.Core.Models;
using Library.Core.SharedResource;
using Library.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Repositories
{
    public class BookRepository : IBookRepository
    {
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var books = new List<Book>();

            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = new SqlCommand("sp_GetAllBooks", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            books.Add(new Book
                            {
                                BookId = Convert.ToInt32(reader["BookId"]),
                                Title = reader["Title"].ToString(),
                                Author = reader["Author"].ToString(),
                                ISBN = reader["ISBN"].ToString(),
                                TotalCopies = Convert.ToInt32(reader["TotalCopies"]),
                                AvailableCopies = Convert.ToInt32(reader["AvailableCopies"]),
                                Category = reader["Category"].ToString()

                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(SharedResources.ErrorWhileFetchingBooks, ex);
            }

            return books;
        }

        public async Task<Book> GetByIdAsync(int bookId)
        {
            Book book = null;

            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = new SqlCommand("sp_GetBookById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            book = new Book
                            {
                                BookId = Convert.ToInt32(reader["BookId"]),
                                Title = reader["Title"].ToString(),
                                Author = reader["Author"].ToString(),
                                ISBN = reader["ISBN"].ToString(),
                                TotalCopies = Convert.ToInt32(reader["TotalCopies"]),
                                AvailableCopies = Convert.ToInt32(reader["AvailableCopies"]),
                                Category = reader["Category"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{SharedResources.ErrorWhileFetchingBookWithId} {bookId}.", ex);
            }

            return book;
        }

        public async Task AddAsync(Book book)
        {
            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = new SqlCommand("sp_AddBook", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Title", book.Title);
                    cmd.Parameters.AddWithValue("@Author", book.Author);
                    cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
                    cmd.Parameters.AddWithValue("@TotalCopies", book.TotalCopies);
                    cmd.Parameters.AddWithValue("@AvailableCopies", book.AvailableCopies);
                    cmd.Parameters.AddWithValue("@Category", book.Category);

                    var result = await cmd.ExecuteScalarAsync();
                    book.BookId = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(SharedResources.ErrorWhileAddingBook, ex);
            }
        }

        public async Task UpdateAsync(Book book)
        {
            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = new SqlCommand("sp_UpdateBook", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", book.BookId);
                    cmd.Parameters.AddWithValue("@Title", book.Title);
                    cmd.Parameters.AddWithValue("@Author", book.Author);
                    cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
                    cmd.Parameters.AddWithValue("@TotalCopies", book.TotalCopies);
                    cmd.Parameters.AddWithValue("@AvailableCopies", book.AvailableCopies);
                    cmd.Parameters.AddWithValue("@Category", book.Category);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{SharedResources.ErrorWhileupdatingBook} {book.BookId}.", ex);
            }
        }

        public async Task DeleteAsync(int bookId)
        {
            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = new SqlCommand("sp_DeleteBook", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{SharedResources.ErrorWhileDeletingingBook} {bookId}.", ex);
            }
        }

        public async Task<string> IssueBookAsync(int bookId, int memberId)
        {
            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = new SqlCommand("sp_IssueBook", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@MemberId", memberId);
                    cmd.Parameters.Add("@ResultMessage", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output;
                    await cmd.ExecuteNonQueryAsync();

                    return cmd.Parameters["@ResultMessage"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{SharedResources.ErrorWhileIssueBook} {bookId} and member {memberId}.", ex);
            }
        }

        public async Task<string> ReturnBookAsync(int issueId)
        {
            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = new SqlCommand("sp_ReturnBook", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IssueId", issueId);
                    cmd.Parameters.Add("@ResultMessage", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output;
                    await cmd.ExecuteNonQueryAsync();
                    return cmd.Parameters["@ResultMessage"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{SharedResources.ErrorWhileReturn} {issueId}.", ex);
            }
        }

        public async Task<IEnumerable<Issue>> GetUnreturnedIssuesAsync()
        {
            var issues = new List<Issue>();

            using (var conn = DbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("sp_GetUnreturnedIssues", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        issues.Add(new Issue
                        {
                            IssueId = Convert.ToInt32(reader["IssueId"]),
                            BookId = Convert.ToInt32(reader["BookId"]),
                            MemberId = Convert.ToInt32(reader["MemberId"]),
                            IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                            DueDate = Convert.ToDateTime(reader["DueDate"]),
                            BookTitle = reader["BookTitle"].ToString(),
                            MemberName = reader["MemberName"].ToString()
                        });
                    }
                }
            }

            return issues;
        }

        public async Task<List<OverdueBook>> GetOverdueBooksAsync()
        {
            try
            {
                var result = new List<OverdueBook>();

                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = new SqlCommand("sp_ReportOverdueBookMaster", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new OverdueBook
                            {
                                IssueId = Convert.ToInt32(reader["IssueId"]),
                                BookTitle = reader["BookTitle"].ToString(),
                                MemberName = reader["MemberName"].ToString(),
                                IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                                DueDate = Convert.ToDateTime(reader["DueDate"]),
                                DaysOverdue = Convert.ToInt32(reader["DaysOverdue"])
                            });
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<BookHistory>> GetBookHistoryAsync(int bookId)
        {
            var result = new List<BookHistory>();

            using (var conn = DbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("sp_Report_BookHistory", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", bookId);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new BookHistory
                        {
                            IssueId = Convert.ToInt32(reader["IssueId"]),
                            MemberName = reader["MemberName"].ToString(),
                            IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                            DueDate = Convert.ToDateTime(reader["DueDate"]),
                            ReturnDate = reader["ReturnDate"] == DBNull.Value ? null : (DateTime?)reader["ReturnDate"],
                            FineAmount = reader["FineAmount"] == DBNull.Value ? null : (decimal?)reader["FineAmount"]
                        });
                    }
                }
            }
            return result;
        }

    }
}
