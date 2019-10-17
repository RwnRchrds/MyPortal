using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    [Table("Sen_Events")]
    public class SenEvent
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int EventTypeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        public string Note { get; set; }

        public virtual Student Student { get; set; }

        public virtual SenEventType Type { get; set; }
    }
}