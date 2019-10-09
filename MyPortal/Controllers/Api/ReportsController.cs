using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MyPortal.Models.Attributes;
using MyPortal.Models.Misc;
using MyPortal.Processes;

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
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponseObject(BehaviourProcesses.GetChartData_BehaviourIncidentsByType(academicYearId, _context));
        }

        [HttpGet]
        [RequiresPermission("RunReports")]
        [Route("behaviour/achievements/byType", Name = "ApiReportsAchievementsByType")]
        public async Task<IEnumerable<ChartDataCategoric>> AchievementsByType()
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponseObject(BehaviourProcesses.GetChartData_AchievementsByType(academicYearId, _context));
        }
    }
}
