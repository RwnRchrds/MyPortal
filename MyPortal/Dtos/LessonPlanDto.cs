namespace MyPortal.Dtos
{
    public class LessonPlanDto
    {
        public int Id { get; set; }
        public int StudyTopicId { get; set; }
        public string Content { get; set; }

        public StudyTopicDto StudyTopic { get; set; }
    }
}