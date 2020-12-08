using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Requests.Admin.Users
{
    public class SetUserEnabledModel
    {
        public Guid UserId { get; set; }
        public bool Enabled { get; set; }
    }
}
