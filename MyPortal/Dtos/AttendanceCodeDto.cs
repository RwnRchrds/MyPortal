namespace MyPortal.Dtos
{
    public class AttendanceCodeDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public int MeaningId { get; set; }

        public AttendanceMeaningDto AttendanceMeaning { get; set; }
    }
}