using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models.QueryResults.Attendance;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Response.Attendance
{
    public class AttendanceRegisterResponseModel
    {
        public AttendanceRegisterResponseModel(SessionMetadata metadata)
        {
            Metadata = metadata;
            Students = new List<AttendanceRegisterStudentResponseModel>();
            Codes = new List<AttendanceCodeModel>();
        }

        public SessionMetadata Metadata { get; set; }
        public ICollection<AttendanceRegisterColumnModel> Columns { get; set; }
        public ICollection<AttendanceCodeModel> Codes { get; set; }
        public ICollection<AttendanceRegisterStudentResponseModel> Students { get; set; }
    }
}
