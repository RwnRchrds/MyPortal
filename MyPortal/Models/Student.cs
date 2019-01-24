using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MyPortal.Models
{
    public class Student
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            BasketItems = new HashSet<BasketItem>();
            Logs = new HashSet<Log>();
            Results = new HashSet<Result>();
            Sales = new HashSet<Sale>();
            StudentDocuments = new HashSet<StudentDocument>();
        }

        [Display(Name = "ID")] public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        [StringLength(320)]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required] 
        [Display(Name = "Reg Group")] 
        public int RegGroupId { get; set; }

        [Required] [Display(Name = "Year Group")] public int YearGroupId { get; set; }

        [StringLength(10)]
        [Display(Name = "Candidate Number")]
        public string CandidateNumber { get; set; }

        [Display(Name = "Account Balance")] public decimal AccountBalance { get; set; }

        [StringLength(255)]
        [Display(Name = "MIS ID")]
        public string MisId { get; set; }

        [StringLength(128)]
        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BasketItem> BasketItems { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Logs { get; set; }

        public virtual RegGroup RegGroup { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentDocument> StudentDocuments { get; set; }

        public virtual YearGroup YearGroup { get; set; }
    }
}