using MyPortal.Interfaces;

namespace MyPortal.Dtos.GridDtos
{
    public class GridCurriculumStudyTopicDto : IGridDto
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string YearGroup { get; set; }
        public string Name { get; set; }
    }
}