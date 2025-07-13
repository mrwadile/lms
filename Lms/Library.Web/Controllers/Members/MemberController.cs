using Library.Core.Models;
using Library.Core.Services.Members;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.Web.Controllers.Members
{
    public class MemberController : BaseController
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: Member
        public async Task<ActionResult> Index()
        {
            try
            {
                var members = await _memberService.GetAllMemberAsync();
                return View(members);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"[Index] Failed to fetch book list: {ex}");
                return View("Error");
            }
        }

        // GET: Member/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        // POST: Member/Create
        [HttpPost]
        public async Task<JsonResult> Create(Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _memberService.AddMemberAsync(member);
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Member") });
                }
                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                Trace.TraceError($"[Create] Failed to create member: {ex}");
                return Json(new { success = false, message = "An error occurred while creating the member." });
            }
        }

        //DELETE: Member/Delete
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                if (_memberService.HasIssuedRecords(id))
                {
                    return Json(new { success = false, message = "Cannot delete. Book has issue history." });
                }
                await _memberService.DeleteMemberAsync(id);
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Member") });
            }
            catch (Exception ex)
            {
                Trace.TraceError($"[Delete] Failed to delete member with ID {id}: {ex}");
                return Json(new { success = false, message = "An error occurred while deleting the member." });
            }
        }

        // GET: Member/Edit
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var member = await _memberService.GetMemberByIdAsync(id);
                if (member == null)
                {
                    return HttpNotFound();
                }
                return View(member);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"[Edit] Failed to fetch member with ID {id}: {ex}");
                return View("Error");
            }
        }
        // POST: Member/Edit
        [HttpPost]
        public async Task<JsonResult> Edit(Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _memberService.UpdateMemberAsync(member);
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Member") });
                }
                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                Trace.TraceError($"[Edit] Failed to update member: {ex}");
                return Json(new { success = false, message = "An error occurred while updating the member." });
            }
        }

        // GET: Member/Details
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var member = await _memberService.GetMemberByIdAsync(id);
                if (member == null)
                {
                    return HttpNotFound();
                }
                return View(member);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"[Details] Failed to fetch member with ID {id}: {ex}");
                return View("Error");
            }
        }
        
    }
}