using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Permissions
{
    public partial class Permissions
    {
        public static class Attendance
        {
            public static class Marks
            {
                public static Guid ViewMarks = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6319");
                public static Guid EditMarks = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC631A");
            }
        }
    }
}
