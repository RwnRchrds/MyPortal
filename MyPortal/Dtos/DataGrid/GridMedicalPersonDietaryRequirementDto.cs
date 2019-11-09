using MyPortal.Interfaces;

namespace MyPortal.Dtos.DataGrid
{
    public class GridMedicalPersonDietaryRequirementDto : IGridDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}