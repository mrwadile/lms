using Library.Core.Data;
using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Services.Report
{
    public class ReportService : IReportService
    {
        public async Task<Dictionary<string, int>> GetBooksIssuedLast7DaysAsync()
        {
            var result = new Dictionary<string, int>();

            using (var conn = DbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("sp_Report_BooksIssuedLast7Days", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string day = reader["DayOfWeek"].ToString();
                        int count = Convert.ToInt32(reader["IssuedCount"]);
                        result[day] = count;
                    }
                }
            }

            return result;
        }

        public async Task<List<BookCategoryStats>> GetBooksIssuedByCategoryAsync()
        {
            var result = new List<BookCategoryStats>();

            using (var conn = DbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("sp_Report_BookMasterIssuedByCategory", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new BookCategoryStats
                        {
                            Category = reader["Category"].ToString(),
                            BooksIssued = Convert.ToInt32(reader["BooksIssued"]),
                            TotalBooks = Convert.ToInt32(reader["TotalBooks"]),
                            Percentage = Convert.ToDecimal(reader["Percentage"])
                        });
                    }
                }
            }
            return result;
        }


        public DashboardStats GetDashboardStats()
        {
            var stats = new DashboardStats();

            using (var conn = DbConnectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("sp_GetDashboardStats", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        stats.TotalBooks = Convert.ToInt32(reader["TotalBooks"]);
                        stats.AvailableBooks = Convert.ToInt32(reader["AvailableBooks"]);
                        stats.IssuedBooks = Convert.ToInt32(reader["IssuedBooks"]);
                        stats.OverdueBooks = Convert.ToInt32(reader["OverdueBooks"]);
                    }
                }
            }
            return stats;
        }
    }
}
