using System;

namespace MyPortal.Database.Permissions
{
    public partial class Permissions
    {
        public static class Admissions
        {
            public static class Applications
            {
                public static Guid ViewApplications = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6300");
                public static Guid EditApplications = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6301");
            }

            public static class Enquiries
            {
                public static Guid ViewEnquiries = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6302");
                public static Guid EditEnquiries = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6303");
            }

            public static class InterviewsAndVisits
            {
                public static Guid ViewInterviewsAndVisits = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6304");
                public static Guid EditInterviewsAndVisits = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6305");
            }
        }
    }
}
