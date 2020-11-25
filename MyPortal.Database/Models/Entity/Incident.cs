using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Incidents")]
    public class Incident : BaseTypes.Entity
    {
        public Incident()
        {
            Detentions = new HashSet<IncidentDetention>();
        }

        [Column(Order = 1)]
        public Guid AcademicYearId { get; set; }

        [Column(Order = 2)]
        public Guid BehaviourTypeId { get; set; }

        [Column(Order = 3)]
        public Guid StudentId { get; set; }

        [Column(Order = 4)]
        public Guid LocationId { get; set; }

        [Column(Order = 5)]
        public Guid RecordedById { get; set; }

        [Column(Order = 6)]
        public Guid? OutcomeId { get; set; }

        [Column(Order = 7)]
        public Guid StatusId { get; set; }

        [Column(Order = 8, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 9)]
        public string Comments { get; set; }

        [Column(Order = 10)]
        public int Points { get; set; }

        [Column(Order = 11)]
        public bool Deleted { get; set; }

        public virtual IncidentType Type { get; set; }

        public virtual Location Location{ get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual User RecordedBy { get; set; } 

        public virtual Student Student { get; set; }

        public virtual BehaviourOutcome Outcome { get; set; }

        public virtual BehaviourStatus Status { get; set; }

        public virtual ICollection<IncidentDetention> Detentions { get; set; }
    }
}