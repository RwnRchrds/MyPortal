using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Dtos
{
    public class StaffDto
    {
        public int Id { get; set; }
        
        [Required] 
        public string Code { get; set; }    

        [StringLength(255)] 
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [StringLength(255)]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [StringLength(320)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [StringLength(128)]
        [Display(Name = "User ID")]
        public string UserId { get; set; }
    }
}