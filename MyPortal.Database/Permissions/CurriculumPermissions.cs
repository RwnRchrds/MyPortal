using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models;

namespace MyPortal.Database.Permissions
{
    public partial class Permissions
    {
        public static class Curriculum
        {
            public static class AcademicStructure
            {
                public static Guid ViewEditAcademicStructure = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6326");
            }

            public static class AcademicYears
            {
                public static Guid ViewEditAcademicYears = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6327");
            }

            public static class Cover
            {
                public static Guid RunArrangeCover = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6328");
            }

            public static class Homework
            {
                public static Guid ViewHomework = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6329");
                public static Guid EditHomework = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC632A");
            }

            public static class LessonPlans
            {
                public static Guid ViewLessonPlans = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC632B");
                public static Guid EditLessonPlans = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC632C");
            }

            public static class StudyTopics
            {
                public static Guid ViewStudyTopics = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC632D");
                public static Guid EditStudyTopics = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC632E");
            }
        }
    }
}
