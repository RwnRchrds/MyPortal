using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("Session")]
    public class Session
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid ClassId { get; set; }

        [DataMember]
        public Guid PeriodId { get; set; }

        [DataMember]
        public Guid TeacherId { get; set; }

        public virtual StaffMember Teacher { get; set; }
        
        public virtual Period Period { get; set; }

        public virtual Class Class { get; set; }
    }
}
