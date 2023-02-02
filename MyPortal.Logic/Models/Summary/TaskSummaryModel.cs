using System;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Models.Data.People;


namespace MyPortal.Logic.Models.Summary
{
    public class TaskSummaryModel
    {
        public Guid Id { get; set; }
        public DateTime? DueDate { get; set; }
        public string AssignedByName { get; set; }
        public string TaskTypeName { get; set; }
        public string TaskTypeColourCode { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public bool CanEdit { get; set; }

        public TaskSummaryModel(TaskModel model, bool editPersonalOnly)
        {
            if (model.Id.HasValue)
            {
                Id = model.Id.Value;   
            }
            DueDate = model.DueDate;
            AssignedByName = model.AssignedBy.GetDisplayName(NameFormat.FullNameAbbreviated);
            TaskTypeName = model.Type?.Description;
            TaskTypeColourCode = model.Type?.ColourCode;
            Title = model.Title;
            Completed = model.Completed;

            if (model.Type != null)
            {
                CanEdit = !model.Type.System && (!editPersonalOnly || model.Type.Personal);
            }
        }

        public bool Overdue
        {
            get { return !Completed && DueDate <= DateTime.Today; }
        }
    }
}
