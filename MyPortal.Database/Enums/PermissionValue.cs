namespace MyPortal.Database.Enums
{
    public enum PermissionValue
    {
        #region Admissions

        AdmissionsViewApplications = 0,
        AdmissionsEditApplications = 1,
        AdmissionsViewEnquiries = 2,
        AdmissionsEditEnquiries = 3,
        AdmissionsViewInterviews = 4,
        AdmissionsEditInterviews = 5,

        #endregion

        #region Agency

        AgencyViewAgencies = 6,
        AgencyEditAgencies = 7,

        #endregion

        #region Assessment

        AssessmentViewAspects = 8,
        AssessmentEditAspects = 9,
        AssessmentViewExamBaseData = 10,
        AssessmentEditExamBaseData = 11,
        AssessmentRunExamAsst = 12,
        AssessmentViewGradeSets = 13,
        AssessmentEditGradeSets = 14,
        AssessmentViewMarksheetTemplates = 15,
        AssessmentEditMarksheetTemplates = 16,
        AssessmentViewResultSets = 17,
        AssessmentEditResultSets = 18,
        AssessmentViewOwnMarksheets = 19,
        AssessmentViewAllMarksheets = 20,
        AssessmentUpdateOwnMarksheets = 21,
        AssessmentUpdateAllMarksheets = 22,
        AssessmentViewResults = 23,
        AssessmentViewEmbargoedResults = 24,
        AssessmentEditResults = 25,

        #endregion

        #region Attendance

        AttendanceViewAttendanceMarks = 26,
        AttendanceEditAttendanceMarks = 27,
        AttendanceUseRestrictedCodes = 123,

        #endregion

        #region Behaviour

        BehaviourViewAchievements = 28,
        BehaviourEditAchievements = 29,
        BehaviourViewIncidents = 30,
        BehaviourEditIncidents = 31,
        BehaviourViewDetentions = 32,
        BehaviourEditDetentions = 33,
        BehaviourViewExclusions = 34,
        BehaviourEditExclusions = 35,
        BehaviourViewReportCards = 36,
        BehaviourEditReportCards = 37,
        BehaviourAddRemoveReportCards = 38,

        #endregion

        #region Curriculum

        CurriculumAcademicStructure = 39,
        CurriculumEditAcademicYears = 40,
        CurriculumArrangeCover = 41,
        CurriculumViewHomework = 42,
        CurriculumEditHomework = 43,
        CurriculumViewLessonPlans = 44,
        CurriculumEditLessonPlans = 45,
        CurriculumViewStudyTopics = 46,
        CurriculumEditStudyTopics = 47,

        #endregion

        #region Finance

        FinanceViewAccounts = 48,
        FinanceEditAccounts = 49,
        FinanceViewProducts = 50,
        FinanceEditProducts = 51,
        FinanceViewBills = 52,
        FinanceEditBills = 53,
        FinanceViewCharges = 125,
        FinanceEditCharges = 126,
        FinanceViewDiscounts = 127,
        FinanceEditDiscounts = 128,

        #endregion

        #region People

        PeopleViewAgentDetails = 54,
        PeopleEditAgentDetails = 55,
        PeopleViewContactDetails = 56,
        PeopleEditContactDetails = 57,
        PeopleViewContactTasks = 58,
        PeopleEditContactTasks = 59,
        PeopleViewStaffBasicDetails = 60,
        PeopleViewStaffEmploymentDetails = 61,
        PeopleEditStaffBasicDetails = 62,
        PeopleEditStaffEmploymentDetails = 63,
        PeopleViewAllStaffDocuments = 64,
        PeopleViewManagedStaffDocuments = 65,
        PeopleViewOwnStaffDocuments = 66,
        PeopleEditAllStaffDocuments = 67,
        PeopleEditManagedStaffDocuments = 68,
        PeopleEditOwnStaffDocuments = 69,
        PeopleViewAllStaffPerformanceDetails = 70,
        PeopleViewManagedStaffPerformanceDetails = 71,
        PeopleViewOwnStaffPerformanceDetails = 72,
        PeopleEditAllStaffPerformanceDetails = 73,
        PeopleEditManagedStaffPerformanceDetails = 74,
        PeopleEditOwnStaffPerformanceDetails = 75,
        PeopleViewAllStaffTasks = 76,
        PeopleViewManagedStaffTasks = 77,
        PeopleEditAllStaffTasks = 78,
        PeopleEditManagedStaffTasks = 79,
        PeopleViewTrainingCourses = 80,
        PeopleEditTrainingCourses = 81,

        #endregion

        #region Profiles

        ProfilesViewCommentBanks = 82,
        ProfilesEditCommentBanks = 83,
        ProfilesViewReports = 84,
        ProfilesEditOwnReports = 85,
        ProfilesEditAllReports = 86,
        ProfilesViewReportingSessions = 87,
        ProfilesEditReportingSessions = 88,

        #endregion

        #region School

        SchoolPastoralStructure = 89,
        SchoolViewRooms = 90,
        SchoolEditRooms = 91,
        SchoolViewSchoolDetails = 92,
        SchoolEditSchoolDetails = 93,
        SchoolViewSchoolDiary = 94,
        SchoolEditSchoolDiary = 95,
        SchoolViewSchoolDocuments = 96,
        SchoolEditSchoolDocuments = 97,
        SchoolViewSchoolBulletins = 98,
        SchoolEditSchoolBulletins = 99,
        SchoolApproveSchoolBulletins = 124,

        #endregion

        #region Student

        StudentViewSenDetails = 100,
        StudentEditSenDetails = 101,
        StudentViewStudentDetails = 102,
        StudentEditStudentDetails = 103,
        StudentViewStudentLogNotes = 104,
        StudentEditStudentLogNotes = 105,
        StudentViewStudentTasks = 106,
        StudentEditStudentTasks = 107,
        StudentViewStudentDocuments = 108,
        StudentEditStudentDocuments = 109,
        StudentViewMedicalEvents = 110,
        StudentEditMedicalEvents = 111,
        StudentViewFinanceDetails = 129,
        StudentEditFinanceDetails = 130,

        #endregion

        #region System

        SystemViewUsers = 112,
        SystemEditUsers = 113,
        SystemViewGroups = 114,
        SystemEditGroups = 115,
        SystemSettings = 116,
        SystemAttendance = 117,
        SystemBehaviour = 118,
        SystemFinance = 119,
        SystemPerson = 120,
        SystemStaff = 121,
        SystemSen = 122

        #endregion
    }
}