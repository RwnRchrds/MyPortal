using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class IncidentDto
    {
        public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid BehaviourTypeId { get; set; }

        public Guid StudentId { get; set; }

        public Guid LocationId { get; set; }

        public Guid RecordedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Comments { get; set; }

        public int Points { get; set; }

        public bool Resolved { get; set; }

        public bool Deleted { get; set; }

        public virtual IncidentTypeDto Type { get; set; }

        public virtual LocationDto Location { get; set; }

        public virtual AcademicYearDto AcademicYear { get; set; }

        public virtual StaffMemberDto RecordedBy { get; set; }

        public virtual StudentDto Student { get; set; }
    }
}
