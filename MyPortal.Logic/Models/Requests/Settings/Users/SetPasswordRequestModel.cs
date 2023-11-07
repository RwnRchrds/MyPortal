using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Settings.Users
{
    public class SetPasswordRequestModel
    {
        [Required] public string NewPassword { get; set; }
    }
}