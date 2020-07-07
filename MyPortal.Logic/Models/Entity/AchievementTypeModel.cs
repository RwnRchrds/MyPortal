using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class AchievementTypeModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public int DefaultPoints { get; set; }

        public bool Active { get; set; }
    }
}
