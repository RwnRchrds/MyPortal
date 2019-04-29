namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Core_Documents_Staff")]
    public partial class CoreStaffDocument
    {
        public int Id { get; set; }

        public int StaffId { get; set; }

        public int DocumentId { get; set; }

        public virtual CoreDocument CoreDocument { get; set; }

        public virtual CoreStaffMember Owner { get; set; }
    }
}