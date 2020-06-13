using System;
using System.Collections.Generic;
using System.Linq;
using MyPortal.Database.Constants;

namespace MyPortal.Logic.Constants
{
    public static class Policies
    {
        public static class UserType
        {
            public const string Student = "policy.user.student";
            public const string Staff = "policy.user.staff";
            public const string Parent = "policy.user.parent";
        }
    }
}
