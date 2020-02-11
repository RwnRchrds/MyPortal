using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class SenReviewTypeDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }
    }
}
