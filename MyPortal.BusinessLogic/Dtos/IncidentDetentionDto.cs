using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class IncidentDetentionDto
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public int DetentionId { get; set; }
        public int AttendanceStatusId { get; set; }

        public virtual IncidentDto Incident { get; set; }
        public virtual DetentionDto Detention { get; set; }
        public virtual DetentionAttendanceStatusDto AttendanceStatus { get; set; }

        public string GetStudentName()
        {
            return Incident.Student.Person.GetDisplayName();
        }

        public string GetReason()
        {
            return Incident.Type.Description;
        }
    }
}
