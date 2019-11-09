using MyPortal.Interfaces;

namespace MyPortal.Dtos.DataGrid
{
    public class GridAssessmentResultDto : IGridDto
    {
        public string StudentName { get; set; }
        public string ResultSet { get; set; }
        public string Aspect { get; set; }
        public string Value { get; set; }
    }
}