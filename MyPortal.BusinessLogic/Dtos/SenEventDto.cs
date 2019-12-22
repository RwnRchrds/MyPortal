using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class SenEventDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int EventTypeId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Note { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual SenEventTypeDto Type { get; set; }
    }
}
