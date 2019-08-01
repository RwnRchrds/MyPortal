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

            };

            var assessmentResults = new List<AssessmentResult>
            {

            };
            
            var attendanceCodes = new List<AttendanceRegisterCode>
            {
                
            };

            var attendanceMarks = new List<AttendanceRegisterMark>
            {

            };

            var attendanceMeanings = new List<AttendanceRegisterCodeMeaning>
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
            
            var behaviourLocations = new List<BehaviourLocation>
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
                new CurriculumAcademicYear {Id = 1, Name = "2019", FirstDate = new DateTime(2019, 01, 01), LastDate = new DateTime(2019,12,31)}
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

            };
            
            var financeProducts = new List<FinanceProduct>
            {
                
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
                
            };
            
            var pastoralRegGroups = new List<PastoralRegGroup>
            {
                
            };

            var pastoralYearGroups = new List<PastoralYearGroup>
            {

            };

            var personDocuments = new List<PersonDocument>
            {

            };

            var personTypes = new List<PersonType>
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
                
            };
            
            var profileCommentBanks = new List<ProfileCommentBank>
            {
                
            };
            
            var profileComments = new List<ProfileComment>
            {
                
            };
            
            
        }

        public static void InitialiseMaps()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }
    }
}