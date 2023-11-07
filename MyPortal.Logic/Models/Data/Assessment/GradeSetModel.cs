using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Assessment
{
    public class GradeSetModel : LookupItemModel
    {
        public GradeSetModel(GradeSet model) : base(model)
        {
            Name = model.Name;
            System = model.System;
        }

        [Required] [StringLength(256)] public string Name { get; set; }

        public bool System { get; set; }
    }
}