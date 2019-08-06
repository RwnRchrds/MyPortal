using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    /// <summary>
    /// Represents an individual person in the system.
    /// </summary>
    [Table("People_Persons")]
    public class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            Staff = new HashSet<StaffMember>();
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }

        public int? PersonTypeId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(300)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(300)]
        public string LastName { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Dob { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        public bool Deleted { get; set; }

        public virtual PersonType PersonType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffMember> Staff { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
    }
}