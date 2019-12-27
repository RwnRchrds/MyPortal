using System;
using System.Threading.Tasks;

namespace MyPortal.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAspectRepository Aspects { get; }
        IAspectTypeRepository AspectTypes { get; }
        IGradeRepository Grades { get; }
        IGradeSetRepository GradeSets { get; }
        IResultRepository Results { get; }
        IResultSetRepository ResultSets { get; }
        IAttendanceCodeRepository AttendanceCodes { get; }
        IAttendanceCodeMeaningRepository AttendanceCodeMeanings { get; }
        IAttendanceMarkRepository AttendanceMarks { get; }
        IPeriodRepository Periods { get; }
        IAttendanceWeekRepository AttendanceWeeks { get; }
        IAchievementRepository Achievements { get; }
        IAchievementTypeRepository AchievementTypes { get; }
        IIncidentRepository Incidents { get; }
        IIncidentTypeRepository IncidentTypes { get; }
        IAddressPersonRepository AddressPersons { get; }
        IAddressRepository Addresses { get; }
        IEmailAddressRepository EmailAddresses { get; }
        IEmailAddressTypeRepository EmailAddressTypes { get; }
        ICommunicationLogRepository CommunicationLogs { get; }
        IPhoneNumberRepository PhoneNumbers { get; }
        IPhoneNumberTypeRepository PhoneNumberTypes { get; }
        ICommunicationTypeRepository CommunicationTypes { get; }
        IContactRepository Contacts { get; }
        IRelationshipTypeRepository RelationshipTypes { get; }
        IAcademicYearRepository AcademicYears { get; }
        IClassRepository Classes { get; }
        IDetentionRepository Detentions { get; }
        IDetentionAttendanceStatusRepository DetentionAttendanceStatus { get; }
        IDetentionTypeRepository DetentionTypes { get; }
        IDiaryEventRepository DiaryEvents { get; }
        IIncidentDetentionRepository IncidentDetentions { get; }
        IEnrolmentRepository Enrolments { get; }
        ILessonPlanRepository LessonPlans { get; }
        ILessonPlanTemplateRepository LessonPlanTemplates { get; }
        ISessionRepository Sessions { get; }
        IStudyTopicRepository StudyTopics { get; }
        ISubjectRepository Subjects { get; }
        ISubjectStaffMemberRepository SubjectStaffMembers { get; }
        ISubjectStaffMemberRoleRepository SubjectStaffMemberRoles { get; }
        IDocumentRepository Documents { get; }
        IDocumentTypeRepository DocumentTypes { get; }
        IBasketItemRepository BasketItems { get; }
        IProductRepository Products { get; }
        IProductTypeRepository ProductTypes { get; }
        ISaleRepository Sales { get; }
        IConditionRepository Conditions { get; }
        IDietaryRequirementRepository DietaryRequirements { get; }
        IMedicalEventRepository MedicalEvent { get; }
        IPersonConditionRepository PersonConditions { get; }
        IPersonDietaryRequirementRepository PersonDietaryRequirements { get; }
        IHouseRepository Houses { get; }
        IRegGroupRepository RegGroups { get; }
        IYearGroupRepository YearGroups { get; }
        IPersonAttachmentRepository PersonAttachments { get; }
        IObservationRepository Observations { get; }
        IObservationOutcomeRepository ObservationOutcomes { get; }
        ITrainingCertificateRepository TrainingCertificates { get; }
        ITrainingCertificateStatusRepository TrainingCertificateStatus { get; }
        ITrainingCourseRepository TrainingCourses { get; }
        IPersonRepository People { get; }
        ICommentBankRepository CommentBanks { get; }
        ICommentRepository Comments { get; }
        IProfileLogNoteRepository ProfileLogNotes { get; }
        IProfileLogNoteTypeRepository ProfileLogNoteTypes { get; }
        IGovernanceTypeRepository GovernanceTypes { get; }
        IIntakeTypeRepository IntakeTypes { get; }
        ILocationRepository Locations { get; }
        IPhaseRepository Phases { get; }
        ISchoolTypeRepository SchoolTypes { get; }
        ISenEventRepository SenEvents { get; }
        ISenEventTypeRepository SenEventTypes { get; }
        IGiftedTalentedRepository GiftedTalented { get; }
        ISenProvisionRepository SenProvisions { get; }
        ISenProvisionTypeRepository SenProvisionTypes { get; }
        ISenReviewTypeRepository SenReviewTypes { get; }
        ISenStatusRepository SenStatus { get; }
        IStaffMemberRepository StaffMembers { get; }
        IStudentContactRepository StudentContacts { get; }
        IStudentRepository Students { get; }
        ISystemAreaRepository SystemAreas { get; }
        IBulletinRepository Bulletins { get; }
        IReportRepository Reports { get; }
        ISchoolRepository Schools { get; }

        Task<int> Complete();
    }
}
