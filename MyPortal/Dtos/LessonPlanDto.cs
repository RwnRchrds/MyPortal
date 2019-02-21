namespace MyPortal.Dtos
{
    public class LessonPlanDto
    {
        public int Id { get; set; }
        public int StudyTopicId { get; set; }
        public string Title { get; set; }
        public string PlanContent { get; set; }

        public StudyTopicDto StudyTopic { get; set; }
    }
}