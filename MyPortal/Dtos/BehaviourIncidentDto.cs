using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
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

        public virtual BehaviourTypeDto BehaviourType { get; set; }

        public virtual BehaviourLocationDto BehaviourLocation{ get; set; }

        public virtual CurriculumAcademicYearDto CurriculumAcademicYear { get; set; }

        public virtual StaffMemberDto RecordedByStaff { get; set; }

        public virtual StudentDto Student { get; set; }
    }
}