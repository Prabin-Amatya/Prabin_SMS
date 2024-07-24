using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Prabin_SMS.Infrastructure.IRepository;
using Prabin_SMS.Infrastructure.Repository;
using Prabin_SMS.Models.Entity;
using Prabin_SMS.Models.ViewModels;

namespace Prabin_SMS.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRawSqlRepository rawSqlRepository;

        public HomeController(IRawSqlRepository rawSqlRepository)
        {
            this.rawSqlRepository = rawSqlRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = rawSqlRepository.FromSql<DashboardCountViewModel>("usp_getCount").ToList();
            return View(result);
        }

        [Route("api/Home/EnrollmentTrend")]
        public async Task<IActionResult> EnrollmentTrend()
        {
            var result = rawSqlRepository.FromSql<DashboardCountTrendModel>("usp_getEnrollmentTrend").ToList();
            var alldate = result.Select(p => p.CreatedDate).Distinct().OrderBy(d=>d).ToList();
            var enrollmentTrend = result.GroupBy(p => p.DegreeName)
                .Select(p => new
                {
                    DegreeName = p.Key,
                    Dates = alldate,
                    Enrollments = alldate.Select(d => p.Where(r => r.CreatedDate == d).Sum(r => r.Enrollments)).ToList()
                });
            return Json(new {alldate, enrollmentTrend});
        }
    }
}
