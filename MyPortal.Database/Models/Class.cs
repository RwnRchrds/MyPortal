using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("Class")]
    public partial class Class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {
            Sessions = new HashSet<Session>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid? SubjectId { get; set; }

        public Guid BandId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public Guid? YearGroupId { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual YearGroup YearGroup { get; set; }

        public virtual CurriculumBand Band { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
