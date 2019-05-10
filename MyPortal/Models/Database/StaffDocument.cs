namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Docs_Documents_Staff")]
    public partial class StaffDocument
    {
        public int Id { get; set; }

        public int StaffId { get; set; }

        public int DocumentId { get; set; }

        public virtual Document Document { get; set; }

        public virtual StaffMember Owner { get; set; }
    }
}
