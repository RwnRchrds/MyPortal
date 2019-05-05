namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Docs_Documents_Students")]
    public partial class DocsStudentDocument
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int DocumentId { get; set; }

        public virtual DocsDocument CoreDocument { get; set; }

        public virtual PeopleStudent CoreStudent { get; set; }
    }
}
