namespace MyPortal.Dtos
{
    public class CurriculumClassPeriodDto
    {
        public int Id { get; set; }

        public int ClassId { get; set; }

        public int PeriodId { get; set; }

        public AttendancePeriodDto AttendancePeriod { get; set; }

        public CurriculumClassDto CurriculumClass { get; set; }
    }
}