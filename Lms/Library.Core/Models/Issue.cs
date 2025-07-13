using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Models
{
    public class Issue
    {
        public int IssueId { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public string BookTitle { get; set; }
        public string MemberName { get; set; }
    }

}
