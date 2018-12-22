using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.UnitTests
{
    public class ContextControl
    {
        public static void Populate(MyPortalDbContext context)
        {
            var basketItems = new List<BasketItem>
            {
                new BasketItem() { Id = 1, StudentId = 1, ProductId = 1},
                new BasketItem() {Id = 1, ProductId = 1, StudentId = 1},
                new BasketItem() {Id = 3, ProductId = 1, StudentId = 1},
            };

            var documents = new List<Document>
            {

            };

            var grades = new List<Grade>
            {

            };

            var gradeSets = new List<GradeSet>
            {

            };

            var logs = new List<Log>
            {

            };

            var logTypes = new List<LogType>
            {

            };

            var products = new List<Product>
            {
                new Product() {Id = 1, Description = "Art Pack", Price = (decimal)7.50, OnceOnly = false, Visible = true},
                new Product() {Id = 2, Description = "School Dinner", OnceOnly = false, Visible = true, Price = (decimal)1.50}
            };

            var regGroups = new List<RegGroup>
            {
                new RegGroup() {Id = 1, Name = "1A", TutorId = 1, YearGroupId = 1},
                                new RegGroup() {Id = 2, Name = "4A", TutorId = 1, YearGroupId = 2},
                                new RegGroup() {Id = 3, Name = "7A", YearGroupId = 3, TutorId = 1},
                                new RegGroup() {Id = 4, Name = "11A", YearGroupId = 4, TutorId = 1}
            };

            var results = new List<Result>
            {

            };

            var resultSets = new List<ResultSet>
            {

            };

            var sales = new List<Sale>
            {

            };

            var staff = new List<Staff>
            {
                new Staff() {Id = 1, FirstName = "Georgia", LastName = "Alibi", Code = "GAL", Email = "gal@test.com", JobTitle = "Test Teacher", Title = "Mrs"},
                new Staff() {Id = 2, FirstName = "Chloe", LastName = "Farrar", Code = "CFA", Title = "Mrs", Email = "cfa@test.com", JobTitle = "Test Teacher"},

                new Staff() {Id = 3, FirstName = "Lily", LastName = "Sprague", Code = "LSP", Title = "Mrs", JobTitle = "Test SLT", Email = "lsp@test.com"},
                new Staff() {Id = 4, FirstName = "William", LastName = "Townsend", Code = "WTO", Title = "Mr", Email = "wto@test.com", JobTitle = "Test SLT"}
            };

            var staffDocuments = new List<StaffDocument>
            {

            };

            var staffObservations = new List<StaffObservation>
            {

            };

            var students = new List<Student>
            {
                new Student() {Id = 1, FirstName = "Aaron", LastName = "Aardvark", YearGroupId = 3, Email = "aardvark1@test.com", AccountBalance = (decimal)200.00, CandidateNumber = "1234", RegGroupId = 3},
                new Student() {Id = 2, FirstName = "Dorothy", LastName = "Perkins", YearGroupId = 1, Email = "dperkins1@test.com", CandidateNumber = "5678", AccountBalance = (decimal)10.00, RegGroupId = 1},
                new Student() {Id = 3, FirstName = "John", LastName = "Appleseed", YearGroupId = 2, RegGroupId = 2, Email = "aappleseed1@test.com", AccountBalance = (decimal)0.00, CandidateNumber = "7821"},
                new Student() {Id = 4, FirstName = "Betty", LastName = "Newbie", YearGroupId = 4, RegGroupId = 4, AccountBalance = (decimal)100.00, Email = "betty@test.com", CandidateNumber = "6452"}
            };

            var studentDocuments = new List<StudentDocument>
            {

            };

            var subjects = new List<Subject>
            {

            };

            var trainingCertificates = new List<TrainingCertificate>
            {

            };

            var trainingCourses = new List<TrainingCourse>
            {

            };

            var trainingStatuses = new List<TrainingStatus>
            {

            };

            var yearGroups = new List<YearGroup>
            {
                new YearGroup() {Id = 1, Name = "Year 1", KeyStage = 1, HeadId = 3},
                new YearGroup() {Id = 2, Name = "Year 4", KeyStage = 2, HeadId = 3},
                new YearGroup() {Id = 3, Name = "Year 7", HeadId = 3, KeyStage = 3},
                new YearGroup() {Id = 4, Name = "Year 11", HeadId = 3, KeyStage = 4}
            };

            context.BasketItems.AddRange(basketItems);
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
