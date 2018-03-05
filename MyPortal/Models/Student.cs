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

        //ID Provided to Student by SIMS .net (MIS ID)
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        //ID of Student in the 4Matrix Database --> Used for links between MyPortal and 4Matrix
        [Display(Name = "4Matrix ID")]        
        public int? FourMId { get; set; }

        [Required]
        [StringLength(3)]
        public string RegGroup { get; set; }

        [Required]
        [StringLength(50)]
        public string YearGroup { get; set; }

        [Display(Name = "Account Balance")]
        public decimal AccountBalance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Logs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }
    }
}
