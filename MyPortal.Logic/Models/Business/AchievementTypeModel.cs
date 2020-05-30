using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Business
{
    public class AchievementTypeModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public int DefaultPoints { get; set; }

        public bool Active { get; set; }

        public bool System { get; set; }
    }
}
