using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Details
{
    public class SenStatusDetails
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(1)]
        public string Code { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }
    }
}