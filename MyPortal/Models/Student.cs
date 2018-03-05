using System.ComponentModel;

namespace MyPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            Logs = new HashSet<Log>();
            Results = new HashSet<Result>();
        }

        //ID Provided to Student by SIMS .net (MIS ID) --> Student *MUST* Exist in MIS before adding to MyPortal
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("SIMS ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        //ID of Student in the 4Matrix Database --> Used for links between MyPortal and 4Matrix
        [Display(Name = "4Matrix ID")]
        public int? FourMId { get; set; }

        [DisplayName("Registration Group")]
        public int RegGroup { get; set; }

        [DisplayName("Year Group")]
        public int YearGroup { get; set; }

        [DisplayName("Account Balance")]
        public decimal AccountBalance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Logs { get; set; }

        public virtual RegGroup RegGroup1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }

        public virtual YearGroup YearGroup1 { get; set; }
    }
}
