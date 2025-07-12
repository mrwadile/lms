using Library.Core.Models;
using Library.Core.Repositories.Interfaces;
using Library.Core.Services.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.Web.Controllers.Books
{
    public class BookController : BaseController
    {
            private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: Book
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var books = await _bookService.GetAllBookAsync();
            return View(books);
        }

        // GET: Book/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        // POST: Book/Create
        [HttpPost]
        public async Task<JsonResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.AddBookAsync(book);
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Book") });
            }
            return Json(new { success = false });
        }

        //GET: Book/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost]
        public async Task<JsonResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.UpdateBookAsync(book);
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Book") });
            }
            return Json(new { success = false });
        }

        // GET: Book/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id, FormCollection collection)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Book") });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: Book/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            // Return full view for normal page requests (refresh/direct navigation)
            return View(book);
        }

        // GET: Book/DetailsPartial/5 - For AJAX requests
        [HttpGet]
        public async Task<ActionResult> DetailsPartial(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            // Return partial view for AJAX calls
            return PartialView("_BookDetails", book);
        }

        [HttpGet]
        public async Task<JsonResult> GetBookDetails(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return Json(new { success = false, message = "Book not found" }, JsonRequestBehavior.AllowGet);
            return Json(book, JsonRequestBehavior.AllowGet);
        }
    }
 }
