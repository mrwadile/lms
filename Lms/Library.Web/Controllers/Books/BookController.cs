using Library.Core.Models;
using Library.Core.Repositories.Interfaces;
using Library.Core.Services.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

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
            try 
            {
                var books = await _bookService.GetAllBookAsync();
                return View(books);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"[Index] Failed to fetch book list: {ex}");
                return View("Error");
            }
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
            try 
            {
                if (ModelState.IsValid)
                {
                    await _bookService.AddBookAsync(book);
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Book") });
                }
                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                Trace.TraceError($"[Create POST] Error creating book: {ex}");
                return Json(new { success = false, message = "Error creating book." });
            }
        }

        // GET: Book/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            try 
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                {
                    return HttpNotFound();
                }
                return View(book);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"[Edit GET] Error fetching book with ID {id}: {ex}");
                return View("Error");
            }
        }

        // POST: Book/Edit
        [HttpPost]
        public async Task<JsonResult> Edit(Book book)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    await _bookService.UpdateBookAsync(book);
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Book") });
                }
                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                Trace.TraceError($"[Edit POST] Error updating book with ID {book.BookId}: {ex}");
                return Json(new { success = false, message = "Error updating book." });
            }
        }

        // GET: Book/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            try 
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                {
                    return HttpNotFound();
                }
                return View(book);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"[Delete GET] Error loading delete view for book ID {id}: {ex}");
                return View("Error");
            }
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
                Trace.TraceError($"[Delete POST] Error deleting book with ID {id}: {ex}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: Book/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            try 
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                {
                    return HttpNotFound();
                }
                return View(book);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"[Details GET] Error loading book details for ID {id}: {ex}");
                return View("Error");
            }
        }
    
    
    }
}
