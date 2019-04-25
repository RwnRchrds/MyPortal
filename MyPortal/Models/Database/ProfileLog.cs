namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Profile_Logs")]
    public partial class ProfileLog
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public int AuthorId { get; set; }

        public int StudentId { get; set; }

        [Required]
        public string Message { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public virtual CoreStaffMember CoreStaffMember { get; set; }

        public virtual CoreStudent CoreStudent { get; set; }

        public virtual ProfileLogType ProfileLogType { get; set; }
    }
}
