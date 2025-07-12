using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Models
{
    public class DashboardData
    {
        public DashboardStats Stats { get; set; }
        public List<BookCategoryStats> Categories { get; set; }
        public List<RecentIssueStats> RecentIssues { get; set; }
        public List<ChartData> BarChartData { get; set; }
        public List<ChartData> PieChartData { get; set; }
    }
}
