using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class AttendanceCodeMeaningModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }
    }
}
