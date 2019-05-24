using System;
using System.Collections.Generic;
using AutoMapper;
using Effort;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.UnitTests.TestData
{
    public static class ContextControl
    {
        public static MyPortalDbContext GetTestData()
        {
            var effortConnection = DbConnectionFactory.CreateTransient();
            var context = new MyPortalDbContext(effortConnection);
            context.IsDebug = true;

            #region AssessmentGradeSets
            context.AssessmentGradeSets.AddRange(new List<AssessmentGradeSet>
            {

            });
            #endregion

            #region AssessmentGrades
            context.AssessmentGrades.AddRange(new List<AssessmentGrade>
            {

            });
            #endregion

            #region AssessmentResultSets
            context.AssessmentResultSets.AddRange(new List<AssessmentResultSet>
            {
                new AssessmentResultSet {Id = 1, Name = "Current", IsCurrent = true, AcademicYearId = 1},
                new AssessmentResultSet {Id = 2, Name = "Old", IsCurrent = false, AcademicYearId = 1}
            });
            #endregion

            #region AssessmentResults
            context.AssessmentResults.AddRange(new List<AssessmentResult>
            {
                new AssessmentResult {StudentId = 1, SubjectId = 1, ResultSetId = 1, Value = "A"},
                new AssessmentResult {StudentId = 1, SubjectId = 2, ResultSetId = 1, Value = "C"}
            });
            #endregion

            #region AttendanceCodes
            context.AttendanceCodes.AddRange(new List<AttendanceRegisterCode>
            {

            });
            #endregion

            #region AttendanceMarks
            context.AttendanceMarks.AddRange(new List<AttendanceRegisterMark>
            {

            });
            #endregion

            #region AttendanceMeanings
            context.AttendanceMeanings.AddRange(new List<AttendanceRegisterCodeMeaning>
            {

            });
            #endregion

            #region AttendancePeriods
            context.AttendancePeriods.AddRange(new List<AttendancePeriod>
            {

            });
            #endregion

            #region AttendanceWeeks
            context.AttendanceWeeks.AddRange(new List<AttendanceWeek>
            {

            });
            #endregion

            #region CurriculumAcademicYears
            context.CurriculumAcademicYears.AddRange(new List<CurriculumAcademicYear>
            {
                new CurriculumAcademicYear
                    {FirstDate = DateTime.Parse("01/01/2019"), LastDate = DateTime.Parse("01/01/2020"), Id = 1, Name = "First"}
            });
            #endregion

            #region CurriculumClassEnrollments
            context.CurriculumClassEnrolments.AddRange(new List<CurriculumClassEnrolment>
            {

            });
            #endregion

            #region CurriculumClasses
            context.CurriculumClasses.AddRange(new List<CurriculumClass>
            {

            });
            #endregion

            #region CurriculumClassPeriods
            context.CurriculumClassPeriods.AddRange(new List<CurriculumClassPeriod>
            {

            });
            #endregion

            #region CurriculumLessonPlans
            context.CurriculumLessonPlans.AddRange(new List<CurriculumLessonPlan>
            {

            });
            #endregion

            #region CurriculumLessonPlanTemplates
            context.CurriculumLessonPlanTemplates.AddRange(new List<CurriculumLessonPlanTemplate>
            {

            });
            #endregion

            #region CurriculumStudyTopics
            context.CurriculumStudyTopics.AddRange(new List<CurriculumStudyTopic>
            {

            });
            #endregion

            #region CurriculumSubjects
            context.CurriculumSubjects.AddRange(new List<CurriculumSubject>
            {
                new CurriculumSubject {Name = "English", LeaderId = 3, Code = "En", Id = 1},
                new CurriculumSubject {Name = "Maths", LeaderId = 3, Code = "Ma", Id = 2},
                new CurriculumSubject {Name = "Science", LeaderId = 3, Code = "Sc", Id = 3}
            });
            #endregion

            #region Documents
            context.Documents.AddRange(new List<Document>
            {
                new Document
                {
                    Description = "Doc1", Url = "http://ftp.test.com/doc1", Date = DateTime.Today, IsGeneral = true,
                    Approved = true, UploaderId = 1, Id = 1
                },
                new Document
                {
                    Description = "Doc2", Url = "http://ftp.test.com/doc2", Date = DateTime.Today, IsGeneral = true,
                    Approved = true, UploaderId = 1, Id = 2
                },
                new Document
                {
                    Description = "Doc3", Url = "http://ftp.test.com/doc3", Date = DateTime.Today, IsGeneral = true,
                    Approved = false, UploaderId = 1, Id = 3
                },
                new Document
                {
                    Description = "Doc4", Url = "http://ftp.test.com/doc4", Date = DateTime.Today, IsGeneral = false,
                    Approved = true, UploaderId = 1, Id = 4
                }
            });
            #endregion

            #region FinanceBasketItems
            context.FinanceBasketItems.AddRange(new List<FinanceBasketItem>
            {
                new FinanceBasketItem {Id = 1, StudentId = 1, ProductId = 1},
                new FinanceBasketItem {Id = 2, ProductId = 1, StudentId = 1},
                new FinanceBasketItem {Id = 3, ProductId = 1, StudentId = 1},
                new FinanceBasketItem {Id = 4, ProductId = 3, StudentId = 3}
            });
            #endregion

            #region FinanceProducts
            context.FinanceProducts.AddRange(new List<FinanceProduct>
            {
                new FinanceProduct
                {
                    Id = 1, Description = "Art Pack", Price = (decimal) 7.50m, OnceOnly = false, Visible = true, ProductTypeId = 1
                },
                new FinanceProduct
                {
                    Id = 2, Description = "School Dinner", OnceOnly = false, Visible = false, Price = (decimal) 1.50m, ProductTypeId = 2
                },
                new FinanceProduct
                {
                    Id = 3, Description = "School Trip", OnceOnly = true, Visible = true, Price = (decimal) 100.00m, ProductTypeId = 1
                },
                new FinanceProduct
                {
                    Id = 4, Description = "Delete Me", OnceOnly = false, Visible = true, Price = 35.99m, ProductTypeId = 1
                }
            });
            #endregion

            #region FinanceProductTypes
            context.FinanceProductTypes.AddRange(new List<FinanceProductType>
            {
                new FinanceProductType{Id = 1, Description = "Test Product", IsMeal = false, System = true},
                new FinanceProductType {Id = 2, Description = "Test Meal", IsMeal = true, System = true}
            });
            #endregion

            #region FinanceSales
            context.FinanceSales.AddRange(new List<FinanceSale>
            {

            });
            #endregion

            #region PastoralRegGroups
            context.PastoralRegGroups.AddRange(new List<PastoralRegGroup>
            {
                new PastoralRegGroup {Id = 1, Name = "1A", TutorId = 1, YearGroupId = 1},
                new PastoralRegGroup {Id = 2, Name = "4A", TutorId = 1, YearGroupId = 2},
                new PastoralRegGroup {Id = 3, Name = "7A", YearGroupId = 3, TutorId = 1},
                new PastoralRegGroup {Id = 4, Name = "11A", YearGroupId = 4, TutorId = 1}
            });
            #endregion

            #region PastoralYearGroups
            context.PastoralYearGroups.AddRange(new List<PastoralYearGroup>
            {
                new PastoralYearGroup {Id = 1, Name = "Year 1", KeyStage = 1, HeadId = 3},
                new PastoralYearGroup {Id = 2, Name = "Year 4", KeyStage = 2, HeadId = 3},
                new PastoralYearGroup {Id = 3, Name = "Year 7", HeadId = 3, KeyStage = 3},
                new PastoralYearGroup {Id = 4, Name = "Year 11", HeadId = 3, KeyStage = 4}
            });
            #endregion

            #region PersonnelObservations
            context.PersonnelObservations.AddRange(new List<PersonnelObservation>
            {

            });
            #endregion

            #region PersonnelTrainingCertificates
            context.PersonnelTrainingCertificates.AddRange(new List<PersonnelTrainingCertificate>
            {

            });
            #endregion

            #region PersonnelTrainingCourses
            context.PersonnelTrainingCourses.AddRange(new List<PersonnelTrainingCourse>
            {

            });
            #endregion

            #region PersonnelTrainingStatuses
            context.PersonnelTrainingStatuses.AddRange(new List<PersonnelTrainingStatus>
            {

            });
            #endregion

            #region ProfileComments
            context.ProfileComments.AddRange(new List<ProfileComment>
            {
                new ProfileComment {CommentBankId = 1, Value = "Hello", Id = 1},
                new ProfileComment {CommentBankId = 2, Value = "<he> works very hard", Id = 2},
                new ProfileComment {CommentBankId = 2, Value = "<he> needs to improve his work", Id = 3},
                new ProfileComment {CommentBankId = 3, Value = "Thank you", Id = 4}
            });
            #endregion

            #region ProfileCommentBanks
            context.ProfileCommentBanks.AddRange(new List<ProfileCommentBank>
            {
                new ProfileCommentBank {Name = "Opening", Id = 1},
                new ProfileCommentBank {Name = "Middle", Id = 2},
                new ProfileCommentBank {Name = "Closing", Id = 3}
            });
            #endregion

            #region ProfileLogs
            context.ProfileLogs.AddRange(new List<ProfileLog>
            {
                new ProfileLog {Date = DateTime.Now, AuthorId = 3, Message = "Test", StudentId = 3, TypeId = 1, AcademicYearId = 1, Id = 1},
                new ProfileLog {Date = DateTime.Now, AuthorId = 3, Message = "Test2", StudentId = 3, TypeId = 2, AcademicYearId = 1, Id = 2},
                new ProfileLog {Date = DateTime.Today, AuthorId = 3, Message = "Test3", StudentId = 3, TypeId = 3, AcademicYearId = 1, Id = 3},
                new ProfileLog {Date = DateTime.Today, AuthorId = 3, Message = "Test4", StudentId = 3, TypeId = 4, AcademicYearId = 1, Id = 4}
            });
            #endregion

            #region ProfileLogTypes
            context.ProfileLogTypes.AddRange(new List<ProfileLogType>
            {
                new ProfileLogType {Name = "Type 1", Id = 1},
                new ProfileLogType {Name = "Type 2", Id = 2},
                new ProfileLogType {Name = "Type 3", Id = 3},
                new ProfileLogType {Name = "Type 4", Id = 4}
            });
            #endregion

            #region SenStatuses
            context.SenStatuses.AddRange(new List<SenStatus>
            {
                new SenStatus {Id = 1, Description = "No SEN", Code = "N"}
            });
            #endregion

            #region StaffDocuments
            context.StaffDocuments.AddRange(new List<StaffDocument>
            {

            });
            #endregion

            #region StaffMembers
            context.StaffMembers.AddRange(new List<StaffMember>
            {
                new StaffMember
                {
                    Id = 1, FirstName = "Georgia", LastName = "Alibi", Code = "GAL", Email = "gal@test.com",
                    JobTitle = "Test Teacher", Title = "Mrs"
                },
                new StaffMember
                {
                    Id = 2, FirstName = "Chloe", LastName = "Farrar", Code = "CFA", Title = "Mrs",
                    Email = "cfa@test.com", JobTitle = "Test Teacher"
                },

                new StaffMember
                {
                    Id = 3, FirstName = "Lily", LastName = "Sprague", Code = "LSP", Title = "Mrs",
                    JobTitle = "Test SLT", Email = "lsp@test.com"
                },
                new StaffMember
                {
                    Id = 4, FirstName = "William", LastName = "Townsend", Code = "WTO", Title = "Mr",
                    Email = "wto@test.com", JobTitle = "Test SLT"
                }
            });
            #endregion

            #region Students
            context.Students.AddRange(new List<Student>
            {
                new Student
                {
                    Id = 1, FirstName = "Aaron", LastName = "Aardvark", YearGroupId = 3, Email = "aardvark1@test.com",
                    AccountBalance = (decimal) 200.00, CandidateNumber = "1234", RegGroupId = 3, Gender = "M", FreeSchoolMeals = false, GiftedAndTalented = false, PupilPremium = false, SenStatusId = 1
                },
                new Student
                {
                    Id = 2, FirstName = "Dorothy", LastName = "Perkins", YearGroupId = 1, Email = "dperkins1@test.com",
                    CandidateNumber = "5678", AccountBalance = (decimal) 10.00, RegGroupId = 1, Gender = "F", FreeSchoolMeals = false, GiftedAndTalented = false, PupilPremium = false, SenStatusId = 1
                },
                new Student
                {
                    Id = 3, FirstName = "John", LastName = "Appleseed", YearGroupId = 2, RegGroupId = 2,
                    Email = "aappleseed1@test.com", AccountBalance = (decimal) 0.00, CandidateNumber = "7821", Gender = "X", FreeSchoolMeals = false, GiftedAndTalented = true, PupilPremium = false, SenStatusId = 1
                },
                new Student
                {
                    Id = 4, FirstName = "Betty", LastName = "Newbie", YearGroupId = 4, RegGroupId = 4,
                    AccountBalance = (decimal) 100.00, Email = "betty@test.com", CandidateNumber = "6452", Gender = "F", FreeSchoolMeals = true, GiftedAndTalented = false, PupilPremium = false, SenStatusId = 1
                }
            });
            #endregion

            #region StudentDocuments
            context.StudentDocuments.AddRange(new List<StudentDocument>
            {

            });
            #endregion

            context.SaveChanges();

            return context;
        }

        public static void InitialiseMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Student, StudentDto>();
                cfg.CreateMap<StudentDto, Student>();

                cfg.CreateMap<ProfileLogDto, ProfileLog>();
                cfg.CreateMap<ProfileLog, ProfileLogDto>();

                cfg.CreateMap<PastoralYearGroupDto, PastoralYearGroup>();
                cfg.CreateMap<PastoralYearGroup, PastoralYearGroupDto>();

                cfg.CreateMap<PastoralRegGroupDto, PastoralRegGroup>();
                cfg.CreateMap<PastoralRegGroup, PastoralRegGroupDto>();

                cfg.CreateMap<StaffMemberDto, StaffMember>();
                cfg.CreateMap<StaffMember, StaffMemberDto>();

                cfg.CreateMap<PersonnelTrainingCertificateDto, PersonnelTrainingCertificate>();
                cfg.CreateMap<PersonnelTrainingCertificate, PersonnelTrainingCertificateDto>();

                cfg.CreateMap<PersonnelTrainingCourseDto, PersonnelTrainingCourse>();
                cfg.CreateMap<PersonnelTrainingCourse, PersonnelTrainingCourseDto>();

                cfg.CreateMap<PastoralRegGroupDto, PastoralRegGroup>();
                cfg.CreateMap<PastoralRegGroup, PastoralRegGroupDto>();

                cfg.CreateMap<AssessmentResultDto, AssessmentResult>();
                cfg.CreateMap<AssessmentResult, AssessmentResultDto>();

                cfg.CreateMap<CurriculumSubjectDto, CurriculumSubject>();
                cfg.CreateMap<CurriculumSubject, CurriculumSubjectDto>();

                cfg.CreateMap<ProfileLogTypeDto, ProfileLogType>();
                cfg.CreateMap<ProfileLogType, ProfileLogTypeDto>();

                cfg.CreateMap<FinanceProductDto, FinanceProduct>();
                cfg.CreateMap<FinanceProduct, FinanceProductDto>();

                cfg.CreateMap<FinanceSaleDto, FinanceSale>();
                cfg.CreateMap<FinanceSale, FinanceSaleDto>();

                cfg.CreateMap<FinanceBasketItemDto, FinanceBasketItem>();
                cfg.CreateMap<FinanceBasketItem, FinanceBasketItemDto>();

                cfg.CreateMap<PersonnelTrainingStatusDto, PersonnelTrainingStatus>();
                cfg.CreateMap<PersonnelTrainingStatus, PersonnelTrainingStatusDto>();

                cfg.CreateMap<DocumentDto, Document>();
                cfg.CreateMap<Document, DocumentDto>();

                cfg.CreateMap<StudentDocumentDto, StudentDocument>();
                cfg.CreateMap<StudentDocument, StudentDocumentDto>();

                cfg.CreateMap<StaffDocumentDto, StaffDocument>();
                cfg.CreateMap<StaffDocument, StaffDocumentDto>();

                cfg.CreateMap<AssessmentGradeSetDto, AssessmentGradeSet>();
                cfg.CreateMap<AssessmentGradeSet, AssessmentGradeSetDto>();

                cfg.CreateMap<AssessmentGradeDto, AssessmentGrade>();
                cfg.CreateMap<AssessmentGrade, AssessmentGradeDto>();

                cfg.CreateMap<ProfileComment, ProfileCommentDto>();
                cfg.CreateMap<ProfileCommentDto, ProfileComment>();

                cfg.CreateMap<ProfileCommentBankDto, ProfileCommentBank>();
                cfg.CreateMap<ProfileCommentBank, ProfileCommentBankDto>();

                cfg.CreateMap<CurriculumStudyTopic, CurriculumStudyTopicDto>();
                cfg.CreateMap<CurriculumStudyTopicDto, CurriculumStudyTopic>();

                cfg.CreateMap<CurriculumLessonPlan, CurriculumLessonPlanDto>();
                cfg.CreateMap<CurriculumLessonPlanDto, CurriculumLessonPlan>();
            });
        }
    }
}