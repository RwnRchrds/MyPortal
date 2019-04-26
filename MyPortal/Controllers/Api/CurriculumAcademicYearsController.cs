using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyPortal.Models.Database;

namespace MyPortal.Controllers.Api
{
    public class CurriculumAcademicYearsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public CurriculumAcademicYearsController()
        {
            _context = new MyPortalDbContext();
        }

        public CurriculumAcademicYearsController(MyPortalDbContext context)
        {
            _context = context;
        }


    }
}
