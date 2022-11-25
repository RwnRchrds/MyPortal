namespace MyPortal.Logic.Models.Requests.Settings.Users;

public class ChangePasswordRequestModel
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}