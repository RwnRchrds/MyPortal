using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Dtos
{
    public class StudentDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required] [StringLength(255)] public string FirstName { get; set; }

        [Required] [StringLength(255)] public string LastName { get; set; }
        
        [Required] [StringLength(1)] public string Gender { get; set; }

        [StringLength(320)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required] public int RegGroupId { get; set; }

        [Required] public int YearGroupId { get; set; }

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

        public YearGroupDto YearGroup { get; set; }

        public RegGroupDto RegGroup { get; set; }
    }
}