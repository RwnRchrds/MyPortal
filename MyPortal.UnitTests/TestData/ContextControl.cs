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
                
            }.AsQueryable();

            var assessmentGrades = new List<AssessmentGrade>
            {

            }.AsQueryable();

            var assessmentResultSets = new List<AssessmentResultSet>
            {

            }.AsQueryable();

            var assessmentResults = new List<AssessmentResult>
            {

            }.AsQueryable();
            
            var attendanceCodes = new List<AttendanceRegisterCode>
            {
                
            }.AsQueryable();

            var attendanceMarks = new List<AttendanceRegisterMark>
            {

            }.AsQueryable();

            var attendanceMeanings = new List<AttendanceRegisterCodeMeaning>
            {

            }.AsQueryable();

            var attendancePeriods = new List<AttendancePeriod>
            {

            }.AsQueryable();
            
            var attendanceWeeks = new List<AttendanceWeek>
            {
                
            }.AsQueryable();
            
            var behaviourAchievementTypes = new List<BehaviourAchievementType>
            {
                
            }.AsQueryable();

            var behaviourAchievements = new List<BehaviourAchievement>
            {

            }.AsQueryable();
            
            var behaviourIncidentTypes = new List<BehaviourIncidentType>
            {
                
            }.AsQueryable();
            
            var behaviourIncidents = new List<BehaviourIncident>
            {
                
            }.AsQueryable();
            
            var behaviourLocations = new List<BehaviourLocation>
            {
                
            }.AsQueryable();
            
            var communicationLogs = new List<CommunicationLog>
            {
                
            }.AsQueryable();
            
            var communicationTypes = new List<CommunicationType>
            {
                
            }.AsQueryable();
            
            var curriculumAcademicYears = new List<CurriculumAcademicYear>
            {
                new CurriculumAcademicYear {Id = 1, Name = "2019", FirstDate = new DateTime(2019, 01, 01), LastDate = new DateTime(2019,12,31)}
            }.AsQueryable();
            
            var curriculumClasses = new List<CurriculumClass>
            {
                
            }.AsQueryable();
            
            var curriculumEnrolments = new List<CurriculumEnrolment>
            {
                
            }.AsQueryable();
            
            var curriculumLessonPlanTemplates = new List<CurriculumLessonPlanTemplate>
            {
                
            }.AsQueryable();
            
            var curriculumSessions = new List<CurriculumSession>
            {
                
            }.AsQueryable();
            
            var curriculumStudyTopics = new List<CurriculumStudyTopic>
            {
                
            }.AsQueryable();
            
            var curriculumSubjects = new List<CurriculumSubject>
            {
                
            }.AsQueryable();
            
            var documentTypes = new List<DocumentType>
            {
                
            }.AsQueryable();
            
            var documents = new List<Document>
            {
                
            }.AsQueryable();

            var financeBasketItems = new List<FinanceBasketItem>
            {

            }.AsQueryable();

            var financeProductTypes = new List<FinanceProductType>
            {

            }.AsQueryable();
            
            var financeProducts = new List<FinanceProduct>
            {
                
            }.AsQueryable();
            
            var financeSales = new List<FinanceSale>
            {
                
            }.AsQueryable();
            
            var medicalConditions = new List<MedicalCondition>
            {
                
            }.AsQueryable();
            
            var medicalEvents = new List<MedicalEvent>
            {
                
            }.AsQueryable();
            
            var medicalStudentConditions = new List<MedicalStudentCondition>
            {
                
            }.AsQueryable();
            
            var pastoralHouses = new List<PastoralHouse>
            {
                
            }.AsQueryable();
            
            var pastoralRegGroups = new List<PastoralRegGroup>
            {
                
            }.AsQueryable();

            var pastoralYearGroups = new List<PastoralYearGroup>
            {

            }.AsQueryable();

            var personDocuments = new List<PersonDocument>
            {

            }.AsQueryable();

            var personTypes = new List<PersonType>
            {

            }.AsQueryable();

            var personnelObservations = new List<PersonnelObservation>
            {

            }.AsQueryable();
            
            var personnelTrainingCertificates = new List<PersonnelTrainingCertificate>
            {
                
            }.AsQueryable();

            var personnelTrainingCourses = new List<PersonnelTrainingCourse>
            {

            }.AsQueryable();
            
            var persons = new List<Person>
            {
                
            }.AsQueryable();
            
            var profileCommentBanks = new List<ProfileCommentBank>
            {
                
            }.AsQueryable();
            
            var profileComments = new List<ProfileComment>
            {
                
            }.AsQueryable();

            var profileLogTypes = new List<ProfileLogType>
            {

            }.AsQueryable();
            
            var profileLogs = new List<ProfileLog>
            {
                
            }.AsQueryable();
            
            var senEvents = new List<SenEvent>
            {
                
            }.AsQueryable();
            
            var senProvisions = new List<SenProvision>
            {
                
            }.AsQueryable();

            var senStatuses = new List<SenStatus>
            {

            }.AsQueryable();
            
            var staffMembers = new List<StaffMember>
            {
                
            }.AsQueryable();
            
            var students = new List<Student>
            {
                
            }.AsQueryable();
            
            var assessmentGradeSetsDbSet = new Mock<DbSet<AssessmentGradeSet>>();
            assessmentGradeSetsDbSet.As<IQueryable<AssessmentGradeSet>>().Setup(m => m.Provider).Returns(assessmentGradeSets.Provider);
            assessmentGradeSetsDbSet.As<IQueryable<AssessmentGradeSet>>().Setup(m => m.Expression).Returns(assessmentGradeSets.Expression);
            assessmentGradeSetsDbSet.As<IQueryable<AssessmentGradeSet>>().Setup(m => m.ElementType).Returns(assessmentGradeSets.ElementType);
            assessmentGradeSetsDbSet.As<IQueryable<AssessmentGradeSet>>().Setup(m => m.GetEnumerator()).Returns(assessmentGradeSets.GetEnumerator());
            
            var assessmentGradesDbSet = new Mock<DbSet<AssessmentGrade>>();
            assessmentGradesDbSet.As<IQueryable<AssessmentGrade>>().Setup(m => m.Provider).Returns(assessmentGrades.Provider);
            assessmentGradesDbSet.As<IQueryable<AssessmentGrade>>().Setup(m => m.Expression).Returns(assessmentGrades.Expression);
            assessmentGradesDbSet.As<IQueryable<AssessmentGrade>>().Setup(m => m.ElementType).Returns(assessmentGrades.ElementType);
            assessmentGradesDbSet.As<IQueryable<AssessmentGrade>>().Setup(m => m.GetEnumerator()).Returns(assessmentGrades.GetEnumerator());
            
            var assessmentResultSetsDbSet = new Mock<DbSet<AssessmentResultSet>>();
            assessmentResultSetsDbSet.As<IQueryable<AssessmentResultSet>>().Setup(m => m.Provider).Returns(assessmentResultSets.Provider);
            assessmentResultSetsDbSet.As<IQueryable<AssessmentResultSet>>().Setup(m => m.Expression).Returns(assessmentResultSets.Expression);
            assessmentResultSetsDbSet.As<IQueryable<AssessmentResultSet>>().Setup(m => m.ElementType).Returns(assessmentResultSets.ElementType);
            assessmentResultSetsDbSet.As<IQueryable<AssessmentResultSet>>().Setup(m => m.GetEnumerator()).Returns(assessmentResultSets.GetEnumerator());
            
            var assessmentResultsDbSet = new Mock<DbSet<AssessmentResult>>();
            assessmentResultsDbSet.As<IQueryable<AssessmentResult>>().Setup(m => m.Provider).Returns(assessmentResults.Provider);
            assessmentResultsDbSet.As<IQueryable<AssessmentResult>>().Setup(m => m.Expression).Returns(assessmentResults.Expression);
            assessmentResultsDbSet.As<IQueryable<AssessmentResult>>().Setup(m => m.ElementType).Returns(assessmentResults.ElementType);
            assessmentResultsDbSet.As<IQueryable<AssessmentResult>>().Setup(m => m.GetEnumerator()).Returns(assessmentResults.GetEnumerator());
            
            var attendanceCodesDbSet = new Mock<DbSet<AttendanceRegisterCode>>();
            attendanceCodesDbSet.As<IQueryable<AttendanceRegisterCode>>().Setup(m => m.Provider).Returns(attendanceCodes.Provider);
            attendanceCodesDbSet.As<IQueryable<AttendanceRegisterCode>>().Setup(m => m.Expression).Returns(attendanceCodes.Expression);
            attendanceCodesDbSet.As<IQueryable<AttendanceRegisterCode>>().Setup(m => m.ElementType).Returns(attendanceCodes.ElementType);
            attendanceCodesDbSet.As<IQueryable<AttendanceRegisterCode>>().Setup(m => m.GetEnumerator()).Returns(attendanceCodes.GetEnumerator());
            
            var attendanceMarksDbSet = new Mock<DbSet<AttendanceRegisterMark>>();
            attendanceMarksDbSet.As<IQueryable<AttendanceRegisterMark>>().Setup(m => m.Provider).Returns(attendanceMarks.Provider);
            attendanceMarksDbSet.As<IQueryable<AttendanceRegisterMark>>().Setup(m => m.Expression).Returns(attendanceMarks.Expression);
            attendanceMarksDbSet.As<IQueryable<AttendanceRegisterMark>>().Setup(m => m.ElementType).Returns(attendanceMarks.ElementType);
            attendanceMarksDbSet.As<IQueryable<AttendanceRegisterMark>>().Setup(m => m.GetEnumerator()).Returns(attendanceMarks.GetEnumerator());
            
            var attendanceMeaningsDbSet = new Mock<DbSet<AttendanceRegisterCodeMeaning>>();
            attendanceMeaningsDbSet.As<IQueryable<AttendanceRegisterCodeMeaning>>().Setup(m => m.Provider).Returns(attendanceMeanings.Provider);
            attendanceMeaningsDbSet.As<IQueryable<AttendanceRegisterCodeMeaning>>().Setup(m => m.Expression).Returns(attendanceMeanings.Expression);
            attendanceMeaningsDbSet.As<IQueryable<AttendanceRegisterCodeMeaning>>().Setup(m => m.ElementType).Returns(attendanceMeanings.ElementType);
            attendanceMeaningsDbSet.As<IQueryable<AttendanceRegisterCodeMeaning>>().Setup(m => m.GetEnumerator()).Returns(attendanceMeanings.GetEnumerator());
            
            var attendancePeriodsDbSet = new Mock<DbSet<AttendancePeriod>>();
            attendancePeriodsDbSet.As<IQueryable<AttendancePeriod>>().Setup(m => m.Provider).Returns(attendancePeriods.Provider);
            attendancePeriodsDbSet.As<IQueryable<AttendancePeriod>>().Setup(m => m.Expression).Returns(attendancePeriods.Expression);
            attendancePeriodsDbSet.As<IQueryable<AttendancePeriod>>().Setup(m => m.ElementType).Returns(attendancePeriods.ElementType);
            attendancePeriodsDbSet.As<IQueryable<AttendancePeriod>>().Setup(m => m.GetEnumerator()).Returns(attendancePeriods.GetEnumerator());
            
            var attendanceWeeksDbSet = new Mock<DbSet<AttendanceWeek>>();
            attendanceWeeksDbSet.As<IQueryable<AttendanceWeek>>().Setup(m => m.Provider).Returns(attendanceWeeks.Provider);
            attendanceWeeksDbSet.As<IQueryable<AttendanceWeek>>().Setup(m => m.Expression).Returns(attendanceWeeks.Expression);
            attendanceWeeksDbSet.As<IQueryable<AttendanceWeek>>().Setup(m => m.ElementType).Returns(attendanceWeeks.ElementType);
            attendanceWeeksDbSet.As<IQueryable<AttendanceWeek>>().Setup(m => m.GetEnumerator()).Returns(attendanceWeeks.GetEnumerator());
            
            var behaviourAchievementTypesDbSet = new Mock<DbSet<BehaviourAchievementType>>();
            behaviourAchievementTypesDbSet.As<IQueryable<BehaviourAchievementType>>().Setup(m => m.Provider).Returns(behaviourAchievementTypes.Provider);
            behaviourAchievementTypesDbSet.As<IQueryable<BehaviourAchievementType>>().Setup(m => m.Expression).Returns(behaviourAchievementTypes.Expression);
            behaviourAchievementTypesDbSet.As<IQueryable<BehaviourAchievementType>>().Setup(m => m.ElementType).Returns(behaviourAchievementTypes.ElementType);
            behaviourAchievementTypesDbSet.As<IQueryable<BehaviourAchievementType>>().Setup(m => m.GetEnumerator()).Returns(behaviourAchievementTypes.GetEnumerator());
            
            var behaviourAchievementsDbSet = new Mock<DbSet<BehaviourAchievement>>();
            behaviourAchievementsDbSet.As<IQueryable<BehaviourAchievement>>().Setup(m => m.Provider).Returns(behaviourAchievements.Provider);
            behaviourAchievementsDbSet.As<IQueryable<BehaviourAchievement>>().Setup(m => m.Expression).Returns(behaviourAchievements.Expression);
            behaviourAchievementsDbSet.As<IQueryable<BehaviourAchievement>>().Setup(m => m.ElementType).Returns(behaviourAchievements.ElementType);
            behaviourAchievementsDbSet.As<IQueryable<BehaviourAchievement>>().Setup(m => m.GetEnumerator()).Returns(behaviourAchievements.GetEnumerator());
            
            var behaviourIncidentTypesDbSet = new Mock<DbSet<BehaviourIncidentType>>();
            behaviourIncidentTypesDbSet.As<IQueryable<BehaviourIncidentType>>().Setup(m => m.Provider).Returns(behaviourIncidentTypes.Provider);
            behaviourIncidentTypesDbSet.As<IQueryable<BehaviourIncidentType>>().Setup(m => m.Expression).Returns(behaviourIncidentTypes.Expression);
            behaviourIncidentTypesDbSet.As<IQueryable<BehaviourIncidentType>>().Setup(m => m.ElementType).Returns(behaviourIncidentTypes.ElementType);
            behaviourIncidentTypesDbSet.As<IQueryable<BehaviourIncidentType>>().Setup(m => m.GetEnumerator()).Returns(behaviourIncidentTypes.GetEnumerator());
            
            var behaviourIncidentsDbSet = new Mock<DbSet<BehaviourIncident>>();
            behaviourIncidentsDbSet.As<IQueryable<BehaviourIncident>>().Setup(m => m.Provider).Returns(behaviourIncidents.Provider);
            behaviourIncidentsDbSet.As<IQueryable<BehaviourIncident>>().Setup(m => m.Expression).Returns(behaviourIncidents.Expression);
            behaviourIncidentsDbSet.As<IQueryable<BehaviourIncident>>().Setup(m => m.ElementType).Returns(behaviourIncidents.ElementType);
            behaviourIncidentsDbSet.As<IQueryable<BehaviourIncident>>().Setup(m => m.GetEnumerator()).Returns(behaviourIncidents.GetEnumerator());
            
            var behaviourLocationsDbSet = new Mock<DbSet<BehaviourLocation>>();
            behaviourLocationsDbSet.As<IQueryable<BehaviourLocation>>().Setup(m => m.Provider).Returns(behaviourLocations.Provider);
            behaviourLocationsDbSet.As<IQueryable<BehaviourLocation>>().Setup(m => m.Expression).Returns(behaviourLocations.Expression);
            behaviourLocationsDbSet.As<IQueryable<BehaviourLocation>>().Setup(m => m.ElementType).Returns(behaviourLocations.ElementType);
            behaviourLocationsDbSet.As<IQueryable<BehaviourLocation>>().Setup(m => m.GetEnumerator()).Returns(behaviourLocations.GetEnumerator());
            
            var communicationLogsDbSet = new Mock<DbSet<CommunicationLog>>();
            communicationLogsDbSet.As<IQueryable<CommunicationLog>>().Setup(m => m.Provider).Returns(communicationLogs.Provider);
            communicationLogsDbSet.As<IQueryable<CommunicationLog>>().Setup(m => m.Expression).Returns(communicationLogs.Expression);
            communicationLogsDbSet.As<IQueryable<CommunicationLog>>().Setup(m => m.ElementType).Returns(communicationLogs.ElementType);
            communicationLogsDbSet.As<IQueryable<CommunicationLog>>().Setup(m => m.GetEnumerator()).Returns(communicationLogs.GetEnumerator());
            
            var communicationTypesDbSet = new Mock<DbSet<CommunicationType>>();
            communicationTypesDbSet.As<IQueryable<CommunicationType>>().Setup(m => m.Provider).Returns(communicationTypes.Provider);
            communicationTypesDbSet.As<IQueryable<CommunicationType>>().Setup(m => m.Expression).Returns(communicationTypes.Expression);
            communicationTypesDbSet.As<IQueryable<CommunicationType>>().Setup(m => m.ElementType).Returns(communicationTypes.ElementType);
            communicationTypesDbSet.As<IQueryable<CommunicationType>>().Setup(m => m.GetEnumerator()).Returns(communicationTypes.GetEnumerator());
            
            var curriculumAcademicYearsDbSet = new Mock<DbSet<CurriculumAcademicYear>>();
            curriculumAcademicYearsDbSet.As<IQueryable<CurriculumAcademicYear>>().Setup(m => m.Provider).Returns(curriculumAcademicYears.Provider);
            curriculumAcademicYearsDbSet.As<IQueryable<CurriculumAcademicYear>>().Setup(m => m.Expression).Returns(curriculumAcademicYears.Expression);
            curriculumAcademicYearsDbSet.As<IQueryable<CurriculumAcademicYear>>().Setup(m => m.ElementType).Returns(curriculumAcademicYears.ElementType);
            curriculumAcademicYearsDbSet.As<IQueryable<CurriculumAcademicYear>>().Setup(m => m.GetEnumerator()).Returns(curriculumAcademicYears.GetEnumerator());
            
            var curriculumClassesDbSet = new Mock<DbSet<CurriculumClass>>();
            curriculumClassesDbSet.As<IQueryable<CurriculumClass>>().Setup(m => m.Provider).Returns(curriculumClasses.Provider);
            curriculumClassesDbSet.As<IQueryable<CurriculumClass>>().Setup(m => m.Expression).Returns(curriculumClasses.Expression);
            curriculumClassesDbSet.As<IQueryable<CurriculumClass>>().Setup(m => m.ElementType).Returns(curriculumClasses.ElementType);
            curriculumClassesDbSet.As<IQueryable<CurriculumClass>>().Setup(m => m.GetEnumerator()).Returns(curriculumClasses.GetEnumerator());
            
            var curriculumEnrolmentsDbSet = new Mock<DbSet<CurriculumEnrolment>>();
            curriculumEnrolmentsDbSet.As<IQueryable<CurriculumEnrolment>>().Setup(m => m.Provider).Returns(curriculumEnrolments.Provider);
            curriculumEnrolmentsDbSet.As<IQueryable<CurriculumEnrolment>>().Setup(m => m.Expression).Returns(curriculumEnrolments.Expression);
            curriculumEnrolmentsDbSet.As<IQueryable<CurriculumEnrolment>>().Setup(m => m.ElementType).Returns(curriculumEnrolments.ElementType);
            curriculumEnrolmentsDbSet.As<IQueryable<CurriculumEnrolment>>().Setup(m => m.GetEnumerator()).Returns(curriculumEnrolments.GetEnumerator());
            
            var curriculumLessonPlanTemplatesDbSet = new Mock<DbSet<CurriculumLessonPlanTemplate>>();
            curriculumLessonPlanTemplatesDbSet.As<IQueryable<CurriculumLessonPlanTemplate>>().Setup(m => m.Provider).Returns(curriculumLessonPlanTemplates.Provider);
            curriculumLessonPlanTemplatesDbSet.As<IQueryable<CurriculumLessonPlanTemplate>>().Setup(m => m.Expression).Returns(curriculumLessonPlanTemplates.Expression);
            curriculumLessonPlanTemplatesDbSet.As<IQueryable<CurriculumLessonPlanTemplate>>().Setup(m => m.ElementType).Returns(curriculumLessonPlanTemplates.ElementType);
            curriculumLessonPlanTemplatesDbSet.As<IQueryable<CurriculumLessonPlanTemplate>>().Setup(m => m.GetEnumerator()).Returns(curriculumLessonPlanTemplates.GetEnumerator());
            
            var curriculumSessionsDbSet = new Mock<DbSet<CurriculumSession>>();
            curriculumSessionsDbSet.As<IQueryable<CurriculumSession>>().Setup(m => m.Provider).Returns(curriculumSessions.Provider);
            curriculumSessionsDbSet.As<IQueryable<CurriculumSession>>().Setup(m => m.Expression).Returns(curriculumSessions.Expression);
            curriculumSessionsDbSet.As<IQueryable<CurriculumSession>>().Setup(m => m.ElementType).Returns(curriculumSessions.ElementType);
            curriculumSessionsDbSet.As<IQueryable<CurriculumSession>>().Setup(m => m.GetEnumerator()).Returns(curriculumSessions.GetEnumerator());
            
            var curriculumStudyTopicsDbSet = new Mock<DbSet<CurriculumStudyTopic>>();
            curriculumStudyTopicsDbSet.As<IQueryable<CurriculumStudyTopic>>().Setup(m => m.Provider).Returns(curriculumStudyTopics.Provider);
            curriculumStudyTopicsDbSet.As<IQueryable<CurriculumStudyTopic>>().Setup(m => m.Expression).Returns(curriculumStudyTopics.Expression);
            curriculumStudyTopicsDbSet.As<IQueryable<CurriculumStudyTopic>>().Setup(m => m.ElementType).Returns(curriculumStudyTopics.ElementType);
            curriculumStudyTopicsDbSet.As<IQueryable<CurriculumStudyTopic>>().Setup(m => m.GetEnumerator()).Returns(curriculumStudyTopics.GetEnumerator());
            
            var curriculumSubjectsDbSet = new Mock<DbSet<CurriculumSubject>>();
            curriculumSubjectsDbSet.As<IQueryable<CurriculumSubject>>().Setup(m => m.Provider).Returns(curriculumSubjects.Provider);
            curriculumSubjectsDbSet.As<IQueryable<CurriculumSubject>>().Setup(m => m.Expression).Returns(curriculumSubjects.Expression);
            curriculumSubjectsDbSet.As<IQueryable<CurriculumSubject>>().Setup(m => m.ElementType).Returns(curriculumSubjects.ElementType);
            curriculumSubjectsDbSet.As<IQueryable<CurriculumSubject>>().Setup(m => m.GetEnumerator()).Returns(curriculumSubjects.GetEnumerator());
            
            var documentTypesDbSet = new Mock<DbSet<DocumentType>>();
            documentTypesDbSet.As<IQueryable<DocumentType>>().Setup(m => m.Provider).Returns(documentTypes.Provider);
            documentTypesDbSet.As<IQueryable<DocumentType>>().Setup(m => m.Expression).Returns(documentTypes.Expression);
            documentTypesDbSet.As<IQueryable<DocumentType>>().Setup(m => m.ElementType).Returns(documentTypes.ElementType);
            documentTypesDbSet.As<IQueryable<DocumentType>>().Setup(m => m.GetEnumerator()).Returns(documentTypes.GetEnumerator());
            
            var documentsDbSet = new Mock<DbSet<Document>>();
            documentsDbSet.As<IQueryable<Document>>().Setup(m => m.Provider).Returns(documents.Provider);
            documentsDbSet.As<IQueryable<Document>>().Setup(m => m.Expression).Returns(documents.Expression);
            documentsDbSet.As<IQueryable<Document>>().Setup(m => m.ElementType).Returns(documents.ElementType);
            documentsDbSet.As<IQueryable<Document>>().Setup(m => m.GetEnumerator()).Returns(documents.GetEnumerator());
            
            var financeBasketItemsDbSet = new Mock<DbSet<FinanceBasketItem>>();
            financeBasketItemsDbSet.As<IQueryable<FinanceBasketItem>>().Setup(m => m.Provider).Returns(financeBasketItems.Provider);
            financeBasketItemsDbSet.As<IQueryable<FinanceBasketItem>>().Setup(m => m.Expression).Returns(financeBasketItems.Expression);
            financeBasketItemsDbSet.As<IQueryable<FinanceBasketItem>>().Setup(m => m.ElementType).Returns(financeBasketItems.ElementType);
            financeBasketItemsDbSet.As<IQueryable<FinanceBasketItem>>().Setup(m => m.GetEnumerator()).Returns(financeBasketItems.GetEnumerator());
            
            var financeProductTypesDbSet = new Mock<DbSet<FinanceProductType>>();
            financeProductTypesDbSet.As<IQueryable<FinanceProductType>>().Setup(m => m.Provider).Returns(financeProductTypes.Provider);
            financeProductTypesDbSet.As<IQueryable<FinanceProductType>>().Setup(m => m.Expression).Returns(financeProductTypes.Expression);
            financeProductTypesDbSet.As<IQueryable<FinanceProductType>>().Setup(m => m.ElementType).Returns(financeProductTypes.ElementType);
            financeProductTypesDbSet.As<IQueryable<FinanceProductType>>().Setup(m => m.GetEnumerator()).Returns(financeProductTypes.GetEnumerator());
            
            var financeProductsDbSet = new Mock<DbSet<FinanceProduct>>();
            financeProductsDbSet.As<IQueryable<FinanceProduct>>().Setup(m => m.Provider).Returns(financeProducts.Provider);
            financeProductsDbSet.As<IQueryable<FinanceProduct>>().Setup(m => m.Expression).Returns(financeProducts.Expression);
            financeProductsDbSet.As<IQueryable<FinanceProduct>>().Setup(m => m.ElementType).Returns(financeProducts.ElementType);
            financeProductsDbSet.As<IQueryable<FinanceProduct>>().Setup(m => m.GetEnumerator()).Returns(financeProducts.GetEnumerator());
            
            var financeSalesDbSet = new Mock<DbSet<FinanceSale>>();
            financeSalesDbSet.As<IQueryable<FinanceSale>>().Setup(m => m.Provider).Returns(financeSales.Provider);
            financeSalesDbSet.As<IQueryable<FinanceSale>>().Setup(m => m.Expression).Returns(financeSales.Expression);
            financeSalesDbSet.As<IQueryable<FinanceSale>>().Setup(m => m.ElementType).Returns(financeSales.ElementType);
            financeSalesDbSet.As<IQueryable<FinanceSale>>().Setup(m => m.GetEnumerator()).Returns(financeSales.GetEnumerator());
            
            var medicalConditionsDbSet = new Mock<DbSet<MedicalCondition>>();
            medicalConditionsDbSet.As<IQueryable<MedicalCondition>>().Setup(m => m.Provider).Returns(medicalConditions.Provider);
            medicalConditionsDbSet.As<IQueryable<MedicalCondition>>().Setup(m => m.Expression).Returns(medicalConditions.Expression);
            medicalConditionsDbSet.As<IQueryable<MedicalCondition>>().Setup(m => m.ElementType).Returns(medicalConditions.ElementType);
            medicalConditionsDbSet.As<IQueryable<MedicalCondition>>().Setup(m => m.GetEnumerator()).Returns(medicalConditions.GetEnumerator());
            
            var medicalEventsDbSet = new Mock<DbSet<MedicalEvent>>();
            medicalEventsDbSet.As<IQueryable<MedicalEvent>>().Setup(m => m.Provider).Returns(medicalEvents.Provider);
            medicalEventsDbSet.As<IQueryable<MedicalEvent>>().Setup(m => m.Expression).Returns(medicalEvents.Expression);
            medicalEventsDbSet.As<IQueryable<MedicalEvent>>().Setup(m => m.ElementType).Returns(medicalEvents.ElementType);
            medicalEventsDbSet.As<IQueryable<MedicalEvent>>().Setup(m => m.GetEnumerator()).Returns(medicalEvents.GetEnumerator());
            
            var medicalStudentConditionsDbSet = new Mock<DbSet<MedicalStudentCondition>>();
            medicalStudentConditionsDbSet.As<IQueryable<MedicalStudentCondition>>().Setup(m => m.Provider).Returns(medicalStudentConditions.Provider);
            medicalStudentConditionsDbSet.As<IQueryable<MedicalStudentCondition>>().Setup(m => m.Expression).Returns(medicalStudentConditions.Expression);
            medicalStudentConditionsDbSet.As<IQueryable<MedicalStudentCondition>>().Setup(m => m.ElementType).Returns(medicalStudentConditions.ElementType);
            medicalStudentConditionsDbSet.As<IQueryable<MedicalStudentCondition>>().Setup(m => m.GetEnumerator()).Returns(medicalStudentConditions.GetEnumerator());
            
            var pastoralHousesDbSet = new Mock<DbSet<PastoralHouse>>();
            pastoralHousesDbSet.As<IQueryable<PastoralHouse>>().Setup(m => m.Provider).Returns(pastoralHouses.Provider);
            pastoralHousesDbSet.As<IQueryable<PastoralHouse>>().Setup(m => m.Expression).Returns(pastoralHouses.Expression);
            pastoralHousesDbSet.As<IQueryable<PastoralHouse>>().Setup(m => m.ElementType).Returns(pastoralHouses.ElementType);
            pastoralHousesDbSet.As<IQueryable<PastoralHouse>>().Setup(m => m.GetEnumerator()).Returns(pastoralHouses.GetEnumerator());
            
            var pastoralRegGroupsDbSet = new Mock<DbSet<PastoralRegGroup>>();
            pastoralRegGroupsDbSet.As<IQueryable<PastoralRegGroup>>().Setup(m => m.Provider).Returns(pastoralRegGroups.Provider);
            pastoralRegGroupsDbSet.As<IQueryable<PastoralRegGroup>>().Setup(m => m.Expression).Returns(pastoralRegGroups.Expression);
            pastoralRegGroupsDbSet.As<IQueryable<PastoralRegGroup>>().Setup(m => m.ElementType).Returns(pastoralRegGroups.ElementType);
            pastoralRegGroupsDbSet.As<IQueryable<PastoralRegGroup>>().Setup(m => m.GetEnumerator()).Returns(pastoralRegGroups.GetEnumerator());
            
            var pastoralYearGroupsDbSet = new Mock<DbSet<PastoralYearGroup>>();
            pastoralYearGroupsDbSet.As<IQueryable<PastoralYearGroup>>().Setup(m => m.Provider).Returns(pastoralYearGroups.Provider);
            pastoralYearGroupsDbSet.As<IQueryable<PastoralYearGroup>>().Setup(m => m.Expression).Returns(pastoralYearGroups.Expression);
            pastoralYearGroupsDbSet.As<IQueryable<PastoralYearGroup>>().Setup(m => m.ElementType).Returns(pastoralYearGroups.ElementType);
            pastoralYearGroupsDbSet.As<IQueryable<PastoralYearGroup>>().Setup(m => m.GetEnumerator()).Returns(pastoralYearGroups.GetEnumerator());
            
            var personDocumentsDbSet = new Mock<DbSet<PersonDocument>>();
            personDocumentsDbSet.As<IQueryable<PersonDocument>>().Setup(m => m.Provider).Returns(personDocuments.Provider);
            personDocumentsDbSet.As<IQueryable<PersonDocument>>().Setup(m => m.Expression).Returns(personDocuments.Expression);
            personDocumentsDbSet.As<IQueryable<PersonDocument>>().Setup(m => m.ElementType).Returns(personDocuments.ElementType);
            personDocumentsDbSet.As<IQueryable<PersonDocument>>().Setup(m => m.GetEnumerator()).Returns(personDocuments.GetEnumerator());
            
            var personTypesDbSet = new Mock<DbSet<PersonType>>();
            personTypesDbSet.As<IQueryable<PersonType>>().Setup(m => m.Provider).Returns(personTypes.Provider);
            personTypesDbSet.As<IQueryable<PersonType>>().Setup(m => m.Expression).Returns(personTypes.Expression);
            personTypesDbSet.As<IQueryable<PersonType>>().Setup(m => m.ElementType).Returns(personTypes.ElementType);
            personTypesDbSet.As<IQueryable<PersonType>>().Setup(m => m.GetEnumerator()).Returns(personTypes.GetEnumerator());
            
            var personnelObservationsDbSet = new Mock<DbSet<PersonnelObservation>>();
            personnelObservationsDbSet.As<IQueryable<PersonnelObservation>>().Setup(m => m.Provider).Returns(personnelObservations.Provider);
            personnelObservationsDbSet.As<IQueryable<PersonnelObservation>>().Setup(m => m.Expression).Returns(personnelObservations.Expression);
            personnelObservationsDbSet.As<IQueryable<PersonnelObservation>>().Setup(m => m.ElementType).Returns(personnelObservations.ElementType);
            personnelObservationsDbSet.As<IQueryable<PersonnelObservation>>().Setup(m => m.GetEnumerator()).Returns(personnelObservations.GetEnumerator());
            
            var personnelTrainingCertificatesDbSet = new Mock<DbSet<PersonnelTrainingCertificate>>();
            personnelTrainingCertificatesDbSet.As<IQueryable<PersonnelTrainingCertificate>>().Setup(m => m.Provider).Returns(personnelTrainingCertificates.Provider);
            personnelTrainingCertificatesDbSet.As<IQueryable<PersonnelTrainingCertificate>>().Setup(m => m.Expression).Returns(personnelTrainingCertificates.Expression);
            personnelTrainingCertificatesDbSet.As<IQueryable<PersonnelTrainingCertificate>>().Setup(m => m.ElementType).Returns(personnelTrainingCertificates.ElementType);
            personnelTrainingCertificatesDbSet.As<IQueryable<PersonnelTrainingCertificate>>().Setup(m => m.GetEnumerator()).Returns(personnelTrainingCertificates.GetEnumerator());
            
            var personnelTrainingCoursesDbSet = new Mock<DbSet<PersonnelTrainingCourse>>();
            personnelTrainingCoursesDbSet.As<IQueryable<PersonnelTrainingCourse>>().Setup(m => m.Provider).Returns(personnelTrainingCourses.Provider);
            personnelTrainingCoursesDbSet.As<IQueryable<PersonnelTrainingCourse>>().Setup(m => m.Expression).Returns(personnelTrainingCourses.Expression);
            personnelTrainingCoursesDbSet.As<IQueryable<PersonnelTrainingCourse>>().Setup(m => m.ElementType).Returns(personnelTrainingCourses.ElementType);
            personnelTrainingCoursesDbSet.As<IQueryable<PersonnelTrainingCourse>>().Setup(m => m.GetEnumerator()).Returns(personnelTrainingCourses.GetEnumerator());
            
            var personsDbSet = new Mock<DbSet<Person>>();
            personsDbSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(persons.Provider);
            personsDbSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(persons.Expression);
            personsDbSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(persons.ElementType);
            personsDbSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(persons.GetEnumerator());
            
            var profileCommentBanksDbSet = new Mock<DbSet<ProfileCommentBank>>();
            profileCommentBanksDbSet.As<IQueryable<ProfileCommentBank>>().Setup(m => m.Provider).Returns(profileCommentBanks.Provider);
            profileCommentBanksDbSet.As<IQueryable<ProfileCommentBank>>().Setup(m => m.Expression).Returns(profileCommentBanks.Expression);
            profileCommentBanksDbSet.As<IQueryable<ProfileCommentBank>>().Setup(m => m.ElementType).Returns(profileCommentBanks.ElementType);
            profileCommentBanksDbSet.As<IQueryable<ProfileCommentBank>>().Setup(m => m.GetEnumerator()).Returns(profileCommentBanks.GetEnumerator());
            
            var profileCommentsDbSet = new Mock<DbSet<ProfileComment>>();
            profileCommentsDbSet.As<IQueryable<ProfileComment>>().Setup(m => m.Provider).Returns(profileComments.Provider);
            profileCommentsDbSet.As<IQueryable<ProfileComment>>().Setup(m => m.Expression).Returns(profileComments.Expression);
            profileCommentsDbSet.As<IQueryable<ProfileComment>>().Setup(m => m.ElementType).Returns(profileComments.ElementType);
            profileCommentsDbSet.As<IQueryable<ProfileComment>>().Setup(m => m.GetEnumerator()).Returns(profileComments.GetEnumerator());
            
            var profileLogTypesDbSet = new Mock<DbSet<ProfileLogType>>();
            profileLogTypesDbSet.As<IQueryable<ProfileLogType>>().Setup(m => m.Provider).Returns(profileLogTypes.Provider);
            profileLogTypesDbSet.As<IQueryable<ProfileLogType>>().Setup(m => m.Expression).Returns(profileLogTypes.Expression);
            profileLogTypesDbSet.As<IQueryable<ProfileLogType>>().Setup(m => m.ElementType).Returns(profileLogTypes.ElementType);
            profileLogTypesDbSet.As<IQueryable<ProfileLogType>>().Setup(m => m.GetEnumerator()).Returns(profileLogTypes.GetEnumerator());
            
            var profileLogsDbSet = new Mock<DbSet<ProfileLog>>();
            profileLogsDbSet.As<IQueryable<ProfileLog>>().Setup(m => m.Provider).Returns(profileLogs.Provider);
            profileLogsDbSet.As<IQueryable<ProfileLog>>().Setup(m => m.Expression).Returns(profileLogs.Expression);
            profileLogsDbSet.As<IQueryable<ProfileLog>>().Setup(m => m.ElementType).Returns(profileLogs.ElementType);
            profileLogsDbSet.As<IQueryable<ProfileLog>>().Setup(m => m.GetEnumerator()).Returns(profileLogs.GetEnumerator());
            
            var senEventsDbSet = new Mock<DbSet<SenEvent>>();
            senEventsDbSet.As<IQueryable<SenEvent>>().Setup(m => m.Provider).Returns(senEvents.Provider);
            senEventsDbSet.As<IQueryable<SenEvent>>().Setup(m => m.Expression).Returns(senEvents.Expression);
            senEventsDbSet.As<IQueryable<SenEvent>>().Setup(m => m.ElementType).Returns(senEvents.ElementType);
            senEventsDbSet.As<IQueryable<SenEvent>>().Setup(m => m.GetEnumerator()).Returns(senEvents.GetEnumerator());
            
            var senProvisionsDbSet = new Mock<DbSet<SenProvision>>();
            senProvisionsDbSet.As<IQueryable<SenProvision>>().Setup(m => m.Provider).Returns(senProvisions.Provider);
            senProvisionsDbSet.As<IQueryable<SenProvision>>().Setup(m => m.Expression).Returns(senProvisions.Expression);
            senProvisionsDbSet.As<IQueryable<SenProvision>>().Setup(m => m.ElementType).Returns(senProvisions.ElementType);
            senProvisionsDbSet.As<IQueryable<SenProvision>>().Setup(m => m.GetEnumerator()).Returns(senProvisions.GetEnumerator());
            
            var senStatusDbSet = new Mock<DbSet<SenStatus>>();
            senStatusDbSet.As<IQueryable<SenStatus>>().Setup(m => m.Provider).Returns(senStatuses.Provider);
            senStatusDbSet.As<IQueryable<SenStatus>>().Setup(m => m.Expression).Returns(senStatuses.Expression);
            senStatusDbSet.As<IQueryable<SenStatus>>().Setup(m => m.ElementType).Returns(senStatuses.ElementType);
            senStatusDbSet.As<IQueryable<SenStatus>>().Setup(m => m.GetEnumerator()).Returns(senStatuses.GetEnumerator());
            
            var staffMembersDbSet = new Mock<DbSet<StaffMember>>();
            staffMembersDbSet.As<IQueryable<StaffMember>>().Setup(m => m.Provider).Returns(staffMembers.Provider);
            staffMembersDbSet.As<IQueryable<StaffMember>>().Setup(m => m.Expression).Returns(staffMembers.Expression);
            staffMembersDbSet.As<IQueryable<StaffMember>>().Setup(m => m.ElementType).Returns(staffMembers.ElementType);
            staffMembersDbSet.As<IQueryable<StaffMember>>().Setup(m => m.GetEnumerator()).Returns(staffMembers.GetEnumerator());
            
            var studentsDbSet = new Mock<DbSet<Student>>();
            studentsDbSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(students.Provider);
            studentsDbSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(students.Expression);
            studentsDbSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(students.ElementType);
            studentsDbSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(students.GetEnumerator());

            var mockContext = new Mock<MyPortalDbContext>();
            mockContext.Setup(c => c.AssessmentGrades).Returns(assessmentGradesDbSet.Object);
            mockContext.Setup(c => c.AssessmentGradeSets).Returns(assessmentGradeSetsDbSet.Object);
            mockContext.Setup(c => c.AssessmentResults).Returns(assessmentResultsDbSet.Object);
            mockContext.Setup(c => c.AssessmentResultSets).Returns(assessmentResultSetsDbSet.Object);
            mockContext.Setup(c => c.AttendancePeriods).Returns(attendancePeriodsDbSet.Object);
            mockContext.Setup(c => c.AttendanceCodes).Returns(attendanceCodesDbSet.Object);
            mockContext.Setup(c => c.AttendanceMeanings).Returns(attendanceMeaningsDbSet.Object);
            mockContext.Setup(c => c.AttendanceMarks).Returns(attendanceMarksDbSet.Object);
            mockContext.Setup(c => c.AttendanceWeeks).Returns(attendanceWeeksDbSet.Object);
            mockContext.Setup(c => c.BehaviourAchievements).Returns(behaviourAchievementsDbSet.Object);
            mockContext.Setup(c => c.BehaviourAchievementTypes).Returns(behaviourAchievementTypesDbSet.Object);
            mockContext.Setup(c => c.BehaviourIncidents).Returns(behaviourIncidentsDbSet.Object);
            mockContext.Setup(c => c.BehaviourIncidentTypes).Returns(behaviourIncidentTypesDbSet.Object);
            mockContext.Setup(c => c.BehaviourLocations).Returns(behaviourLocationsDbSet.Object);
            mockContext.Setup(c => c.CommunicationLogs).Returns(communicationLogsDbSet.Object);

            return mockContext.Object;
        }

        public static void InitialiseMaps()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }
    }
}