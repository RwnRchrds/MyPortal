using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Constants
{
    public class Permissions
    {
        public static Dictionary<Guid, string> ClaimValues;

        public static Guid EditAcademicYears { get; } = Guid.Parse("F9F0F505-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditAccounts { get; } = Guid.Parse("F9F0F645-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditAllLessonPlans { get; } = Guid.Parse("F9F0F535-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditAttendanceData { get; } = Guid.Parse("F9F0F455-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditBulletins { get; } = Guid.Parse("F9F0F855-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditClasses { get; } = Guid.Parse("F9F0F515-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditCommentBanks { get; } = Guid.Parse("5190AC88-5C14-4A6B-AEF2-C0E796A0B2B7");
        public static Guid EditObservations { get; } = Guid.Parse("F9F0F755-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditOwnLessonPlans { get; } = Guid.Parse("F9F0F545-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditPastoralStructure { get; } = Guid.Parse("F9F0F695-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditProducts { get; } = Guid.Parse("F9F0F655-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditResults { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditResultSets { get; } = Guid.Parse("F9F0F435-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditRoles { get; } = Guid.Parse("F9F0F885-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditSales { get; } = Guid.Parse("F9F0F675-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditSchoolDocuments { get; } = Guid.Parse("F9F0F605-B47C-4C53-B897-D5964FBC6356");
        public static Guid ManageStaff { get; } = Guid.Parse("F9F0F815-B47C-4C53-B897-D5964FBC6356");
        public static Guid ManageStudents { get; } = Guid.Parse("F9F0F835-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditStudyTopics { get; } = Guid.Parse("F9F0F565-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditSubjects { get; } = Guid.Parse("F9F0F585-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditTrainingCertificates { get; } = Guid.Parse("F9F0F715-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditTrainingCourses { get; } = Guid.Parse("F9F0F735-B47C-4C53-B897-D5964FBC6356");
        public static Guid EditUsers { get; } = Guid.Parse("F9F0F895-B47C-4C53-B897-D5964FBC6356");
        public static Guid ViewAttendanceData { get; } = Guid.Parse("F9F0F465-B47C-4C53-B897-D5964FBC6356");
        public static Guid ViewClasses { get; } = Guid.Parse("F9F0F525-B47C-4C53-B897-D5964FBC6356");
        public static Guid ViewLessonPlans { get; } = Guid.Parse("F9F0F555-B47C-4C53-B897-D5964FBC6356");
        public static Guid ViewResults { get; } = Guid.Parse("F9F0F425-B47C-4C53-B897-D5964FBC6356");
        public static Guid ViewResultSets { get; } = Guid.Parse("F9F0F445-B47C-4C53-B897-D5964FBC6356");
    }
}
