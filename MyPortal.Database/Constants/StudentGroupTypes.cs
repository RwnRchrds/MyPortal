using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Constants
{
    public class StudentGroupTypes
    {
        public static Guid Activity { get; } = Guid.Parse("5E37BCFF-6C32-4FC9-B2CB-D45E44C7A3D6");
        public static Guid RegGroup { get; } = Guid.Parse("5E37BCFF-6C32-4FC9-B2CB-D45E44C7A3D7");
        public static Guid YearGroup { get; } = Guid.Parse("5E37BCFF-6C32-4FC9-B2CB-D45E44C7A3D8");
        public static Guid House { get; } = Guid.Parse("5E37BCFF-6C32-4FC9-B2CB-D45E44C7A3D9");
        public static Guid CurriculumBand { get; } = Guid.Parse("5E37BCFF-6C32-4FC9-B2CB-D45E44C7A3DA");
        public static Guid CurriculumGroup { get; } = Guid.Parse("5E37BCFF-6C32-4FC9-B2CB-D45E44C7A3DB");
        public static Guid UserDefined { get; } = Guid.Parse("5E37BCFF-6C32-4FC9-B2CB-D45E44C7A3DC");
    }
}
