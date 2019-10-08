using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    /// <summary>
    /// A record of a behaviour incident.
    /// </summary>
    
    public class BehaviourIncidentDto
    {
        public int Id { get; set; }

        public int AcademicYearId { get; set; }

        public int BehaviourTypeId { get; set; }

        public int StudentId { get; set; }

        public int LocationId { get; set; }

        public int RecordedById { get; set; }

        
        public DateTime Date { get; set; }

        public string Comments { get; set; }

        public int Points { get; set; }

        public bool Resolved { get; set; }

        public bool Deleted { get; set; }

        public virtual BehaviourIncidentTypeDto IncidentType { get; set; }

        public virtual SchoolLocationDto Location { get; set; }

        public virtual CurriculumAcademicYearDto AcademicYear { get; set; }

        public virtual StaffMemberDto RecordedBy { get; set; }

        public virtual StudentDto Student { get; set; }
    }
}