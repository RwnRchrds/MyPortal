using MyPortal.Interfaces;

namespace MyPortal.Dtos.DataGrid
{
    public class GridPersonnelTrainingCourseDto : IGridDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}