using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Auth
{
    public class ChangePasswordModel
    {
        [Required] public string UserId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage =
            "The password and confirmation password do not match.")]
        public string Confirm { get; set; }
    }
}