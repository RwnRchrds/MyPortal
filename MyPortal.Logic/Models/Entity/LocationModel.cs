using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class LocationModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public bool System { get; set; }
    }
}
