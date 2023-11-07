using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.People
{
    public class TaskTypeModel : LookupItemModel
    {
        public TaskTypeModel(TaskType model) : base(model)
        {
            Personal = model.Personal;
            ColourCode = model.ColourCode;
            System = model.System;
        }

        public bool Personal { get; set; }

        [Required] public string ColourCode { get; set; }

        public bool System { get; set; }
    }
}