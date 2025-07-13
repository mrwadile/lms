using Library.Core.Models;
using Library.Core.Services.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.Web.Controllers.Reports
{
    public class ReportController : BaseController
    {
        // GET: Report
        private readonly IBookService _bookService;

        public ReportController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult> BookList()
        {
            var books = await _bookService.GetAllBookAsync();
            return View(books);
        }

        //GET: Book History
        [HttpGet]
        public async Task<ActionResult> BookHistory(int id)
        {
            var history = await _bookService.GetBookHistoryAsync(id);
            return View(history);
        }

        //GET: Overdue books
        [HttpGet]
        public async Task<ActionResult> OverdueBooks()
        {
            try
            {
                var overdueBooks = await _bookService.GetOverdueBooksAsync();
                var webModel = overdueBooks.Select(x => new Library.Web.Models.OverdueBook
                {
                    IssueId = x.IssueId,
                    BookTitle = x.BookTitle,
                    MemberName = x.MemberName,
                    IssueDate = x.IssueDate,
                    DueDate = x.DueDate,
                    DaysOverdue = x.DaysOverdue
                }).ToList();

                return View(webModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Something went wrong while loading overdue books page.";
                return View("Error");
            }
        }
    }
}