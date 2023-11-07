using System;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Curriculum
{
    public class SubjectCodeModel : LookupItemModel
    {
        public SubjectCodeModel(SubjectCode model) : base(model)
        {
        }

        public string Code { get; set; }

        public Guid SubjectCodeSetId { get; set; }

        public virtual SubjectCodeSetModel SubjectCodeSet { get; set; }
    }
}