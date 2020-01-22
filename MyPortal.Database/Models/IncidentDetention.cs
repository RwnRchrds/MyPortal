using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("IncidentDetention")]
    public class IncidentDetention
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public int DetentionId { get; set; }
        public int AttendanceStatusId { get; set; }

        public virtual DetentionAttendanceStatus AttendanceStatus { get; set; }
        public virtual Incident Incident { get; set; }
        public virtual Detention Detention { get; set; }
    }
}
