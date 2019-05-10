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

        public StaffMemberDto Observee { get; set; }

        public StaffMemberDto Observer { get; set; }
    }
}