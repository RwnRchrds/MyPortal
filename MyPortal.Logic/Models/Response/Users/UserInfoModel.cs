using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Response.Users
{
    public class UserInfoModel
    {
        public string DisplayName { get; set; }
        public string ProfileImage { get; set; }
        public int[] Permissions { get; set; }
    }
}
