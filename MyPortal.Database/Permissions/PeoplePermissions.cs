using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Permissions
{
    public partial class Permissions
    {
        public static class People
        {
            public static class AgentDetails
            {
                public static Guid ViewAgentDetails = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6335");
                public static Guid EditAgentDetails = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6336");
            }

            public static class ContactDetails
            {
                public static Guid ViewContactDetails = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6337");
                public static Guid EditContactDetails = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6338");
            }

            public static class ContactTasks
            {
                public static Guid ViewContactTasks = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6339");
                public static Guid EditContactTasks = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC633A");
            }

            public static class StaffDetails
            {
                public static Guid ViewStaffBasicDetails = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC633B");
                public static Guid ViewStaffEmploymentDetails = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC633C");
                public static Guid EditStaffBasicDetails = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC633D");
                public static Guid EditStaffEmploymentDetails = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC633E");
            }

            public static class StaffDocuments
            {
                public static Guid ViewAllStaffDocuments = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC633F");
                public static Guid ViewManagedStaffDocuments = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6340");
                public static Guid ViewOwnStaffDocuments = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6341");
                public static Guid EditAllStaffDocuments = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6342");
                public static Guid EditManagedStaffDocuments = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6343");
                public static Guid EditOwnStaffDocuments = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6344");
            }

            public static class StaffPerformance
            {
                public static Guid ViewAllStaffPerformance = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6345");
                public static Guid ViewManagedStaffPerformance = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6346");
                public static Guid ViewOwnStaffPerformance = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6347");
                public static Guid EditAllStaffPerformance = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6348");
                public static Guid EditManagedStaffPerformance = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6349");
                public static Guid EditOwnStaffPerformance = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC634A");
            }

            public static class StaffTasks
            {
                public static Guid ViewAllStaffTasks = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC634B");
                public static Guid ViewManagedStaffTasks = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC634C");
                public static Guid EditAllStaffTasks = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC634D");
                public static Guid EditManagedStaffTasks = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC634E");
            }

            public static class TrainingCourses
            {
                public static Guid ViewTrainingCourses = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6378");
                public static Guid EditTrainingCourses = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6379");
            }
        }
    }
}
