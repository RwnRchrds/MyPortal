using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Permissions
{
    public partial class Permissions
    {
        public static class Behaviour
        {
            public static class Achievements
            {
                public static Guid ViewAchievements = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC631B");
                public static Guid EditAchievements = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC631C");
            }

            public static class Incidents
            {
                public static Guid ViewIncidents = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC631D");
                public static Guid EditIncidents = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC631E");
            }

            public static class Detentions
            {
                public static Guid ViewDetentions = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC631F");
                public static Guid EditDetentions = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6320");
            }

            public static class Exclusions
            {
                public static Guid ViewExclusions = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6321");
                public static Guid EditExclusions = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6322");
            }

            public static class ReportCards
            {
                public static Guid ViewReportCards = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6323");
                public static Guid EditReportCards = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6324");
                public static Guid AddRemoveReportCards = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6325");
            }
        }
    }
}
