namespace MyPortal.Dtos.Identity
{
    public class UserDto
    {
        public string Id { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public int AccessFailedCount { get; set; }
    }
}