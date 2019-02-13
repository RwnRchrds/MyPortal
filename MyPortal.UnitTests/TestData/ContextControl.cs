using System;
using System.Collections.Generic;
using AutoMapper;
using Effort;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.UnitTests.TestData
{
    public static class ContextControl
    {
        public static MyPortalDbContext GetTestData()
        {
            var effortConnection = DbConnectionFactory.CreateTransient();
            var context = new MyPortalDbContext(effortConnection);

            #region BasketItems
            var basketItems = new List<BasketItem>
            {
                new BasketItem {Id = 1, StudentId = 1, ProductId = 1},
                new BasketItem {Id = 2, ProductId = 1, StudentId = 1},
                new BasketItem {Id = 3, ProductId = 1, StudentId = 1},
                new BasketItem {Id = 4, ProductId = 3, StudentId = 3}
            };
            #endregion
            
            #region CommentBanks

            var commentBanks = new List<CommentBank>
            {
                new CommentBank {Name = "Opening", Id = 1},
                new CommentBank {Name = "Middle", Id = 2},
                new CommentBank {Name = "Closing", Id = 3}
            };
            #endregion
            
            #region Comments

            var comments = new List<Comment>
            {
               new Comment {CommentBankId = 1, Value = "Hello"},
               new Comment {CommentBankId = 2, Value = "<he> works very hard"},
               new Comment {CommentBankId = 2, Value = "<he> needs to improve his work"},
               new Comment {CommentBankId = 3, Value = "Thank you"}
            };
            #endregion

            #region Documents
            var documents = new List<Document>
            {
                new Document
                {
                    Description = "Doc1", Url = "http://ftp.test.com/doc1", Date = DateTime.Today, IsGeneral = true,
                    Approved = true, UploaderId = 1
                },
                new Document
                {
                    Description = "Doc2", Url = "http://ftp.test.com/doc2", Date = DateTime.Today, IsGeneral = true,
                    Approved = true, UploaderId = 1
                },
                new Document
                {
                    Description = "Doc3", Url = "http://ftp.test.com/doc3", Date = DateTime.Today, IsGeneral = true,
                    Approved = false, UploaderId = 1
                },
                new Document
                {
                    Description = "Doc4", Url = "http://ftp.test.com/doc4", Date = DateTime.Today, IsGeneral = false,
                    Approved = true, UploaderId = 1
                }
            };
            #endregion

            #region Grades
            var grades = new List<Grade>();
            #endregion

            #region GradeSets
            var gradeSets = new List<GradeSet>();
            #endregion

            #region Logs
            var logs = new List<Log>
            {
                new Log {Date = DateTime.Now, AuthorId = 3, Message = "Test", StudentId = 3, TypeId = 1},
                new Log {Date = DateTime.Now, AuthorId = 3, Message = "Test2", StudentId = 3, TypeId = 2},
                new Log {Date = DateTime.Today, AuthorId = 3, Message = "Test3", StudentId = 3, TypeId = 3},
                new Log {Date = DateTime.Today, AuthorId = 3, Message = "Test4", StudentId = 3, TypeId = 4}
            };
            #endregion

            #region LogTypes
            var logTypes = new List<LogType>
            {
                new LogType {Name = "Type 1"},
                new LogType {Name = "Type 2"},
                new LogType {Name = "Type 3"},
                new LogType {Name = "Type 4"}
            };
            #endregion

            #region Products
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1, Description = "Art Pack", Price = (decimal) 7.50, OnceOnly = false, Visible = true
                },
                new Product
                {
                    Id = 2, Description = "School Dinner", OnceOnly = false, Visible = false, Price = (decimal) 1.50
                },
                new Product
                {
                    Id = 3, Description = "School Trip", OnceOnly = true, Visible = true, Price = (decimal) 100.00
                },
                new Product
                {
                    Id = 4, Description = "Delete Me", OnceOnly = false, Visible = true, Price = 35.99m
                }
            };
            #endregion

            #region RegGroups
            var regGroups = new List<RegGroup>
            {
                new RegGroup {Id = 1, Name = "1A", TutorId = 1, YearGroupId = 1},
                new RegGroup {Id = 2, Name = "4A", TutorId = 1, YearGroupId = 2},
                new RegGroup {Id = 3, Name = "7A", YearGroupId = 3, TutorId = 1},
                new RegGroup {Id = 4, Name = "11A", YearGroupId = 4, TutorId = 1}
            };
            #endregion

            #region Results
            var results = new List<Result>
            {
                new Result {StudentId = 1, SubjectId = 1, ResultSetId = 1, Value = "A"},
                new Result {StudentId = 1, SubjectId = 2, ResultSetId = 1, Value = "C"}
            };
            #endregion

            #region ResultSets
            var resultSets = new List<ResultSet>
            {
                new ResultSet {Id = 1, Name = "Current", IsCurrent = true},
                new ResultSet {Id = 2, Name = "Old", IsCurrent = false}
            };
            #endregion

            #region Sales
            var sales = new List<Sale>();
            #endregion

            #region Staff
            var staff = new List<Staff>
            {
                new Staff
                {
                    Id = 1, FirstName = "Georgia", LastName = "Alibi", Code = "GAL", Email = "gal@test.com",
                    JobTitle = "Test Teacher", Title = "Mrs"
                },
                new Staff
                {
                    Id = 2, FirstName = "Chloe", LastName = "Farrar", Code = "CFA", Title = "Mrs",
                    Email = "cfa@test.com", JobTitle = "Test Teacher"
                },

                new Staff
                {
                    Id = 3, FirstName = "Lily", LastName = "Sprague", Code = "LSP", Title = "Mrs",
                    JobTitle = "Test SLT", Email = "lsp@test.com"
                },
                new Staff
                {
                    Id = 4, FirstName = "William", LastName = "Townsend", Code = "WTO", Title = "Mr",
                    Email = "wto@test.com", JobTitle = "Test SLT"
                }
            };
            #endregion

            #region StaffDocuments
            var staffDocuments = new List<StaffDocument>();
            #endregion

            #region StaffObservations
            var staffObservations = new List<StaffObservation>();
            #endregion

            #region Students
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1, FirstName = "Aaron", LastName = "Aardvark", YearGroupId = 3, Email = "aardvark1@test.com",
                    AccountBalance = (decimal) 200.00, CandidateNumber = "1234", RegGroupId = 3, Gender = "M"
                },
                new Student
                {
                    Id = 2, FirstName = "Dorothy", LastName = "Perkins", YearGroupId = 1, Email = "dperkins1@test.com",
                    CandidateNumber = "5678", AccountBalance = (decimal) 10.00, RegGroupId = 1, Gender = "F"
                },
                new Student
                {
                    Id = 3, FirstName = "John", LastName = "Appleseed", YearGroupId = 2, RegGroupId = 2,
                    Email = "aappleseed1@test.com", AccountBalance = (decimal) 0.00, CandidateNumber = "7821", Gender = "X"
                },
                new Student
                {
                    Id = 4, FirstName = "Betty", LastName = "Newbie", YearGroupId = 4, RegGroupId = 4,
                    AccountBalance = (decimal) 100.00, Email = "betty@test.com", CandidateNumber = "6452", Gender = "F"
                }
            };
            #endregion

            #region StudentDocuments
            var studentDocuments = new List<StudentDocument>();
            #endregion

            #region Subjects
            var subjects = new List<Subject>
            {
                new Subject {Name = "English", LeaderId = 3, Code = "En"},
                new Subject {Name = "Maths", LeaderId = 3, Code = "Ma"},
                new Subject {Name = "Science", LeaderId = 3, Code = "Sc"}
            };
            #endregion

            #region TrainingCertificates
            var trainingCertificates = new List<TrainingCertificate>();
            #endregion

            #region TrainingCourses
            var trainingCourses = new List<TrainingCourse>();
            #endregion

            #region TrainingStatuses
            var trainingStatuses = new List<TrainingStatus>();
            #endregion

            #region YearGroups
            var yearGroups = new List<YearGroup>
            {
                new YearGroup {Id = 1, Name = "Year 1", KeyStage = 1, HeadId = 3},
                new YearGroup {Id = 2, Name = "Year 4", KeyStage = 2, HeadId = 3},
                new YearGroup {Id = 3, Name = "Year 7", HeadId = 3, KeyStage = 3},
                new YearGroup {Id = 4, Name = "Year 11", HeadId = 3, KeyStage = 4}
            };
            #endregion

            context.BasketItems.AddRange(basketItems);
            context.CommentBanks.AddRange(commentBanks);
            context.Comments.AddRange(comments);
            context.Documents.AddRange(documents);
            context.Grades.AddRange(grades);
            context.GradeSets.AddRange(gradeSets);
            context.Logs.AddRange(logs);
            context.LogTypes.AddRange(logTypes);
            context.Products.AddRange(products);
            context.RegGroups.AddRange(regGroups);
            context.Results.AddRange(results);
            context.ResultSets.AddRange(resultSets);
            context.Sales.AddRange(sales);
            context.Staff.AddRange(staff);
            context.StaffDocuments.AddRange(staffDocuments);
            context.StaffObservations.AddRange(staffObservations);
            context.Students.AddRange(students);
            context.StudentDocuments.AddRange(studentDocuments);
            context.Subjects.AddRange(subjects);
            context.TrainingCertificates.AddRange(trainingCertificates);
            context.TrainingCourses.AddRange(trainingCourses);
            context.TrainingStatuses.AddRange(trainingStatuses);
            context.YearGroups.AddRange(yearGroups);

            context.SaveChanges();

            return context;
        }

        public static void InitialiseMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Student, StudentDto>();
                cfg.CreateMap<StudentDto, Student>();

                cfg.CreateMap<LogDto, Log>();
                cfg.CreateMap<Log, LogDto>();

                cfg.CreateMap<YearGroupDto, YearGroup>();
                cfg.CreateMap<YearGroup, YearGroupDto>();

                cfg.CreateMap<RegGroupDto, RegGroup>();
                cfg.CreateMap<RegGroup, RegGroupDto>();

                cfg.CreateMap<StaffDto, Staff>();
                cfg.CreateMap<Staff, StaffDto>();

                cfg.CreateMap<TrainingCertificateDto, TrainingCertificate>();
                cfg.CreateMap<TrainingCertificate, TrainingCertificateDto>();

                cfg.CreateMap<TrainingCourseDto, TrainingCourse>();
                cfg.CreateMap<TrainingCourse, TrainingCourseDto>();

                cfg.CreateMap<RegGroupDto, RegGroup>();
                cfg.CreateMap<RegGroup, RegGroupDto>();

                cfg.CreateMap<ResultDto, Result>();
                cfg.CreateMap<Result, ResultDto>();

                cfg.CreateMap<SubjectDto, Subject>();
                cfg.CreateMap<Subject, SubjectDto>();

                cfg.CreateMap<LogTypeDto, LogType>();
                cfg.CreateMap<LogType, LogTypeDto>();

                cfg.CreateMap<ProductDto, Product>();
                cfg.CreateMap<Product, ProductDto>();

                cfg.CreateMap<SaleDto, Sale>();
                cfg.CreateMap<Sale, SaleDto>();

                cfg.CreateMap<BasketItemDto, BasketItem>();
                cfg.CreateMap<BasketItem, BasketItemDto>();

                cfg.CreateMap<TrainingStatusDto, TrainingStatus>();
                cfg.CreateMap<TrainingStatus, TrainingStatusDto>();

                cfg.CreateMap<DocumentDto, Document>();
                cfg.CreateMap<Document, DocumentDto>();

                cfg.CreateMap<StudentDocumentDto, StudentDocument>();
                cfg.CreateMap<StudentDocument, StudentDocumentDto>();

                cfg.CreateMap<StaffDocumentDto, StaffDocument>();
                cfg.CreateMap<StaffDocument, StaffDocumentDto>();

                cfg.CreateMap<GradeSetDto, GradeSet>();
                cfg.CreateMap<GradeSet, GradeSetDto>();

                cfg.CreateMap<GradeDto, Grade>();
                cfg.CreateMap<Grade, GradeDto>();
            });
        }
    }
}