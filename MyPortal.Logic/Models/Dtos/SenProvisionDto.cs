using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class SenProvisionDto
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid ProvisionTypeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public string Note { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual SenProvisionTypeDto Type { get; set; }
    }
}
