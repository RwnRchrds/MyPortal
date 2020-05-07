namespace MyPortal.Logic.Models.Requests.Admin
{
    public class CreateUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}