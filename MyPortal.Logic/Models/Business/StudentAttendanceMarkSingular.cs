using MyPortal.Logic.Models.Lite;

namespace MyPortal.Logic.Models.Business
{
    public class StudentAttendanceMarkSingular
    {
        public string StudentName { get; set; }
        public AttendanceMarkLiteDto Mark { get; set; }
    }
}