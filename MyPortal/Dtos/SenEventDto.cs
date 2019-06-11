using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    public class SenEventDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int EventTypeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }


        public string Note { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual SenEventTypeDto SenEventType { get; set; }
    }
}