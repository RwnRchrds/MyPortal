using MyPortal.Models.Validation;

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

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name="ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "4Matrix ID")]
        public int? FourMId { get; set; }

        [Display(Name = "Reg Group")]
        //[IsInYearGroup]
        public int RegGroup { get; set; }

        [Display(Name = "Year Group")]
        public int YearGroup { get; set; }

        [Display(Name = "Account Balance")]
        public decimal AccountBalance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Logs { get; set; }

        public virtual RegGroup RegGroup1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }

        public virtual YearGroup YearGroup1 { get; set; }
    }
}
