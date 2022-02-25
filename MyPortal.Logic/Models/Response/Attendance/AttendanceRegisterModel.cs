using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models.QueryResults.Attendance;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Response.Attendance
{
    public class AttendanceRegisterModel
    {
        public AttendanceRegisterModel(SessionMetadata metadata)
        {
            Metadata = metadata;
            Students = new List<AttendanceRegisterStudentModel>();
            Codes = new List<AttendanceCodeModel>();
        }

        public SessionMetadata Metadata { get; set; }
        public ICollection<AttendanceCodeModel> Codes { get; set; }
        public ICollection<AttendanceRegisterStudentModel> Students { get; set; }
    }
}
