using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Dtos
{
    public class ObservationDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int ObserveeId { get; set; }

        public int ObserverId { get; set; }

        public int OutcomeId { get; set; }

        public virtual ObservationOutcome Outcome { get; set; }

        public virtual StaffMemberDto Observee { get; set; }

        public virtual StaffMemberDto Observer { get; set; }

        public string GetOutcome()
        {
            return Outcome.Description;
        }
    }
}
