﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Helpers;
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

        [HttpGet]
        [Route("api/curriculum/academicYears/get/all")]
        public IEnumerable<CurriculumAcademicYearDto> GetAcademicYears()
        {
            return _context.CurriculumAcademicYears.ToList().OrderByDescending(x => x.FirstDate)
                .Select(Mapper.Map<CurriculumAcademicYear, CurriculumAcademicYearDto>);
        }

        [HttpGet]
        [Route("api/curriculum/academicYears/get/byId")]
        public CurriculumAcademicYearDto GetAcademicYear(int id)
        {
            var academicYear = _context.CurriculumAcademicYears.SingleOrDefault(x => x.Id == id);

            if (academicYear == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<CurriculumAcademicYear, CurriculumAcademicYearDto>(academicYear);
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        [Route("api/curriculum/academicYears/select")]    
        public IHttpActionResult ChangeSelectedAcademicYear(CurriculumAcademicYear year)
        {
            User.ChangeSelectedAcademicYear(year.Id);
            return Ok("Selected academic year changed");
        }
    }
}