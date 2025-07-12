using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Models
{
    public class DashboardStats
    {
        public int TotalBooks { get; set; }
        public int AvailableBooks { get; set; }
        public int IssuedBooks { get; set; }
        public int OverdueBooks { get; set; }
    }

}
