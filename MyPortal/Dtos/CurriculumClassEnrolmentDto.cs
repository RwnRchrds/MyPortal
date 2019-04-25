namespace MyPortal.Dtos
{
    public class CurriculumClassEnrolmentDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ClassId { get; set; }

        public CurriculumClassDto CurriculumClass { get; set; }
    }
}