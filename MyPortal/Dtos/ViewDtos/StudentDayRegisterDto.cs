using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos.ViewDtos
{
    public class StudentDayRegisterDto
    {
        public CoreStudentDto StudentDto { get; set; }
        public IEnumerable<AttendanceMarkDto> AttendanceMarks { get; set; }
    }
}