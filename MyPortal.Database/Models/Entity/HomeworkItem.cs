﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("HomeworkItems")]
    public class HomeworkItem : BaseTypes.Entity, IDirectoryEntity
    {
        public HomeworkItem()
        {
            Submissions = new HashSet<HomeworkSubmission>();
        }

        [Column(Order = 2)] public Guid DirectoryId { get; set; }

        [Column(Order = 3)]
        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [Column(Order = 4)]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(Order = 5)] public bool SubmitOnline { get; set; }

        [Column(Order = 6)] public int MaxPoints { get; set; }

        public virtual Directory Directory { get; set; }
        public virtual ICollection<HomeworkSubmission> Submissions { get; set; }
        public virtual ICollection<LessonPlanHomeworkItem> LessonPlanHomeworkItems { get; set; }
    }
}