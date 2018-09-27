using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Dtos
{
    public class StaffDto
    {
        [StringLength(3)] 
        public string Id { get; set; }
        
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
    }
}