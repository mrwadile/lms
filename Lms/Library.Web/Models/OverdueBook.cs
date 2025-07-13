using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Web.Models
{
    public class OverdueBook
    {
        public int IssueId { get; set; }
        public string BookTitle { get; set; }
        public string MemberName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public int DaysOverdue { get; set; }
    }
}