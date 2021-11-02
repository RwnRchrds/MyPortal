﻿using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class StudyTopicModel : LookupItemModel, ILoadable
    {
        public StudyTopicModel(StudyTopic model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(StudyTopic model)
        {
            CourseId = model.CourseId;
            Name = model.Name;

            if (model.Course != null)
            {
                Course = new CourseModel(model.Course);
            }
        }
        
        public Guid CourseId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public virtual CourseModel Course { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.StudyTopics.GetById(Id.Value);
                
                LoadFromModel(model);
            }
        }
    }
}
