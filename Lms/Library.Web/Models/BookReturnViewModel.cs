using System.Collections.Generic;

namespace Library.Web.ViewModels
{
    public class BookReturnViewModel
    {
        public List<IssueDropdownDto> IssuedBooks { get; set; }
    }

    public class IssueDropdownDto
    {
        public int IssueId { get; set; }
        public string DisplayText { get; set; }
    }
}
