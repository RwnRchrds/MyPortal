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

            #region CurriculumAcademicYears

            var academicYears = new List<CurriculumAcademicYear>
            {
                new CurriculumAcademicYear
                    {FirstDate = DateTime.Parse("01/01/2019"), LastDate = DateTime.Parse("01/01/2020"), Id = 1, Name = "First"}
            };
            #endregion

            #region FinanceBasketItems
            var basketItems = new List<FinanceBasketItem>
            {
                new FinanceBasketItem {Id = 1, StudentId = 1, ProductId = 1},
                new FinanceBasketItem {Id = 2, ProductId = 1, StudentId = 1},
                new FinanceBasketItem {Id = 3, ProductId = 1, StudentId = 1},
                new FinanceBasketItem {Id = 4, ProductId = 3, StudentId = 3}
            };
            #endregion
            
            #region ProfileProfileCommentBanks

            var commentBanks = new List<ProfileCommentBank>
            {
                new ProfileCommentBank {Name = "Opening", Id = 1},
                new ProfileCommentBank {Name = "Middle", Id = 2},
                new ProfileCommentBank {Name = "Closing", Id = 3}
            };
            #endregion
            
            #region ProfileComments

            var comments = new List<ProfileComment>
            {
               new ProfileComment {CommentBankId = 1, Value = "Hello"},
               new ProfileComment {CommentBankId = 2, Value = "<he> works very hard"},
               new ProfileComment {CommentBankId = 2, Value = "<he> needs to improve his work"},
               new ProfileComment {CommentBankId = 3, Value = "Thank you"}
            };
            #endregion

            #region Documents
            var documents = new List<CoreDocument>
            {
                new CoreDocument
                {
                    Description = "Doc1", Url = "http://ftp.test.com/doc1", Date = DateTime.Today, IsGeneral = true,
                    Approved = true, UploaderId = 1
                },
                new CoreDocument
                {
                    Description = "Doc2", Url = "http://ftp.test.com/doc2", Date = DateTime.Today, IsGeneral = true,
                    Approved = true, UploaderId = 1
                },
                new CoreDocument
                {
                    Description = "Doc3", Url = "http://ftp.test.com/doc3", Date = DateTime.Today, IsGeneral = true,
                    Approved = false, UploaderId = 1
                },
                new CoreDocument
                {
                    Description = "Doc4", Url = "http://ftp.test.com/doc4", Date = DateTime.Today, IsGeneral = false,
                    Approved = true, UploaderId = 1
                }
            };
            #endregion

            #region AssessmentGrades
            var grades = new List<AssessmentGrade>();
            #endregion

            #region AssessmentGradeSets
            var gradeSets = new List<AssessmentGradeSet>();
            #endregion
            
            #region CurriculumLessonPlans
            var lessonPlans = new List<CurriculumLessonPlan>
            {
                
            };
            #endregion

            #region ProfileLogs
            var logs = new List<ProfileLog>
            {
                new ProfileLog {Date = DateTime.Now, AuthorId = 3, Message = "Test", StudentId = 3, TypeId = 1, AcademicYearId = 1},
                new ProfileLog {Date = DateTime.Now, AuthorId = 3, Message = "Test2", StudentId = 3, TypeId = 2, AcademicYearId = 1},
                new ProfileLog {Date = DateTime.Today, AuthorId = 3, Message = "Test3", StudentId = 3, TypeId = 3, AcademicYearId = 1},
                new ProfileLog {Date = DateTime.Today, AuthorId = 3, Message = "Test4", StudentId = 3, TypeId = 4, AcademicYearId = 1}
            };
            #endregion

            #region ProfileLogTypes
            var logTypes = new List<ProfileLogType>
            {
                new ProfileLogType {Name = "Type 1", Id = 1},
                new ProfileLogType {Name = "Type 2", Id = 2},
                new ProfileLogType {Name = "Type 3", Id = 3},
                new ProfileLogType {Name = "Type 4", Id = 4}
            };
            #endregion

            #region FinanceProducts
            var products = new List<FinanceProduct>
            {
                new FinanceProduct
                {
                    Id = 1, Description = "Art Pack", Price = (decimal) 7.50, OnceOnly = false, Visible = true
                },
                new FinanceProduct
                {
                    Id = 2, Description = "School Dinner", OnceOnly = false, Visible = false, Price = (decimal) 1.50
                },
                new FinanceProduct
                {
                    Id = 3, Description = "School Trip", OnceOnly = true, Visible = true, Price = (decimal) 100.00
                },
                new FinanceProduct
                {
                    Id = 4, Description = "Delete Me", OnceOnly = false, Visible = true, Price = 35.99m
                }
            };
            #endregion

            #region PastoralRegGroups
            var regGroups = new List<PastoralRegGroup>
            {
                new PastoralRegGroup {Id = 1, Name = "1A", TutorId = 1, YearGroupId = 1},
                new PastoralRegGroup {Id = 2, Name = "4A", TutorId = 1, YearGroupId = 2},
                new PastoralRegGroup {Id = 3, Name = "7A", YearGroupId = 3, TutorId = 1},
                new PastoralRegGroup {Id = 4, Name = "11A", YearGroupId = 4, TutorId = 1}
            };
            #endregion

            #region AssessmentResults
            var results = new List<AssessmentResult>
            {
                new AssessmentResult {StudentId = 1, SubjectId = 1, ResultSetId = 1, Value = "A"},
                new AssessmentResult {StudentId = 1, SubjectId = 2, ResultSetId = 1, Value = "C"}
            };
            #endregion

            #region AssessmentResultSets
            var resultSets = new List<AssessmentResultSet>
            {
                new AssessmentResultSet {Id = 1, Name = "Current", IsCurrent = true, AcademicYearId = 1},
                new AssessmentResultSet {Id = 2, Name = "Old", IsCurrent = false, AcademicYearId = 1}
            };
            #endregion

            #region FinanceSales
            var sales = new List<FinanceSale>();
            #endregion

            #region CoreStaff
            var staff = new List<CoreStaffMember>
            {
                new CoreStaffMember
                {
                    Id = 1, FirstName = "Georgia", LastName = "Alibi", Code = "GAL", Email = "gal@test.com",
                    JobTitle = "Test Teacher", Title = "Mrs"
                },
                new CoreStaffMember
                {
                    Id = 2, FirstName = "Chloe", LastName = "Farrar", Code = "CFA", Title = "Mrs",
                    Email = "cfa@test.com", JobTitle = "Test Teacher"
                },

                new CoreStaffMember
                {
                    Id = 3, FirstName = "Lily", LastName = "Sprague", Code = "LSP", Title = "Mrs",
                    JobTitle = "Test SLT", Email = "lsp@test.com"
                },
                new CoreStaffMember
                {
                    Id = 4, FirstName = "William", LastName = "Townsend", Code = "WTO", Title = "Mr",
                    Email = "wto@test.com", JobTitle = "Test SLT"
                }
            };
            #endregion

            #region CoreStaffDocuments
            var staffDocuments = new List<CoreStaffDocument>();
            #endregion

            #region PersonnelObservations
            var staffObservations = new List<PersonnelObservation>();
            #endregion

            #region CoreStudents
            var students = new List<CoreStudent>
            {
                new CoreStudent
                {
                    Id = 1, FirstName = "Aaron", LastName = "Aardvark", YearGroupId = 3, Email = "aardvark1@test.com",
                    AccountBalance = (decimal) 200.00, CandidateNumber = "1234", RegGroupId = 3, Gender = "M", 
                },
                new CoreStudent
                {
                    Id = 2, FirstName = "Dorothy", LastName = "Perkins", YearGroupId = 1, Email = "dperkins1@test.com",
                    CandidateNumber = "5678", AccountBalance = (decimal) 10.00, RegGroupId = 1, Gender = "F"
                },
                new CoreStudent
                {
                    Id = 3, FirstName = "John", LastName = "Appleseed", YearGroupId = 2, RegGroupId = 2,
                    Email = "aappleseed1@test.com", AccountBalance = (decimal) 0.00, CandidateNumber = "7821", Gender = "X"
                },
                new CoreStudent
                {
                    Id = 4, FirstName = "Betty", LastName = "Newbie", YearGroupId = 4, RegGroupId = 4,
                    AccountBalance = (decimal) 100.00, Email = "betty@test.com", CandidateNumber = "6452", Gender = "F"
                }
            };
            #endregion

            #region CoreStudentDocuments
            var studentDocuments = new List<CoreStudentDocument>();
            #endregion
            
            #region CurriculumStudyTopics
            var studyTopics = new List<CurriculumStudyTopic>
            {
                
            };
            #endregion

            #region CurriculumSubjects
            var subjects = new List<CurriculumSubject>
            {
                new CurriculumSubject {Name = "English", LeaderId = 3, Code = "En"},
                new CurriculumSubject {Name = "Maths", LeaderId = 3, Code = "Ma"},
                new CurriculumSubject {Name = "Science", LeaderId = 3, Code = "Sc"}
            };
            #endregion

            #region PersonnelTrainingCertificates
            var trainingCertificates = new List<PersonnelTrainingCertificate>();
            #endregion

            #region PersonnelTrainingCourses
            var trainingCourses = new List<PersonnelTrainingCourse>();
            #endregion

            #region PersonnelTrainingStatuses
            var trainingStatuses = new List<PersonnelTrainingStatus>();
            #endregion

            #region PastoralYearGroups
            var yearGroups = new List<PastoralYearGroup>
            {
                new PastoralYearGroup {Id = 1, Name = "Year 1", KeyStage = 1, HeadId = 3},
                new PastoralYearGroup {Id = 2, Name = "Year 4", KeyStage = 2, HeadId = 3},
                new PastoralYearGroup {Id = 3, Name = "Year 7", HeadId = 3, KeyStage = 3},
                new PastoralYearGroup {Id = 4, Name = "Year 11", HeadId = 3, KeyStage = 4}
            };
            #endregion

            context.CurriculumAcademicYears.AddRange(academicYears);
            context.FinanceBasketItems.AddRange(basketItems);
            context.ProfileCommentBanks.AddRange(commentBanks);
            context.ProfileComments.AddRange(comments);
            context.CoreDocuments.AddRange(documents);
            context.AssessmentGrades.AddRange(grades);
            context.AssessmentGradeSets.AddRange(gradeSets);
            context.CurriculumLessonPlans.AddRange(lessonPlans);
            context.ProfileLogs.AddRange(logs);
            context.ProfileLogTypes.AddRange(logTypes);
            context.FinanceProducts.AddRange(products);
            context.PastoralRegGroups.AddRange(regGroups);
            context.AssessmentResults.AddRange(results);
            context.AssessmentResultSets.AddRange(resultSets);
            context.FinanceSales.AddRange(sales);
            context.CoreStaff.AddRange(staff);
            context.CoreStaffDocuments.AddRange(staffDocuments);
            context.PersonnelObservations.AddRange(staffObservations);
            context.CoreStudents.AddRange(students);
            context.CoreStudentDocuments.AddRange(studentDocuments);
            context.CurriculumStudyTopics.AddRange(studyTopics);
            context.CurriculumSubjects.AddRange(subjects);
            context.PersonnelTrainingCertificates.AddRange(trainingCertificates);
            context.PersonnelTrainingCourses.AddRange(trainingCourses);
            context.PersonnelTrainingStatuses.AddRange(trainingStatuses);
            context.PastoralYearGroups.AddRange(yearGroups);

            context.SaveChanges();

            return context;
        }

        public static void InitialiseMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CoreStudent, CoreStudentDto>();
                cfg.CreateMap<CoreStudentDto, CoreStudent>();

                cfg.CreateMap<ProfileLogDto, ProfileLog>();
                cfg.CreateMap<ProfileLog, ProfileLogDto>();

                cfg.CreateMap<PastoralYearGroupDto, PastoralYearGroup>();
                cfg.CreateMap<PastoralYearGroup, PastoralYearGroupDto>();

                cfg.CreateMap<PastoralRegGroupDto, PastoralRegGroup>();
                cfg.CreateMap<PastoralRegGroup, PastoralRegGroupDto>();

                cfg.CreateMap<CoreStaffMemberDto, CoreStaffMember>();
                cfg.CreateMap<CoreStaffMember, CoreStaffMemberDto>();

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

                cfg.CreateMap<CoreDocumentDto, CoreDocument>();
                cfg.CreateMap<CoreDocument, CoreDocumentDto>();

                cfg.CreateMap<CoreStudentDocumentDto, CoreStudentDocument>();
                cfg.CreateMap<CoreStudentDocument, CoreStudentDocumentDto>();

                cfg.CreateMap<CoreStaffDocumentDto, CoreStaffDocument>();
                cfg.CreateMap<CoreStaffDocument, CoreStaffDocumentDto>();

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