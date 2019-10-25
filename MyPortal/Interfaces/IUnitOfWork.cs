using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAssessmentAspectRepository AssessmentAspects { get; }
        IAssessmentAspectTypeRepository AssessmentAspectTypes { get; }
        IAssessmentGradeRepository AssessmentGrades { get; }
        IAssessmentGradeSetRepository AssessmentGradeSets { get; }
        IAssessmentResultRepository AssessmentResults { get; }
        IAssessmentResultSetRepository AssessmentResultSets { get; }
        IAttendanceCodeRepository AttendanceCodes { get; }
        IAttendanceMarkRepository AttendanceMarks { get; }
        IAttendanceMeaningRepository AttendanceMeanings { get; }
        IAttendancePeriodRepository AttendancePeriods { get; }
        IAttendanceWeekRepository AttendanceWeeks { get; }
        IBehaviourAchievementRepository BehaviourAchievements { get; }
        IBehaviourAchievementTypeRepository BehaviourAchievementTypes { get; }
        IBehaviourIncidentRepository BehaviourIncidents { get; }
        IBehaviourIncidentTypeRepository BehaviourIncidentTypes { get; }
        ICommunicationAddressPersonRepository CommunicationAddressPersons { get; }
        ICommunicationAddressRepository CommunicationAddresses { get; }
        ICommunicationEmailAddressRepository CommunicationEmailAddresses { get; }
        ICommunicationLogRepository CommunicationLogs { get; }
        ICommunicationPhoneNumberRepository CommunicationPhoneNumbers { get; }
        ICommunicationPhoneNumberTypeRepository CommunicationPhoneNumberTypes { get; }
        ICommunicationTypeRepository CommunicationTypes { get; }
        IContactRepository Contacts { get; }
        IRelationshipTypeRepository RelationshipTypes { get; }
        ICurriculumAcademicYearRepository CurriculumAcademicYears { get; }
        ICurriculumClassRepository CurriculumClasses { get; }
        ICurriculumEnrolmentRepository CurriculumEnrolments { get; }
        ICurriculumLessonPlanRepository CurriculumLessonPlans { get; }
        ICurriculumLessonPlanTemplateRepository CurriculumLessonPlanTemplates { get; }
        ICurriculumSessionRepository CurriculumSessions { get; }
        ICurriculumStudyTopicRepository CurriculumStudyTopics { get; }
        ICurriculumSubjectRepository CurriculumSubjects { get; }
        IDocumentRepository Documents { get; }
        IDocumentTypeRepository DocumentTypes { get; }
        IFinanceBasketItemRepository FinanceBasketItems { get; }
        IFinanceProductRepository FinanceProducts { get; }
        IFinanceProductTypeRepository FinanceProductTypes { get; }
        IFinanceSaleRepository FinanceSales { get; }
        IMedicalConditionRepository MedicalConditions { get; }
        IMedicalDietaryRequirementRepository MedicalDietaryRequirements { get; }
        IMedicalEventRepository MedicalEvent { get; }
        IMedicalPersonConditionRepository MedicalPersonConditions { get; }
        IMedicalPersonDietaryRequirementRepository MedicalPersonDietaryRequirements { get; }
        IPastoralHouseRepository PastoralHouses { get; }
        IPastoralRegGroupRepository PastoralRegGroups { get; }
        IPastoralYearGroupRepository PastoralYearGroups { get; }
        IPersonDocumentRepository PersonDocuments { get; }
        IPersonnelObservationRepository PersonnelObservations { get; }
        IPersonnelTrainingCertificateRepository PersonnelTrainingCertificates { get; }
        IPersonnelTrainingCourseRepository PersonnelTrainingCourses { get; }
        IPersonRepository People { get; }
        IProfileCommentBankRepository ProfileCommentBanks { get; }
        IProfileCommentRepository ProfileComments { get; }
        IProfileLogRepository ProfileLogs { get; }
        IProfileLogTypeRepository ProfileLogTypes { get; }
        ISchoolGovernanceTypeRepository SchoolGovernanceTypes { get; }
        ISchoolIntakeTypeRepository SchoolIntakeTypes { get; }
        ISchoolLocationRepository SchoolLocations { get; }
        ISchoolPhaseRepository SchoolPhases { get; }
        ISchoolTypeRepository SchoolTypes { get; }
        ISenEventRepository SenEvents { get; }
        ISenEventTypeRepository SenEventTypes { get; }
        ISenGiftedTalentedRepository SenGiftedTalented { get; }
        ISenProvisionRepository SenProvisions { get; }
        ISenProvisionTypeRepository SenProvisionTypes { get; }
        ISenReviewTypeRepository SenReviewTypes { get; }
        ISenStatusRepository SenStatus { get; }
        IStaffMemberRepository StaffMembers { get; }
        IStudentRepository Students { get; }
        ISystemAreaRepository SystemAreas { get; }
        ISystemBulletinRepository SystemBulletins { get; }
        ISystemReportRepository SystemReports { get; }
        ISystemSchoolRepository SystemSchools { get; }

        Task<int> Complete();
    }
}
