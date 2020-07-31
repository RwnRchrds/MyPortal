using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Constants
{
    public class StudentGroupTypes
    {
        public static Guid CurriculumGroup { get; } = Guid.Parse("");
        public static Guid RegGroup { get; } = Guid.Parse("");
        public static Guid YearGroup { get; } = Guid.Parse("");
        public static Guid CurriculumYearGroup { get; } = Guid.Parse("");
        public static Guid House { get; } = Guid.Parse("");
    }
}
