using System;

namespace MyPortal.Logic.Models.Dtos
{
    public class IncidentDto
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

        public virtual IncidentTypeDto Type { get; set; }

        public virtual LocationDto Location { get; set; }

        public virtual AcademicYearDto AcademicYear { get; set; }

        public virtual StaffMemberDto RecordedBy { get; set; }

        public virtual StudentDto Student { get; set; }
    }
}
