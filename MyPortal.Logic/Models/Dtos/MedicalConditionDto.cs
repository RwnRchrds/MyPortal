using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class MedicalConditionDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public bool System { get; set; }
    }
}
