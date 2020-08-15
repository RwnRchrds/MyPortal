using System;

namespace MyPortal.Logic.Models.Entity
{
    public abstract class ResultModel
    {
        public Guid Id { get; set; }

        public Guid ResultSetId { get; set; }

        public Guid StudentId { get; set; }

        public Guid AspectId { get; set; }

        public DateTime Date { get; set; }

        public string Comments { get; set; }

        public virtual ResultSetModel ResultSet { get; set; }

        public virtual AspectModel Aspect { get; set; }

        public virtual StudentModel Student { get; set; }
    }
}
