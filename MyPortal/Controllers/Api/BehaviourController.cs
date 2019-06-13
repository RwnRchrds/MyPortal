using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Processes;

namespace MyPortal.Controllers.Api
{
    public class BehaviourController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public BehaviourController()
        {
            _context = new MyPortalDbContext();
        }

        public BehaviourController(MyPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/behaviour/points/get/{studentId}")]
        public int GetBehaviourPoints(int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return BehaviourProcesses.GetBehaviourPoints(studentId, academicYearId, _context);
        }
    }
}
