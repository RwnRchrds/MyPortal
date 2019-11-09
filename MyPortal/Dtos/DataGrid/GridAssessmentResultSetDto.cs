using MyPortal.Interfaces;

namespace MyPortal.Dtos.DataGrid
{
    public class GridAssessmentResultSetDto : IGridDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
    }
}