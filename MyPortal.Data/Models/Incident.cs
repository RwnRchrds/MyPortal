using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// A record of a behaviour incident.
    /// </summary>
    [Table("Incident", Schema = "behaviour")]
    public class Incident
    {
        public int Id { get; set; }

        public int AcademicYearId { get; set; }

        public int BehaviourTypeId { get; set; }

        public int StudentId { get; set; }

        public int LocationId { get; set; }

        public int RecordedById { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public string Comments { get; set; }

        public int Points { get; set; }

        public bool Resolved { get; set; }

        public bool Deleted { get; set; }

        public virtual IncidentType Type { get; set; }

        public virtual Location Location{ get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual StaffMember RecordedBy { get; set; }

        public virtual Student Student { get; set; }
    }
}