using MyPortal.BusinessLogic.Dtos.Lite;

namespace MyPortal.BusinessLogic.Models.Data
{
    public class StudentAttendanceMarkSingular
    {
        public string StudentName { get; set; }
        public AttendanceMarkLiteDto Mark { get; set; }
    }
}