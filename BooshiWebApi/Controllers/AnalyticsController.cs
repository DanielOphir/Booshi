using BooshiDAL.Interfaces;
using BooshiWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooshiWebApi.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AnalyticsController : BaseController
    {
        private readonly IAnalyticsRepository _analyticsRepo;

        public AnalyticsController(IAnalyticsRepository analyticsRepo)
        {
            this._analyticsRepo = analyticsRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnalyticsAsync()
        {
            var newUsersAnalytics = await _analyticsRepo.GetNewUsersByMonth();
            var deliveriesAnalytics = await _analyticsRepo.GetDeliveriesByMonth();
            var analytics = new Dictionary<string, List<int>>() { {"newUsers", newUsersAnalytics }, {"deliveries", deliveriesAnalytics } };
            return Ok(analytics);

        }

        [HttpGet("users")]
        public async Task<IActionResult> GetNewUsersCountByMonthsAsync()
        {
            var rtn = await _analyticsRepo.GetNewUsersByMonth();
            return Ok(rtn);
        }

        [HttpGet("deliveries")]
        public async Task<IActionResult> GetDeliveriesCountByMonthsAsync()
        {
            var rtn = await _analyticsRepo.GetDeliveriesByMonth();
            return Ok(rtn);
        }

        [HttpPost("reports")]
        public IActionResult GetReports(ReportModel reportModel)
        {
            var reports = _analyticsRepo.GetReport(reportModel.FromDate, reportModel.ToDate);
            return Ok(reports);
        }
    }
}