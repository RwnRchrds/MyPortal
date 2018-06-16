using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    public class StaffController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public StaffController()
        {
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public IEnumerable<StaffDto> GetStaff()
        {
            return _context.Staff
                .ToList()
                .Select(Mapper.Map<Staff, StaffDto>);
        }

        public StaffDto GetStaffMember(string id)
        {
            var staff = _context.Staff.SingleOrDefault(s => s.Id == id);

            if (staff == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Staff, StaffDto>(staff);
        }

        [HttpPost]
        public StaffDto CreateStaff(StaffDto staffDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var Staff = Mapper.Map<StaffDto, Staff>(staffDto);
            _context.Staff
                .Add(Staff);

            _context.SaveChanges();

            staffDto.Id = Staff.Id;

            return staffDto;
        }
    }
}