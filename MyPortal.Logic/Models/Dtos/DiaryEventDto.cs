using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class DiaryEventDto
    {
        public Guid Id { get; set; }

        public Guid EventTypeId { get; set; }

        [Required]
        [StringLength(256)]
        public string Subject { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [StringLength(256)]
        public string Location { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsAllDay { get; set; }

        public bool IsBlock { get; set; }

        public bool IsPublic { get; set; }

        public bool IsStudentVisible { get; set; }

        public virtual DetentionDto Detention { get; set; }
        public virtual DiaryEventTypeDto EventType { get; set; }
    }
}
