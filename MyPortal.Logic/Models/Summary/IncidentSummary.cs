using System;

namespace MyPortal.Logic.Models.Summary
{
    public class IncidentSummary
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Location { get; set; }
        public string RecordedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comments { get; set; }
        public int Points { get; set; }
        public bool Resolved { get; set; }
    }
}