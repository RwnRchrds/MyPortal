using System;

namespace MyPortal.Logic.Models.Admin
{
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
        public Guid? PersonId { get; set; }
        public Guid[] RoleIds { get; set; }
    }
}