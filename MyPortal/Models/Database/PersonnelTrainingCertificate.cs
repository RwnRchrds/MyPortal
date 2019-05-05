namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Personnel_TrainingCertificates")]
    public partial class PersonnelTrainingCertificate
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StaffId { get; set; }

        public int StatusId { get; set; }

        public virtual PeopleStaffMember CoreStaffMember { get; set; }

        public virtual PersonnelTrainingCourse PersonnelTrainingCourse { get; set; }

        public virtual PersonnelTrainingStatus PersonnelTrainingStatus { get; set; }
    }
}
