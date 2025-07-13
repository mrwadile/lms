using Library.Core.Models;
using System.Collections.Generic;

namespace Library.Web.ViewModels
{
    public class BookIssueViewModel
    {
        public List<Book> AvailableBooks { get; set; }
        public List<Member> Members { get; set; }
    }
}
