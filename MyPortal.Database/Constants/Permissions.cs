using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPortal.Database.Constants
{
    public class Permissions
    {
        public static Guid ViewEditPastoralStructure { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6356");
        public static Guid ViewEditAcademicStructure { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6357");
        public static Guid ViewStudyTopics { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6358");
        public static Guid EditStudyTopics { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6359");
        public static Guid DeleteStudyTopics { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC635A");
        public static Guid ViewLessonPlans { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC635B");
        public static Guid EditLessonPlans { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC635C");
        public static Guid DeleteLessonPlans { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC635D");
        public static Guid ViewSchoolDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC635E");
        public static Guid EditSchoolDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC635F");
        public static Guid ViewSchoolDocuments { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6360");
        public static Guid EditSchoolDocuments { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6361");
        public static Guid ViewSchoolDiary { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6362");
        public static Guid EditSchoolDiary { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6363");
        public static Guid RunArrangeCover { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6364");
        public static Guid ViewStudentDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6365");
        public static Guid EditStudentDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6366");
        public static Guid ViewStudentOverview { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6367");
        public static Guid EditStudentOverview { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6368");
        public static Guid ViewSenDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6369");
        public static Guid EditSenDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC636A");
        public static Guid ViewExclusions { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC636B");
        public static Guid EditExclusions { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC636C");
        public static Guid ViewIncidents { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC636D");
        public static Guid EditIncidents { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC636E");
        public static Guid ViewAchievements { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC636F");
        public static Guid EditAchievements { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6370");
        public static Guid ViewDetentions { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6371");
        public static Guid EditDetentions { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6372");
        public static Guid ViewReportCard { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6373");
        public static Guid EditReportCard { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6374");
        public static Guid AddReportCard { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6375");
        public static Guid DeleteReportCard { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6376");
        public static Guid ViewStaffBasicDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6377");
        public static Guid EditStaffBasicDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6378");
        public static Guid ViewStaffEmploymentDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6379");
        public static Guid EditStaffEmploymentDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC637A");
        public static Guid ViewAllStaffPerformanceDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC637B");
        public static Guid ViewManagedStaffPerformanceDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC637C");
        public static Guid ViewOwnStaffPerformanceDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC637D");
        public static Guid ViewLeaversStaffPerformanceDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC637E");
        public static Guid EditAllStaffPerformanceDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC637F");
        public static Guid EditManagedStaffPerformanceDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6380");
        public static Guid EditOwnStaffPerformanceDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6381");
        public static Guid ViewContactDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6382");
        public static Guid EditContactDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6383");
        public static Guid ViewAgentDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6384");
        public static Guid EditAgentDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6385");
        public static Guid ViewAgencyDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6386");
        public static Guid EditAgencyDetails { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6387");
        public static Guid ViewEnquiries { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6388");
        public static Guid EditEnquiries { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6389");
        public static Guid ViewApplications { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC638A");
        public static Guid EditApplications { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC638B");
        public static Guid ViewInterviews { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC638C");
        public static Guid EditInterviews { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC638D");
        public static Guid ViewAccounts { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC638E");
        public static Guid EditAccounts { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC638F");
        public static Guid ViewSales { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6390");
        public static Guid EditSales { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6391");
        public static Guid ViewProducts { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6392");
        public static Guid EditProducts { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6393");
        public static Guid ViewAttendanceMarks { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6394");
        public static Guid EditAttendanceMarks { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6395");
        public static Guid ViewMarksheets { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6396");
        public static Guid UpdateOwnMarksheets { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6397");
        public static Guid UpdateAllMarksheets { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6398");
        public static Guid ViewAspects { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6399");
        public static Guid EditAspects { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC639A");
        public static Guid ViewGradeSets { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC639B");
        public static Guid EditGradeSets { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC639C");
        public static Guid ViewResultSets { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC639D");
        public static Guid EditResultSets { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC639E");
        public static Guid ViewMarksheetTemplates { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC639F");
        public static Guid EditMarksheetTemplates { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6400");
        public static Guid ViewCommentBanks { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6401");
        public static Guid EditCommentBanks { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6402");
        public static Guid ViewProfileSessions { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6403");
        public static Guid EditProfileSessions { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6404");
        public static Guid EditOwnEntries { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6405");
        public static Guid EditAllEntries { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6406");
        public static Guid ViewUsers { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6407");
        public static Guid EditUsers { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6408");
        public static Guid ViewUserGroups { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6409");
        public static Guid EditUserGroups { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC640A");
        public static Guid EditSettings { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC640B");
        public static Guid EditAcademicYears { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC640C");
        public static Guid ViewHomework { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC640D");
        public static Guid EditHomework { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC640E");
        public static Guid BehaviourSettings { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC640F");
        public static Guid AttendanceSettings { get; } = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6410");

        #region Permission Groups

        public static Guid[] GetAll()
        {
            return new List<Guid>
            {
                ViewEditPastoralStructure,
                ViewEditAcademicStructure,
                ViewStudyTopics,
                EditStudyTopics,
                DeleteStudyTopics,
                ViewLessonPlans,
                EditLessonPlans,
                DeleteLessonPlans,
                ViewSchoolDetails,
                EditSchoolDetails,
                ViewSchoolDocuments,
                EditSchoolDocuments,
                ViewSchoolDiary,
                EditSchoolDiary,
                RunArrangeCover,
                ViewStudentDetails,
                EditStudentDetails,
                ViewStudentOverview,
                EditStudentOverview,
                ViewSenDetails,
                EditSenDetails,
                ViewExclusions,
                EditExclusions,
                ViewIncidents,
                EditIncidents,
                ViewAchievements,
                EditAchievements,
                ViewDetentions,
                EditDetentions,
                ViewReportCard,
                EditReportCard,
                AddReportCard,
                DeleteReportCard,
                ViewStaffBasicDetails,
                EditStaffBasicDetails,
                ViewStaffEmploymentDetails,
                EditStaffEmploymentDetails,
                ViewAllStaffPerformanceDetails,
                ViewManagedStaffPerformanceDetails,
                ViewOwnStaffPerformanceDetails,
                ViewLeaversStaffPerformanceDetails,
                EditAllStaffPerformanceDetails,
                EditManagedStaffPerformanceDetails,
                EditOwnStaffPerformanceDetails,
                ViewContactDetails,
                EditContactDetails,
                ViewAgentDetails,
                EditAgentDetails,
                ViewAgencyDetails,
                EditAgencyDetails,
                ViewEnquiries,
                EditEnquiries,
                ViewApplications,
                EditApplications,
                ViewInterviews,
                EditInterviews,
                ViewAccounts,
                EditAccounts,
                ViewSales,
                EditSales,
                ViewProducts,
                EditProducts,
                ViewAttendanceMarks,
                EditAttendanceMarks,
                ViewMarksheets,
                UpdateOwnMarksheets,
                UpdateAllMarksheets,
                ViewAspects,
                EditAspects,
                ViewGradeSets,
                EditGradeSets,
                ViewResultSets,
                EditResultSets,
                ViewMarksheetTemplates,
                EditMarksheetTemplates,
                ViewCommentBanks,
                EditCommentBanks,
                ViewProfileSessions,
                EditProfileSessions,
                EditOwnEntries,
                EditAllEntries,
                ViewUsers,
                EditUsers,
                ViewUserGroups,
                EditUserGroups,
                EditSettings,
                EditAcademicYears,
                ViewHomework,
                EditHomework,
                BehaviourSettings,
                AttendanceSettings
            }.ToArray();
        }

        public static Guid[] GetViewStaffPerformanceDetailsGroup()
        {
            return new List<Guid>
            {
                ViewAllStaffPerformanceDetails,
                ViewManagedStaffPerformanceDetails,
                ViewOwnStaffPerformanceDetails,
                ViewLeaversStaffPerformanceDetails
            }.ToArray();
        }

        public static Guid[] GetEditStaffPerformanceDetailsGroup()
        {
            return new List<Guid>
            {
                EditAllStaffPerformanceDetails,
                EditManagedStaffPerformanceDetails,
                EditOwnStaffPerformanceDetails
            }.ToArray();
        }

        #endregion
    }
}
