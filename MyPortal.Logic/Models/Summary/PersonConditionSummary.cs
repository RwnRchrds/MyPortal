namespace MyPortal.Logic.Models.Summary
{
    public class PersonConditionSummary
    {
        public int Id { get; set; }
        public string Condition { get; set; }
        public bool MedicationTaken { get; set; }
        public string Medication { get; set; }
    }
}