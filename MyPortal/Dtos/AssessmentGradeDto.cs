namespace MyPortal.Dtos
{
    public class AssessmentGradeDto
    {
        public int Id { get; set; }

        public int GradeSetId { get; set; }
        
        public string Grade { get; set; }

        public AssessmentGradeSetDto AssessmentGradeSet { get; set; }
    }
}