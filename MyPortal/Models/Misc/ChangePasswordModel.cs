using System.ComponentModel.DataAnnotations;

namespace MyPortal.Models.Misc
{
    public class ChangePasswordModel
    {
        [Required] public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string Confirm { get; set; }
    }
}