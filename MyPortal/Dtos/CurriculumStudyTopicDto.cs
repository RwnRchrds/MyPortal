namespace MyPortal.Dtos
{
    public class CurriculumStudyTopicDto
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public int YearGroupId { get; set; }
        
        public string Name { get; set; }

        public CurriculumSubjectDto CurriculumSubject { get; set; }

        public PastoralYearGroupDto PastoralYearGroup { get; set; }
    }
}