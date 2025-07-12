using Library.Core.Data;
using Library.Core.Models;
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
                throw new ApplicationException("Error fetching books.", ex);
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
                throw new ApplicationException($"Error retrieving book with ID {bookId}.", ex);
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
                throw new ApplicationException("Error adding book.", ex);
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
                throw new ApplicationException($"Error updating book with ID {book.BookId}.", ex);
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
                throw new ApplicationException($"Error deleting book with ID {bookId}.", ex);
            }
        }
    }
}
