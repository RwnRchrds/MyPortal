using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Permissions
{
    public partial class Permissions
    {
        public static class System
        {
            public static class Groups
            {
                public static Guid ViewGroups = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC636F");
                public static Guid EditGroups = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6370");
            }

            public static class Settings
            {
                public static Guid SystemSettings = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6371");
                public static Guid AttendanceSettings = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6372");
                public static Guid BehaviourSettings = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6373");
                public static Guid FinanceSettings = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6374");
                public static Guid PersonSettings = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6375");
                public static Guid StaffSettings = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6376");
                public static Guid SenSettings = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6377");
            }

            public static class Users
            {
                public static Guid ViewUsers = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC636D");
                public static Guid EditUsers = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC636E");
            }
        }
    }
}
