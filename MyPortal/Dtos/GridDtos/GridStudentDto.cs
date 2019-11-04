using MyPortal.Interfaces;

namespace MyPortal.Dtos.GridDtos
{
    public class GridStudentDto : IGridDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string RegGroupName { get; set; }
        public string YearGroupName { get; set; }
        public string HouseName { get; set; }
        public decimal AccountBalance { get; set; }
    }
}