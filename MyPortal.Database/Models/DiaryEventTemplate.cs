using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("DiaryEventTemplate")]
    public class DiaryEventTemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid EventTypeId { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }
        public int Minutes { get; set; }
        public int Hours { get; set; }
        public int Days { get; set; }

        public virtual DiaryEventType DiaryEventType { get; set; }
    }
}
