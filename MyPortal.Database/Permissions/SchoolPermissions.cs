using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Permissions
{
    public partial class Permissions
    {
        public static class School
        {
            public static class PastoralStructure
            {
                public static Guid ViewEditPastoralStructure = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6356");
            }

            public static class Rooms
            {
                public static Guid ViewRooms = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6357");
                public static Guid EditRooms = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6358");
            }

            public static class Bulletins
            {
                public static Guid ViewBulletins = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC635F");
                public static Guid EditBulletins = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6360");
            }

            public static class SchoolDetails
            {
                public static Guid ViewSchoolDetails = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6359");
                public static Guid EditSchoolDetails = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC635A");
            }

            public static class SchoolDiary
            {
                public static Guid ViewSchoolDiary = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC635B");
                public static Guid EditSchoolDiary = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC635C");
            }

            public static class SchoolDocuments
            {
                public static Guid ViewSchoolDocuments = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC635D");
                public static Guid EditSchoolDocuments = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC635E");
            }
        }
    }
}
