using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Effort;
using Moq;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Database;
using NUnit.Framework;

namespace MyPortal.UnitTests.TestData
{
    public static class ContextControl
    {
        public static MyPortalDbContext GetTestData()
        {
            var assessmentGradeSets = new List<AssessmentGradeSet>
            {
                
            };

            var assessmentGrades = new List<AssessmentGrade>
            {

            };

            var assessmentResultSets = new List<AssessmentResultSet>
            {
                new AssessmentResultSet {Id = 1, Name = "Current", AcademicYearId = 1, IsCurrent = true},
                new AssessmentResultSet {Id = 2, Name = "DeleteMe", AcademicYearId = 1, IsCurrent = false}
            };

            var assessmentResults = new List<AssessmentResult>
            {

            };
            
            var attendanceCodes = new List<AttendanceCode>
            {
                
            };

            var attendanceMarks = new List<AttendanceMark>
            {

            };

            var attendanceMeanings = new List<AttendanceMeaning>
            {

            };

            var attendancePeriods = new List<AttendancePeriod>
            {

            };
            
            var attendanceWeeks = new List<AttendanceWeek>
            {
                
            };
            
            var behaviourAchievementTypes = new List<BehaviourAchievementType>
            {
                
            };

            var behaviourAchievements = new List<BehaviourAchievement>
            {

            };
            
            var behaviourIncidentTypes = new List<BehaviourIncidentType>
            {
                
            };
            
            var behaviourIncidents = new List<BehaviourIncident>
            {
                
            };
            
            var behaviourLocations = new List<SchoolLocation>
            {
                
            };
            
            var communicationLogs = new List<CommunicationLog>
            {
                
            };
            
            var communicationTypes = new List<CommunicationType>
            {
                
            };
            
            var curriculumAcademicYears = new List<CurriculumAcademicYear>
            {
                new CurriculumAcademicYear {Id = 1, Name = "Current", FirstDate = new DateTime(DateTime.Now.Year, 01, 01), LastDate = new DateTime(DateTime.Now.Year,12,31)}
            };
            
            var curriculumClasses = new List<CurriculumClass>
            {
                
            };
            
            var curriculumEnrolments = new List<CurriculumEnrolment>
            {
                
            };
            
            var curriculumLessonPlanTemplates = new List<CurriculumLessonPlanTemplate>
            {
                
            };
            
            var curriculumLessonPlans = new List<CurriculumLessonPlan>
            {
                
            };
            
            var curriculumSessions = new List<CurriculumSession>
            {
                
            };
            
            var curriculumStudyTopics = new List<CurriculumStudyTopic>
            {
                
            };
            
            var curriculumSubjects = new List<CurriculumSubject>
            {
                
            };
            
            var documentTypes = new List<DocumentType>
            {
                
            };
            
            var documents = new List<Document>
            {
                
            };

            var financeBasketItems = new List<FinanceBasketItem>
            {

            };

            var financeProductTypes = new List<FinanceProductType>
            {
                new FinanceProductType {Id = 1, Description = "Meal", IsMeal = true, System = true} 
            };
            
            var financeProducts = new List<FinanceProduct>
            {
                new FinanceProduct {Id = 1, Deleted = false, Description = "School Dinner", Price = 1.90m, Visible = true, OnceOnly = true, ProductTypeId = 1}
            };
            
            var financeSales = new List<FinanceSale>
            {
                
            };
            
            var medicalConditions = new List<MedicalCondition>
            {
                
            };
            
            var medicalEvents = new List<MedicalEvent>
            {
                
            };
            
            var medicalStudentConditions = new List<MedicalStudentCondition>
            {
                
            };
            
            var pastoralHouses = new List<PastoralHouse>
            {
                new PastoralHouse {Id = 1, Name = "Penguins", HeadId = 2}
            };
            
            var pastoralRegGroups = new List<PastoralRegGroup>
            {
                new PastoralRegGroup {Id = 1, Name = "1A", TutorId = 3, YearGroupId = 1},
                new PastoralRegGroup {Id = 8, Name = "8A", TutorId = 4, YearGroupId = 8}
            };

            var pastoralYearGroups = new List<PastoralYearGroup>
            {
                new PastoralYearGroup {Id = 1, Name = "Year 1", KeyStage = 1, HeadId = 1},
                new PastoralYearGroup {Id = 8, Name = "Year 8", KeyStage = 3, HeadId = 2}
            };

            var personDocuments = new List<PersonDocument>
            {

            };

            var personnelObservations = new List<PersonnelObservation>
            {

            };
            
            var personnelTrainingCertificates = new List<PersonnelTrainingCertificate>
            {
                
            };

            var personnelTrainingCourses = new List<PersonnelTrainingCourse>
            {

            };
            
            var persons = new List<Person>
            {
                //Students
                new Person {Id = 1, FirstName = "Aaron", LastName = "Aardvark", Dob = new DateTime(2000,06,05), Deleted = false, Gender = "M", UserId = "aardvark"},
                new Person {Id = 2, FirstName = "Chloe", LastName = "Brown", Dob = new DateTime(2000,06,05), Deleted = false, Gender = "F", UserId = "cbrown"},
                    
                //Staff
                new Person {Id = 3, Title = "Mrs", FirstName = "Lily", LastName = "Sprague", Dob = new DateTime(1987,08,05), Deleted = false, Gender = "F", UserId = "l.sprague"},
                new Person {Id = 4, Title = "Sir", FirstName = "William", LastName = "Townsend", Dob = new DateTime(1986,04,26), Deleted = false, Gender = "M", UserId = "wtownsend"},
                new Person {Id = 5, Title = "Mrs", FirstName = "Joanne", LastName = "Cobb", Dob = new DateTime(1986,04,26), Deleted = false, Gender = "F", UserId = "jcobb"},
                new Person {Id = 6, Title = "Miss", FirstName = "Ellie", LastName = "Williams", Dob = new DateTime(1986,04,26), Deleted = false, Gender = "F", UserId = "ewilliams"}
            };
            
            var profileCommentBanks = new List<ProfileCommentBank>
            {
                
            };
            
            var profileComments = new List<ProfileComment>
            {
                
            };

            var profileLogTypes = new List<ProfileLogType>
            {
                new ProfileLogType {Id = 1, Name = "Academic Support", System = true},
                new ProfileLogType {Id = 2, Name = "Report", System = true},
                new ProfileLogType {Id = 3, Name = "Behaviour Log", System = true},
                new ProfileLogType {Id = 4, Name = "Praise", System = true}
            };
            
            var profileLogs = new List<ProfileLog>
            {
                
            };
            
            var senEvents = new List<SenEvent>
            {
                
            };

            var senEventTypes = new List<SenEventType>
            {

            };
            
            var senProvisions = new List<SenProvision>
            {
                
            };

            var senStatuses = new List<SenStatus>
            {
                new SenStatus {Id = 1, Code = "N", Description = "No SEN Status"},
                new SenStatus {Id = 2, Code = "E", Description = "School Early Years Action"}
            };
            
            var staffMembers = new List<StaffMember>
            {
                new StaffMember {Id = 1, Code = "LSP", Deleted = false, JobTitle = "Deputy Headteacher", PersonId = 3},
                new StaffMember {Id = 2, Code = "WTO", Deleted = false, JobTitle = "Headteacher", PersonId = 4},
                new StaffMember {Id = 3, Code = "JCO", Deleted = false, JobTitle = "Teacher", PersonId = 5},
                new StaffMember {Id = 4, Code = "EWI", Deleted = false, JobTitle = "Teacher", PersonId = 6}
            };
            
            var students = new List<Student>
            {
                new Student {Id = 1, Deleted = false, AccountBalance = 8.99m, PersonId = 1, PupilPremium = false, FreeSchoolMeals = false, GiftedAndTalented = false, RegGroupId = 1, SenStatusId = 1, YearGroupId = 1},
                new Student {Id = 2, Deleted = false, AccountBalance = 100m, PersonId = 2, PupilPremium = true, FreeSchoolMeals = true, GiftedAndTalented = false, RegGroupId = 8, YearGroupId = 8, SenStatusId = 2}
            };

            var effortConnection = DbConnectionFactory.CreateTransient();
            var context = new MyPortalDbContext(effortConnection);
            context.AssessmentGrades.AddRange(assessmentGrades);
            context.AssessmentGradeSets.AddRange(assessmentGradeSets);
            context.AssessmentResults.AddRange(assessmentResults);
            context.AssessmentResultSets.AddRange(assessmentResultSets);
            context.AttendancePeriods.AddRange(attendancePeriods);
            context.AttendanceCodes.AddRange(attendanceCodes);
            context.AttendanceMeanings.AddRange(attendanceMeanings);
            context.AttendanceMarks.AddRange(attendanceMarks);
            context.AttendanceWeeks.AddRange(attendanceWeeks);
            context.BehaviourAchievements.AddRange(behaviourAchievements);
            context.BehaviourAchievementTypes.AddRange(behaviourAchievementTypes);
            context.BehaviourIncidents.AddRange(behaviourIncidents);
            context.BehaviourIncidentTypes.AddRange(behaviourIncidentTypes);
            context.BehaviourLocations.AddRange(behaviourLocations);
            context.CommunicationLogs.AddRange(communicationLogs);
            context.CommunicationTypes.AddRange(communicationTypes);
            context.CurriculumAcademicYears.AddRange(curriculumAcademicYears);
            context.CurriculumClasses.AddRange(curriculumClasses);
            context.CurriculumEnrolments.AddRange(curriculumEnrolments);
            context.CurriculumLessonPlans.AddRange(curriculumLessonPlans);
            context.CurriculumLessonPlanTemplates.AddRange(curriculumLessonPlanTemplates);
            context.CurriculumSessions.AddRange(curriculumSessions);
            context.CurriculumStudyTopics.AddRange(curriculumStudyTopics);
            context.CurriculumSubjects.AddRange(curriculumSubjects);
            context.Documents.AddRange(documents);
            context.DocumentTypes.AddRange(documentTypes);
            context.FinanceBasketItems.AddRange(financeBasketItems);
            context.FinanceProducts.AddRange(financeProducts);
            context.FinanceProductTypes.AddRange(financeProductTypes);
            context.FinanceSales.AddRange(financeSales);
            context.MedicalConditions.AddRange(medicalConditions);
            context.MedicalEvents.AddRange(medicalEvents);
            context.MedicalStudentConditions.AddRange(medicalStudentConditions);
            context.PastoralHouses.AddRange(pastoralHouses);
            context.PastoralRegGroups.AddRange(pastoralRegGroups);
            context.PastoralYearGroups.AddRange(pastoralYearGroups);
            context.Persons.AddRange(persons);
            context.PersonDocuments.AddRange(personDocuments);
            context.PersonnelObservations.AddRange(personnelObservations);
            context.PersonnelTrainingCertificates.AddRange(personnelTrainingCertificates);
            context.PersonnelTrainingCourses.AddRange(personnelTrainingCourses);
            context.ProfileComments.AddRange(profileComments);
            context.ProfileCommentBanks.AddRange(profileCommentBanks);
            context.ProfileLogs.AddRange(profileLogs);
            context.ProfileLogTypes.AddRange(profileLogTypes);
            context.SenEvents.AddRange(senEvents);
            context.SenProvisions.AddRange(senProvisions);
            context.SenStatuses.AddRange(senStatuses);
            context.StaffMembers.AddRange(staffMembers);
            context.Students.AddRange(students);
            context.SaveChanges();

            return context;
        }

        public static void InitialiseMaps()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }
    }
}