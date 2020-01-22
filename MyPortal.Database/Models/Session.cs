using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("Session")]
    public partial class Session
    {
        public int Id { get; set; }

        public int ClassId { get; set; }

        public int PeriodId { get; set; }

        public virtual Period Period { get; set; }

        public virtual Class Class { get; set; }
    }
}
