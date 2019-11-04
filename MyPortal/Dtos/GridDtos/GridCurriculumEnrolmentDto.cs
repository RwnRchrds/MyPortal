using MyPortal.Interfaces;

namespace MyPortal.Dtos.GridDtos
{
    public class GridCurriculumEnrolmentDto : IGridDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }
    }
}