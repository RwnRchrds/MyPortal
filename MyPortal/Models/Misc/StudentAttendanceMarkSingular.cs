using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Dtos.Lite;

namespace MyPortal.Models.Misc
{
    public class StudentAttendanceMarkSingular
    {
        public string StudentName { get; set; }
        public AttendanceMarkLiteDto Mark { get; set; }
    }
}