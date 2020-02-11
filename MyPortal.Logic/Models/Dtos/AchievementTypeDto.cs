using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Dtos
{
    public class AchievementTypeDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [NotNegative]
        public int DefaultPoints { get; set; }

        public bool System { get; set; }
    }
}
