using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Dtos.LiteDtos;

namespace MyPortal.Dtos.ViewDtos
{
    public class StudentLiteMarksCollection
    {
        //public StudentDto Student { get; set; }
        public string StudentName { get; set; }
        public IEnumerable<AttendanceMarkLite> Marks { get; set; }
    }
}