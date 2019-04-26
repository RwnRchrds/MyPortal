namespace MyPortal.Dtos
{
    public class AssessmentResultDto
    {
        public int ResultSetId { get; set; }

        public int StudentId { get; set; }

        public int SubjectId { get; set; }

        public string Value { get; set; }

        public AssessmentResultSetDto AssessmentResultSet { get; set; }

        public CoreStudentDto CoreStudent { get; set; }

        public CurriculumSubjectDto CurriculumSubject { get; set; }
    }
}