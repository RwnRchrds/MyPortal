using MyPortal.Interfaces;

namespace MyPortal.Dtos.DataGrid
{
    public class GridCurriculumClassDto : IGridDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Teacher { get; set; }
    }
}