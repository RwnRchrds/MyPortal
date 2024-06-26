﻿using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Settings;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.People
{
    public class TaskModel : BaseModelWithLoad
    {
        public TaskModel(Task model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Task model)
        {
            TypeId = model.TypeId;
            AssignedToId = model.AssignedToId;
            CreatedById = model.CreatedById;
            CreatedDate = model.CreatedDate;
            DueDate = model.DueDate;
            CompletedDate = model.CompletedDate;
            Title = model.Title;
            Description = model.Description;
            Completed = model.Completed;
            AllowEdit = model.AllowEdit;
            System = model.System;

            if (model.AssignedTo != null)
            {
                AssignedTo = new PersonModel(model.AssignedTo);
            }

            if (model.CreatedBy != null)
            {
                CreatedBy = new UserModel(model.CreatedBy);
            }

            if (model.Type != null)
            {
                Type = new TaskTypeModel(model.Type);
            }
        }

        public Guid TypeId { get; set; }

        public Guid? AssignedToId { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        [Required] [StringLength(128)] public string Title { get; set; }

        [StringLength(256)] public string Description { get; set; }

        public bool Completed { get; set; }

        // Allow the assignee to edit the task
        public bool AllowEdit { get; set; }
        public bool System { get; set; }

        public virtual PersonModel AssignedTo { get; set; }
        public virtual UserModel CreatedBy { get; set; }
        public virtual TaskTypeModel Type { get; set; }

        public bool Overdue => !Completed && DueDate <= DateTime.Now;

        protected override async System.Threading.Tasks.Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Tasks.GetById(Id.Value);

                LoadFromModel(model);
            }
        }
    }
}