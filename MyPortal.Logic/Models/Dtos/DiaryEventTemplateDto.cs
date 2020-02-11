using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class DiaryEventTemplateDto
    {
        public Guid Id { get; set; }
        public Guid EventTypeId { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }
        public int Minutes { get; set; }
        public int Hours { get; set; }
        public int Days { get; set; }

        public virtual DiaryEventTypeDto DiaryEventType { get; set; }
    }
}
