namespace MyPortal.Dtos
{
    public class AttendanceRegisterMarkDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int WeekId { get; set; }

        public int PeriodId { get; set; }

        public string Mark { get; set; }

        public AttendancePeriodDto AttendancePeriod { get; set; }

        public StudentDto Student { get; set; }
    }
}