using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ReportCards")]
    public class ReportCard : Entity
    {
        [Column(Order = 1)] 
        public Guid StudentId { get; set; }

        [Column(Order = 2)] 
        public Guid BehaviourTypeId { get; set; }

        [Column(Order = 3, TypeName = "date")] 
        public DateTime StartDate { get; set; }

        [Column(Order = 4, TypeName = "date")] 
        public DateTime EndDate { get; set; }

        [Column(Order = 5)]
        [StringLength(256)]
        public string Comments { get; set; }

        [Column(Order = 6)]
        public bool Active { get; set; }

        public virtual Student Student { get; set; }
        public virtual IncidentType BehaviourType { get; set; }
        public virtual ICollection<ReportCardTarget> Targets { get; set; }
        public virtual ICollection<ReportCardSubmission> Submissions { get; set; }
    }
}
