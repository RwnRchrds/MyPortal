using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Permissions
{
    public partial class Permissions
    {
        public static class Profiles
        {
            public static class CommentBanks
            {
                public static Guid ViewCommentBanks = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC634F");
                public static Guid EditCommentBanks = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6350");
            }

            public static class ReportWriting
            {
                public static Guid ViewReports = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6351");
                public static Guid EditOwnReports = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6352");
                public static Guid EditAllReports = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6353");
            }

            public static class ReportingSessions
            {
                public static Guid ViewReportingSessions = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6354");
                public static Guid EditReportingSessions = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6355");
            }
        }
    }
}
