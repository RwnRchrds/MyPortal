using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Incidents")]
    public class Incident : BaseTypes.Entity, ICreatable, ISoftDeleteEntity
    {
        [Column(Order = 1)]
        public Guid AcademicYearId { get; set; }

        [Column(Order = 2)]
        public Guid BehaviourTypeId { get; set; }

        [Column(Order = 4)]
        public Guid? LocationId { get; set; }

        [Column(Order = 5)]
        public Guid CreatedById { get; set; }

        [Column(Order = 8, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 9)]
        public string Comments { get; set; }

        [Column(Order = 11)]
        public bool Deleted { get; set; }

        public virtual IncidentType Type { get; set; }

        public virtual Location Location{ get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual User CreatedBy { get; set; }

        public virtual ICollection<StudentIncident> InvolvedStudents { get; set; }
    }
}