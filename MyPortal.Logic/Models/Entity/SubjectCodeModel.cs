using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class SubjectCodeModel : LookupItemModel
    {
        public string Code { get; set; }

        public Guid SubjectCodeSetId { get; set; }

        public virtual SubjectCodeSetModel SubjectCodeSet { get; set; }
    }
}