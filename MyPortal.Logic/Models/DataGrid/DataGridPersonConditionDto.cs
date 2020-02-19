namespace MyPortal.Logic.Models.Dtos.DataGrid
{
    public class DataGridPersonConditionDto
    {
        public int Id { get; set; }
        public string Condition { get; set; }
        public bool MedicationTaken { get; set; }
        public string Medication { get; set; }
    }
}