using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MyPortal.Attributes;
using MyPortal.Models.Misc;
using MyPortal.Services;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/reports")]
    public class ReportsController : MyPortalApiController
    {
        // [Behaviour Reports]

        [HttpGet]
        [RequiresPermission("RunReports")]
        [Route("behaviour/incidents/byType", Name = "ApiReportsBehaviourIncidentsByType")]
        public async Task<IEnumerable<ChartDataCategoric>> BehaviourIncidentsByType()
        {
            var academicYearId = await SystemService.GetCurrentOrSelectedAcademicYearId(_context, User);
            return await BehaviourService.GetChartDataBehaviourIncidentsByType(academicYearId, _context);
        }

        [HttpGet]
        [RequiresPermission("RunReports")]
        [Route("behaviour/achievements/byType", Name = "ApiReportsAchievementsByType")]
        public async Task<IEnumerable<ChartDataCategoric>> AchievementsByType()
        {
            var academicYearId = await SystemService.GetCurrentOrSelectedAcademicYearId(_context, User);
            return await BehaviourService.GetChartDataAchievementsByType(academicYearId, _context);
        }
    }
}
