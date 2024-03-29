﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Tasks")]
    public class Task : BaseTypes.Entity, ISystemEntity, ICreatable
    {
        [Column(Order = 2)] public Guid TypeId { get; set; }

        [Column(Order = 3)] public Guid? AssignedToId { get; set; }

        [Column(Order = 4)] public Guid CreatedById { get; set; }

        [Column(Order = 5)] public DateTime CreatedDate { get; set; }

        [Column(Order = 6)] public DateTime? DueDate { get; set; }

        [Column(Order = 7)] public DateTime? CompletedDate { get; set; }

        [Column(Order = 8)]
        [StringLength(128)]
        public string Title { get; set; }

        [Column(Order = 9)]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(Order = 10)] public bool Completed { get; set; }

        [Column(Order = 11)] public bool AllowEdit { get; set; }

        [Column(Order = 12)] public bool System { get; set; }

        public virtual Person AssignedTo { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual TaskType Type { get; set; }

        public virtual ICollection<HomeworkSubmission> HomeworkSubmissions { get; set; }
        public virtual ICollection<TaskReminder> Reminders { get; set; }
    }
}