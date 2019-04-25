namespace MyPortal.Dtos
{
    public class CurriculumLessonPlanDto
    {
        public int Id { get; set; }

        public int StudyTopicId { get; set; }

        public string Title { get; set; }

        public string LearningObjectives { get; set; }

        public string PlanContent { get; set; }

        public string Homework { get; set; }

        public int AuthorId { get; set; }

        public CoreStaffMemberDto Author { get; set; }

        public CurriculumStudyTopicDto StudyTopic { get; set; }
    }
}