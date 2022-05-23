using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Auth
{
    public class LoginRequestModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
