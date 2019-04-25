using System;

namespace MyPortal.Dtos
{
    public class PersonnelObservationDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int ObserveeId { get; set; }

        public int ObserverId { get; set; }
        
        public string Outcome { get; set; }

        public CoreStaffMemberDto Observee { get; set; }

        public CoreStaffMemberDto Observer { get; set; }
    }
}