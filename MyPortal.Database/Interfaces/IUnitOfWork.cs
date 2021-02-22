using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;

namespace MyPortal.Database.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAcademicTermRepository AcademicTerms { get; }
        IAcademicYearRepository AcademicYears { get; }
        IAccountTransactionRepository AccountTransactions { get; }
        IAchievementOutcomeRepository AchievementOutcomes { get; }
        IAchievementRepository Achievements { get; }
        IAchievementTypeRepository AchievementTypes { get; }
        IActivityEventRepository ActivityEvents { get; }
        IActivityRepository Activities { get; }
        IAddressPersonRepository AddressPersons { get; }
        IAddressRepository Addresses { get; }
        IAspectRepository Aspects { get; }
        IAspectTypeRepository AspectTypes { get; }
        IAttendanceCodeMeaningRepository AttendanceCodeMeanings { get; }
        IAttendanceCodeRepository AttendanceCodes { get; }
        IAttendanceMarkRepository AttendanceMarks { get; }
        IAttendancePeriodRepository AttendancePeriods { get; }
        IAttendanceWeekRepository AttendanceWeeks { get; }
        IBasketItemRepository BasketItems { get; }
        IBehaviourOutcomeRepository BehaviourOutcomes { get; }
        IBehaviourStatusRepository BehaviourStatus { get; }
        IBillRepository Bills { get; }
        IBulletinRepository Bulletins { get; }
        IChargeDiscountRepository ChargeDiscounts { get; }
        IChargeRepository Charges { get; }
        IClassRepository Classes { get; }
        ICommentBankRepository CommentBanks { get; }
        ICommentRepository Comments { get; }
        ICommunicationLogRepository CommunicationLogs { get; }
        ICommunicationTypeRepository CommunicationTypes { get; }
        IContactRepository Contacts { get; }
        ICurriculumBandBlockAssignmentRepository CurriculumBandBlockAssignments { get; }
        ICurriculumBandMembershipRepository CurriculumBandMemberships { get; }
        ICurriculumBandRepository CurriculumBands { get; }
        ICurriculumBlockRepository CurriculumBlocks { get; }
        ICurriculumGroupMembershipRepository CurriculumGroupMemberships { get; }
        ICurriculumGroupRepository CurriculumGroups { get; }
        ICurriculumYearGroupRepository CurriculumYearGroups { get; }
        IDetentionRepository Detentions { get; }
        IDetentionTypeRepository DetentionTypes { get; }
        IDiaryEventAttendeeRepository DiaryEventAttendees { get; }
        IDiaryEventRepository DiaryEvents { get; }  
        IDiaryEventTemplateRepository DiaryEventTemplates { get; }
        IDiaryEventTypeRepository DiaryEventTypes { get; }
        IDietaryRequirementRepository DietaryRequirements { get; }
        IDirectoryRepository Directories { get; }
        IDocumentRepository Documents { get; }
        IDocumentTypeRepository DocumentTypes { get; }
        IEmailAddressRepository EmailAddresses { get; }
        IEmailAddressTypeRepository EmailAddressTypes { get; }
        IExclusionRepository Exclusions { get; }
        IFileRepository Files { get; }
        IGiftedTalentedRepository GiftedTalented { get; }
        IGovernanceTypeRepository GovernanceTypes { get; }
        IGradeRepository Grades { get; }
        IGradeSetRepository GradeSets { get; }
        IHomeworkItemRepository HomeworkItems { get; }
        IHomeworkSubmissionRepository HomeworkSubmissions { get; }
        IHouseRepository Houses { get; }
        IIncidentDetentionRepository IncidentDetentions { get; }
        IIncidentRepository Incidents { get; }
        IIncidentTypeRepository IncidentTypes { get; }
        IIntakeTypeRepository IntakeTypes { get; }
        ILessonPlanRepository LessonPlans { get; }
        ILessonPlanTemplateRepository LessonPlanTemplates { get; }
        ILocalAuthorityRepository LocalAuthorities { get; }
        ILocationRepository Locations { get; }
        ILogNoteRepository LogNotes { get; }
        ILogNoteTypeRepository LogNoteTypes { get; }
        IMedicalConditionRepository MedicalConditions { get; }
        IMedicalEventRepository MedicalEvents { get; }
        IObservationOutcomeRepository ObservationOutcomes { get; }
        IObservationRepository Observations { get; }
        IPermissionRepository Permissions { get; }
        IPersonConditionRepository PersonConditions { get; }
        IPersonDietaryRequirementRepository PersonDietaryRequirements { get; }
        IPersonRepository People { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IRegGroupRepository RegGroups { get; }
        IRolePermissionRepository RolePermissions { get; }
        ISchoolPhaseRepository SchoolPhases { get; }
        ISchoolRepository Schools { get; }
        ISchoolTypeRepository SchoolTypes { get; }
        ISenEventRepository SenEvents { get; }
        ISenEventTypeRepository SenEventTypes { get; }
        ISenProvisionRepository SenProvisions { get; }
        ISenProvisionTypeRepository SenProvisionTypes { get; }
        ISenReviewRepository SenReviews { get; }
        ISenReviewTypeRepository SenReviewTypes { get; }
        ISenStatusRepository SenStatus { get; }
        ISessionRepository Sessions { get; }
        IStaffMemberRepository StaffMembers { get; }
        IStudentChargeRepository StudentCharges { get; }
        IStudentContactRelationshipRepository StudentContactRelationships { get; }
        IStudentDiscountRepository StudentDiscounts { get; }
        IStudentRepository Students { get; }
        IStudyTopicRepository StudyTopics { get; }
        ISubjectCodeSetRepository SubjectCodeSets { get; }
        ISubjectRepository Subjects { get; }
        ISubjectStaffMemberRepository SubjectStaffMembers { get; }
        ISubjectStaffMemberRoleRepository SubjectStaffMemberRoles { get; }
        ISystemAreaRepository SystemAreas { get; }
        ISystemSettingRepository SystemSettings { get; }
        ITaskRepository Tasks { get; }
        ITaskTypeRepository TaskTypes { get; }
        ITrainingCertificateRepository TrainingCertificates { get; }
        ITrainingCertificateStatusRepository TrainingCertificateStatus { get; }
        ITrainingCourseRepository TrainingCourses { get; }
        IUserRoleRepository UserRoles { get; }
        IUserRepository Users { get; }
        IYearGroupRepository YearGroups { get; }

        Task SaveChanges();
    }
}
