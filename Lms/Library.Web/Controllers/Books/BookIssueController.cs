using Library.Core.Services.Books;
using Library.Core.Services.Members;
using Library.Web.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Library.Web.Controllers.Books
{
    public class BookIssueController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly IMemberService _memberService;

        public BookIssueController(IBookService bookService, IMemberService memberService)
        {
            _bookService = bookService;
            _memberService = memberService;
        }

        // GET: Issue Book Form
        [HttpGet]
        public async Task<ActionResult> Issue()
        {
            try
            {
                var books = await _bookService.GetAllBookAsync();
                var members = await _memberService.GetAllMemberAsync();

                var model = new BookIssueViewModel
                {
                    AvailableBooks = books.Where(b => b.AvailableCopies > 0).ToList(),
                    Members = members.ToList()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                // Log error (if logger available)
                ViewBag.Error = "Something went wrong while loading issue page.";
                return View("Error");
            }
        }

        // POST: Issue Book
        [HttpPost]
        public async Task<JsonResult> Issue(int BookId, int MemberId)
        {
            try
            {
                var message = await _bookService.IssueBookAsync(BookId, MemberId);
                bool success = message.Contains("successfully");

                return Json(new { success, message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An unexpected error occurred during issuing book." });
            }
        }

        // GET: Return Book Form
        [HttpGet]
        public async Task<ActionResult> Return()
        {
            try
            {
                var issues = await _bookService.GetUnreturnedIssuesAsync();
                var model = new BookReturnViewModel
                {
                    IssuedBooks = issues.Select(i => new IssueDropdownDto
                    {
                        IssueId = i.IssueId,
                        DisplayText = $"Book: {i.BookTitle.ToString()} | Member: {i.MemberName.ToString()}"
                    }).ToList()
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Something went wrong while loading return page." + ex;
                return View("Error");
            }
        }

        // POST: Return Book
        [HttpPost]
        public async Task<JsonResult> Return(int issueId)
        {
            try
            {
                var message = await _bookService.ReturnBookAsync(issueId);
                bool success = message.Contains("successfully");

                return Json(new { success, message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An unexpected error occurred while returning book." });
            }
        }
        
    }
}
