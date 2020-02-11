using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class SenEventDto
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid EventTypeId { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public string Note { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual SenEventTypeDto Type { get; set; }
    }
}
