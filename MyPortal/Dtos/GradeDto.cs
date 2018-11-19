namespace MyPortal.Dtos
{
    public class GradeDto
    {
        public int Id { get; set; }
        public int GradeSetId { get; set; }
        public string GradeValue { get; set; }
        public GradeSetDto GradeSet { get; set; }
    }
}