using MyPortal.Interfaces;

namespace MyPortal.Dtos.DataGrid
{
    public class GridMedicalPersonConditionDto : IGridDto
    {
        public int Id { get; set; }
        public string Condition { get; set; }
        public bool MedicationTaken { get; set; }
        public string Medication { get; set; }
    }
}