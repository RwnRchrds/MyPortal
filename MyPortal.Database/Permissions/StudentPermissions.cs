using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Permissions
{
    public partial class Permissions
    {
        public static class Student
        {
            public static class MedicalEvents
            {
                public static Guid ViewMedicalEvents = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC636B");
                public static Guid EditMedicalEvents = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC636C");
            }

            public static class Sen
            {
                public static Guid ViewSenDetails = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6361");
                public static Guid EditSenDetails = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6362");
            }

            public static class StudentDetails
            {
                public static Guid ViewStudentDetails = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6363");
                public static Guid EditStudentDetails = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6364");
            }

            public static class StudentDocuments
            {
                public static Guid ViewStudentDocuments = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6369");
                public static Guid EditStudentDocuments = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC636A");
            }

            public static class StudentLogNotes
            {
                public static Guid ViewLogNotes = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6365");
                public static Guid EditLogNotes = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6366");
            }

            public static class StudentTasks
            {
                public static Guid ViewStudentTasks = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6367");
                public static Guid EditStudentTasks = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6368");
            }
        }
    }
}
