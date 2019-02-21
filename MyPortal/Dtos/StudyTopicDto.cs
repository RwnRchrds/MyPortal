namespace MyPortal.Dtos
{
    public class StudyTopicDto
    {
        public int Id { get; set; }
        
        public int Type { get; set; }

        public int SubjectId { get; set; }

        public int YearGroupId { get; set; }

        public string Name { get; set; }

        public SubjectDto Subject { get; set; }

        public YearGroupDto YearGroup { get; set; }
    }
}