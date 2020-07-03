using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Scaffolding;
using MyPortal.Logic.Models.Business;

namespace MyPortal.Logic.Models.ListModels
{
    public class TaskListModel
    {
        public Guid Id { get; set; }
        public DateTime? DueDate { get; set; }
        public string AssignedByName { get; set; }
        public string TaskTypeName { get; set; }
        public string TaskTypeColourCode { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public bool CanEdit { get; set; }

        public TaskListModel(TaskModel model)
        {
            Id = model.Id;
            DueDate = model.DueDate;
            AssignedByName = model.AssignedBy.GetDisplayName(true);
            TaskTypeName = model.Type?.Description;
            TaskTypeColourCode = model.Type?.ColourCode;
            Title = model.Title;
            Completed = model.Completed;
            CanEdit = !model.Type?.Reserved ?? false;
        }

        public bool Overdue
        {
            get { return !Completed && DueDate <= DateTime.Today; }
        }
    }
}
