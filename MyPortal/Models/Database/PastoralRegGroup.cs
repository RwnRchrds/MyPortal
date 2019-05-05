namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pastoral_RegGroups")]
    public partial class PastoralRegGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PastoralRegGroup()
        {
            Students = new HashSet<PeopleStudent>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        public int TutorId { get; set; }

        public int YearGroupId { get; set; }

        public virtual PeopleStaffMember Tutor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PeopleStudent> Students { get; set; }

        public virtual PastoralYearGroup PastoralYearGroup { get; set; }
    }
}
