using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class GradeSetModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public bool Active { get; set; }

        public bool System { get; set; }
    }
}
