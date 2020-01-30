using System.Collections.Generic;
using MyPortal.Logic.Authorisation;

namespace MyPortal.Logic.Constants
{
    public static class Permission
    {
        public static class Admin
        {
            public static class Users
            {
                public const int Edit = 0x10;
            }

            public static class Roles
            {
                public const int Edit = 0x11;
            }
        }

        public static class Assessment
        {
            public static class ResultSets
            {
                public const int Edit = 0x20;
                public const int View = 0x21;
            }

            public static class Results
            {
                public const int Edit = 0x22;
                public const int View = 0x23;
            }
        }

        public static class Attendance
        {
            public static class Data
            {
                public const int Edit = 0x30;
                public const int View = 0x31;
            }
        }

        public static class Behaviour
        {
            public static class Data
            {
                public const int Edit = 0x40;
                public const int View = 0x41;
            }
        }

        public static class Curriculum
        {
            public static class AcademicYears
            {
                public const int Edit = 0x50;
            }

            public static class LessonPlans
            {
                public const int Delete = 0x51;
                public const int View = 0x52;
            }

            public static class Classes
            {
                public const int Edit = 0x53;
            }
        }
    }
}