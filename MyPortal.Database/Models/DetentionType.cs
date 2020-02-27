using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("DetentionType")]
    public class DetentionType
    {
        public DetentionType()
        {
            Detentions = new HashSet<Detention>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(TypeName = "time(2)")]
        public TimeSpan StartTime { get; set; }

        [Column(TypeName = "time(2)")]
        public TimeSpan EndTime { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<Detention> Detentions { get; set; }
    }
}
