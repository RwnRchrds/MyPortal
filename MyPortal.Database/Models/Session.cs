using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("Session")]
    public partial class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ClassId { get; set; }

        public Guid PeriodId { get; set; }

        public virtual Period Period { get; set; }

        public virtual Class Class { get; set; }
    }
}
