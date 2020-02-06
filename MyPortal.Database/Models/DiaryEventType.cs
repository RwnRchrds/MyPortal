using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("DiaryEventType")]
    public class DiaryEventType
    {
        public DiaryEventType()
        {
            DiaryEventTemplates = new HashSet<DiaryEventTemplate>();
            DiaryEvents = new HashSet<DiaryEvent>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [StringLength(128)]
        public string ColourCode { get; set; }

        public virtual  ICollection<DiaryEventTemplate> DiaryEventTemplates { get; set; }
        public virtual  ICollection<DiaryEvent> DiaryEvents { get; set; }
    }
}
