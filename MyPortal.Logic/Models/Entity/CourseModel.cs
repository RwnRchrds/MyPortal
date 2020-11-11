using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class CourseModel : LookupItemModel
    {
        public Guid SubjectId { get; set; }
        
        public string Name { get; set; }

        public virtual SubjectModel Subject { get; set; }
    }
}