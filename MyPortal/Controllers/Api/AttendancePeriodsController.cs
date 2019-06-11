using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models.Database;

namespace MyPortal.Controllers.Api
{
    [Authorize(Roles = ("Staff"))]
    public class AttendancePeriodsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public AttendancePeriodsController()
        {
            _context = new MyPortalDbContext();
        }

        public AttendancePeriodsController(MyPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/attendance/periods/get/all")]
        public IEnumerable<AttendancePeriodDto> GetAllPeriods()
        {
            var dayIndex = new List<string> {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"};
            return _context.AttendancePeriods.ToList().OrderBy(x => dayIndex.IndexOf(x.Weekday))
                .ThenBy(x => x.StartTime).Select(Mapper.Map<AttendancePeriod, AttendancePeriodDto>);
        }

        [HttpGet]
        public AttendancePeriodDto GetPeriod(int periodId)
        {
            var period = _context.AttendancePeriods.SingleOrDefault(x => x.Id == periodId);

            if (period == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<AttendancePeriod, AttendancePeriodDto>(period);
        }
    }
}
