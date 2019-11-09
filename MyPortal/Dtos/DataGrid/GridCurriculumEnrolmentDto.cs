using MyPortal.Interfaces;

namespace MyPortal.Dtos.DataGrid
{
    public class GridCurriculumEnrolmentDto : IGridDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }
    }
}