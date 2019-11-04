using MyPortal.Interfaces;

namespace MyPortal.Dtos.GridDtos
{
    public class GridCurriculumSubjectDto : IGridDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LeaderName { get; set; }
        public string Code { get; set; }
    }
}