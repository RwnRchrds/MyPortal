using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamResultEmbargoes")]
    public class ExamResultEmbargo : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid ResultSetId { get; set; }

        [Column(Order = 2)]
        public DateTime StartTime { get; set; }

        [Column(Order = 3)]
        public DateTime EndTime { get; set; }

        public virtual ResultSet ResultSet { get; set; }
    }
}
