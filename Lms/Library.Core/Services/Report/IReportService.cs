using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Services.Report
{
    public interface IReportService
    {
        Task<Dictionary<string, int>> GetBooksIssuedLast7DaysAsync();
        Task<List<BookCategoryStats>> GetBooksIssuedByCategoryAsync();
        DashboardStats GetDashboardStats();
    }
}
