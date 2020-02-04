using System.Collections.Generic;
using MyPortal.Logic.Authorisation;

namespace MyPortal.Logic.Constants
{
    public static class Permission
    {
        public static class Assessment
        {
            public static class Results
            {
                public const int Edit = 0x10;
                public const int View = 0x11;
            }

            public static class ResultSets
            {
                public const int Edit = 0x12;
                public const int View = 0x13;
            }
        }

        public static class Attendance
        {
            public static class Marks
            {
                public const int Edit = 0x20;
                public const int View = 0x21;
            }
        }

        public static class Behaviour
        {
            public static class Data
            {
                public const int Edit = 0x30;
                public const int View = 0x31;
            }
        }

        public static class Curriculum
        {
            public static class AcademicYears
            {
                public const int ChangeSelected = 0x40;
                public const int Edit = 0x41;
            }

            public static class Classes
            {
                public const int Edit = 0x42;
                public const int View = 0x43;
            }

            public static class LessonPlans
            {
                public const int Delete = 0x45;
                public const int Edit = 0x46;
                public const int View = 0x47;
            }

            public static class StudyTopics
            {
                public const int Edit = 0x48;
                public const int View = 0x49;
            }

            public static class Subjects
            {
                public const int Edit = 0x4A;
                public const int View = 0x4B;
            }
        }

        public static class Documents
        {
            public static class General
            {
                public const int Edit = 0x50;
                public const int ViewApproved = 0x51;
            }

            public static class Personal
            {
                public const int Edit = 0x52;
                public const int View = 0x53;
            }
        }

        public static class Finance
        {
            public static class Accounts
            {
                public const int Edit = 0x60;
            }

            public static class Products
            {
                public const int Edit = 0x61;
                public const int View = 0x62;
            }

            public static class Sales
            {
                public const int Edit = 0x63;
                public const int View = 0x64;
            }
        }

        public static class Pastoral
        {
            public static class Structure
            {
                public const int Edit = 0x70;
                public const int View = 0x71;
            }
        }

        public static class Personnel
        {
            public static class TrainingCertificate
            {
                public const int Edit = 0x80;
                public const int View = 0x81;
            }

            public static class TrainingCourse
            {
                public const int Edit = 0x82;
                public const int View = 0x83;
            }

            public static class Observation
            {
                public const int Edit = 0x84;
                public const int View = 0x85;
            }
        }

        public static class Profiles
        {
            public static class Comments
            {
                public const int Edit = 0x90;
                public const int View = 0x91;
            }

            public static class LogNotes
            {
                public const int Edit = 0x92;
                public const int View = 0x93;
            }
        }

        public static class Staff
        {
            public static class Details
            {
                public const int Edit = 0xA0;
                public const int View = 0xA1;
            }
        }

        public static class Student
        {
            public static class Details
            {
                public const int Edit = 0xB0;
                public const int View = 0xB1;
            }
        }

        public static class System
        {
            public static class Bulletins
            {
                public const int Edit = 0xC0;
                public const int ViewApproved = 0xC1;
                public const int ViewStudent = 0xC2;
            }

            public static class Roles
            {
                public const int Edit = 0xC3;
            }

            public static class Users
            {
                public const int Edit = 0xC4;
            }
        }
    }
}