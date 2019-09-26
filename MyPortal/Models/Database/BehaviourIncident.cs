using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    /// <summary>
    /// A record of a behaviour incident.
    /// </summary>
    [Table("Behaviour_Incidents")]
    public class BehaviourIncident
    {
        public int Id { get; set; }

        public int AcademicYearId { get; set; }

        public int BehaviourTypeId { get; set; }

        public int StudentId { get; set; }

        public int LocationId { get; set; }

        public int RecordedById { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [StringLength(50)]
        public string Comments { get; set; }

        public int Points { get; set; }

        public bool Resolved { get; set; }

        public bool Deleted { get; set; }

        public virtual BehaviourIncidentType BehaviourIncidentType { get; set; }

        public virtual SchoolLocation Location{ get; set; }

        public virtual CurriculumAcademicYear CurriculumAcademicYear { get; set; }

        public virtual StaffMember RecordedBy { get; set; }

        public virtual Student Student { get; set; }
    }
}