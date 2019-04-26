namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Core_Documents_Students")]
    public partial class CoreStudentDocument
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int DocumentId { get; set; }

        public virtual CoreDocument CoreDocument { get; set; }

        public virtual CoreStudent CoreStudent { get; set; }
    }
}
