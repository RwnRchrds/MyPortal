using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Dtos.LiteDtos;

namespace MyPortal.Models.Misc
{
    public class StudentAttendanceMarkSingular
    {
        public string StudentName { get; set; }
        public AttendanceMarkLite Mark { get; set; }
    }
}