using Library.Core.Models;
using Library.Core.Repositories.Interfaces;
using Library.Core.Services.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.Web.Controllers.Dashboard
{
    public class DashboardController : Controller
    {
        private readonly IReportService _reportService;

        public DashboardController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.Username = Session["Username"];

            var issuedPerDay = await _reportService.GetBooksIssuedLast7DaysAsync();
            ViewBag.Last7DaysLabels = issuedPerDay.Keys.ToArray();
            ViewBag.Last7DaysData = issuedPerDay.Values.ToArray();

            var issuedByCategory = await _reportService.GetBooksIssuedByCategoryAsync();
            ViewBag.Categories = issuedByCategory.Select(c => c.Category).ToArray();
            ViewBag.CategoryData = issuedByCategory.Select(c => c.Percentage).ToArray();


            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetDashboardData(string timeRange)
        {
            var barChartData = await _reportService.GetBooksIssuedLast7DaysAsync();
            var pieChartDataRaw = await _reportService.GetBooksIssuedByCategoryAsync();
            var stats = _reportService.GetDashboardStats();

            // Prepare pie chart data as expected by Chart.js
            var pieChartData = pieChartDataRaw.Select(x => new
            {
                label = x.Category,
                value = x.Percentage
            });

            return Json(new
            {
                charts = new
                {
                    bar = barChartData,
                    pie = pieChartData
                },
                stats = stats,
                categories = pieChartDataRaw
            }, JsonRequestBehavior.AllowGet);
        }

    }
}