using MyPortal.Interfaces;

namespace MyPortal.Dtos.GridDtos
{
    public class GridCurriculumLessonPlanDto : IGridDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StudyTopic { get; set; }
        public string Author { get; set; }
    }
}