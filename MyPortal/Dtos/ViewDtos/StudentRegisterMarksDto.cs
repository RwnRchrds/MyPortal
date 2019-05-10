using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Dtos.LiteDtos;

namespace MyPortal.Dtos.ViewDtos
{
    public class StudentRegisterMarksDto
    {
        public StudentDto Student { get; set; }
        public IEnumerable<AttendanceRegisterMarkLite> Marks { get; set; }
    }
}