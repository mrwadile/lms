using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Models
{
    public class BookCategoryStats
    {
        public string Category { get; set; }
        public int BooksIssued { get; set; }
        public int TotalBooks { get; set; }
        public decimal Percentage { get; set; }
    }
}
