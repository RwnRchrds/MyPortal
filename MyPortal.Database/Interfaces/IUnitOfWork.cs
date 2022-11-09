﻿using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Interfaces
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IAcademicTermRepository AcademicTerms { get; }
        IAcademicYearRepository AcademicYears { get; }
        IAccountTransactionRepository AccountTransactions { get; }
        IAchievementOutcomeRepository AchievementOutcomes { get; }
        IAchievementRepository Achievements { get; }
        IAchievementTypeRepository AchievementTypes { get; }
        IActivityRepository Activities { get; }
        IAddressLinkRepository AddressLinks { get; }
        IAddressRepository Addresses { get; }
        IAddressTypeRepository AddressTypes { get; }
        IAgencyRepository Agencies { get; }
        IAgentRepository Agents { get; }
        IAgentTypeRepository AgentTypes { get; }
        IAspectRepository Aspects { get; }
        IAspectTypeRepository AspectTypes { get; }
        IAttendanceCodeTypeRepository AttendanceCodeTypes { get; }
        IAttendanceCodeRepository AttendanceCodes { get; }
        IAttendanceMarkRepository AttendanceMarks { get; }
        IAttendancePeriodRepository AttendancePeriods { get; }
        IAttendanceWeekPatternRepository AttendanceWeekPatterns { get; }
        IAttendanceWeekRepository AttendanceWeeks { get; }
        IBasketItemRepository BasketItems { get; }
        IBehaviourOutcomeRepository BehaviourOutcomes { get; }
        IBehaviourRoleTypeRepository BehaviourRoleTypes { get; }
        IBehaviourStatusRepository BehaviourStatus { get; }
        IBehaviourTargetRepository BehaviourTargets { get; }
        IBillAccountTransactionRepository BillAccountTransactions { get; }
        IBillDiscountRepository BillDiscounts { get; }
        IBillStudentChargeRepository BillStudentCharges { get; }
        IBillItemRepository BillItems { get; }
        IBillRepository Bills { get; }
        IBoarderStatusRepository BoarderStatus { get; }
        IBuildingRepository Buildings { get; }
        IBuildingFloorRepository BuildingFloors { get; }
        IBulletinRepository Bulletins { get; }
        IChargeBillingPeriodRepository ChargeBillingPeriods { get; }
        IChargeDiscountRepository ChargeDiscounts { get; }
        IChargeRepository Charges { get; }
        IClassRepository Classes { get; }
        ICommentBankRepository CommentBanks { get; }
        ICommentBankAreaRepository CommentBankAreas { get; }
        ICommentBankSectionRepository CommentBankSections { get; }
        ICommentRepository Comments { get; }
        ICommunicationLogRepository CommunicationLogs { get; }
        ICommunicationTypeRepository CommunicationTypes { get; }
        IContactRepository Contacts { get; }
        ICourseRepository Courses { get; }
        ICoverArrangementRepository CoverArrangements { get; }
        ICurriculumBandBlockAssignmentRepository CurriculumBandBlockAssignments { get; }
        ICurriculumBandRepository CurriculumBands { get; }
        ICurriculumBlockRepository CurriculumBlocks { get; }
        ICurriculumGroupRepository CurriculumGroups { get; }
        ICurriculumYearGroupRepository CurriculumYearGroups { get; }
        IDetentionRepository Detentions { get; }
        IDetentionTypeRepository DetentionTypes { get; }
        IDiaryEventAttendeeRepository DiaryEventAttendees { get; }
        IDiaryEventAttendeeResponseRepository DiaryEventAttendeeResponses { get; }
        IDiaryEventRepository DiaryEvents { get; }  
        IDiaryEventTemplateRepository DiaryEventTemplates { get; }
        IDiaryEventTypeRepository DiaryEventTypes { get; }
        IDietaryRequirementRepository DietaryRequirements { get; }
        IDirectoryRepository Directories { get; }
        IDiscountRepository Discounts { get; }
        IDocumentRepository Documents { get; }
        IDocumentTypeRepository DocumentTypes { get; }
        IEmailAddressRepository EmailAddresses { get; }
        IEmailAddressTypeRepository EmailAddressTypes { get; }
        IEnrolmentStatusRepository EnrolmentStatus { get; }
        IEthnicityRepository Ethnicities { get; }
        IExamAssessmentAspectRepository ExamAssessmentAspects { get; }
        IExamAssessmentModeRepository ExamAssessmentModes { get; }
        IExamAssessmentRepository ExamAssessments { get; }
        IExamAwardElementRepository ExamAwardElements { get; }
        IExamAwardRepository ExamAwards { get; }
        IExamAwardSeriesRepository ExamAwardSeries { get; }
        IExamBaseComponentRepository ExamBaseComponents { get; }
        IExamBaseElementRepository ExamBaseElements { get; }
        IExamBoardRepository ExamBoards { get; }
        IExamCandidateRepository ExamCandidates { get; }
        IExamCandidateSeriesRepository ExamCandidateSeries { get; }
        IExamCandidateSpecialArrangementRepository ExamCandidateSpecialArrangements { get; }
        IExamComponentRepository ExamComponents { get; }
        IExamComponentSittingRepository ExamComponentSittings { get; }
        IExamDateRepository ExamDates { get; }
        IExamElementComponentRepository ExamElementComponents { get; }
        IExamElementRepository ExamElements { get; }
        IExamEnrolmentRepository ExamEnrolments { get; }
        IExamQualificationLevelRepository ExamQualificationLevels { get; }
        IExamQualificationRepository ExamQualifications { get; }
        IExamResultEmbargoRepository ExamResultEmbargoes { get; }
        IExamRoomRepository ExamRooms { get; }
        IExamRoomSeatBlockRepository ExamRoomSeatBlocks { get; }
        IExamSeasonRepository ExamSeasons { get; }
        IExamSeatAllocationRepository ExamSeatAllocations { get; }
        IExamSeriesRepository ExamSeries { get; }
        IExamSessionRepository ExamSessions { get; }
        IExamSpecialArrangementRepository ExamSpecialArrangements { get; }
        IExclusionAppealResultRepository ExclusionAppealResults { get; }
        IExclusionReasonRepository ExclusionReasons { get; }
        IExclusionRepository Exclusions { get; }
        IExclusionTypeRepository ExclusionTypes { get; }
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
        ILanguageRepository Languages { get; }
        ILessonPlanRepository LessonPlans { get; }
        ILessonPlanHomeworkItemRepository LessonPlanHomeworkItems { get; }
        ILessonPlanTemplateRepository LessonPlanTemplates { get; }
        ILocalAuthorityRepository LocalAuthorities { get; }
        ILocationRepository Locations { get; }
        ILogNoteRepository LogNotes { get; }
        ILogNoteTypeRepository LogNoteTypes { get; }
        IMarksheetColumnRepository MarksheetColumns { get; }
        IMarksheetRepository Marksheets { get; }
        IMarksheetTemplateRepository MarksheetTemplates { get; }
        IMedicalConditionRepository MedicalConditions { get; }
        IMedicalEventRepository MedicalEvents { get; }
        INextOfKinRelationshipTypeRepository NextOfKinRelationshipTypes { get; }
        INextOfKinRepository NextOfKin { get; }
        IObservationOutcomeRepository ObservationOutcomes { get; }
        IObservationRepository Observations { get; }
        IParentEveningAppointmentRepository ParentEveningAppointments { get; }
        IParentEveningBreakRepository ParentEveningBreaks { get; }
        IParentEveningGroupRepository ParentEveningGroups { get; }
        IParentEveningRepository ParentEvenings { get; }
        IParentEveningStaffMemberRepository ParentEveningStaffMembers { get; }
        IPersonConditionRepository PersonConditions { get; }
        IPersonDietaryRequirementRepository PersonDietaryRequirements { get; }
        IPersonRepository People { get; }
        IPhoneNumberRepository PhoneNumbers { get; }
        IPhoneNumberTypeRepository PhoneNumberTypes { get; }
        IPhotoRepository Photos { get; }
        IProductRepository Products { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IRegGroupRepository RegGroups { get; }
        IRelationshipTypeRepository RelationshipTypes { get; }
        IReportCardEntryRepository ReportCardEntries { get; }
        IReportCardRepository ReportCards { get; }
        IReportCardTargetEntryRepository ReportCardTargetEntries { get; }
        IReportCardTargetRepository ReportCardTargets { get; }
        IResultRepository Results { get; }
        IResultSetRepository ResultSets { get; }
        IRoleRepository Roles { get; }
        IRoomClosureReasonRepository RoomClosureReasons { get; }
        IRoomClosureRepository RoomClosures { get; }
        IRoomRepository Rooms { get; }
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
        ISenTypeRepository SenTypes { get; }
        ISessionRepository Sessions { get; }
        IStaffAbsenceRepository StaffAbsences { get; }
        IStaffAbsenceTypeRepository StaffAbsenceTypes { get; }
        IStaffIllnessTypeRepository StaffIllnessTypes { get; }
        IStaffMemberRepository StaffMembers { get; }
        IStoreDiscountRepository StoreDiscounts { get; }
        IStudentAchievementRepository StudentAchievements { get; }
        IStudentAgentRelationshipRepository StudentAgentRelationships { get; }
        IStudentChargeDiscountRepository StudentChargeDiscounts { get; }
        IStudentChargeRepository StudentCharges { get; }
        IStudentContactRelationshipRepository StudentContactRelationships { get; }
        IStudentGroupMembershipRepository StudentGroupMemberships { get; }
        IStudentGroupRepository StudentGroups { get; }
        IStudentGroupSupervisorRepository StudentGroupSupervisors { get; }
        IStudentIncidentRepository StudentIncidents { get; }
        IStudentRepository Students { get; }
        IStudyTopicRepository StudyTopics { get; }
        ISubjectCodeRepository SubjectCodes { get; }
        ISubjectCodeSetRepository SubjectCodeSets { get; }
        ISubjectRepository Subjects { get; }
        ISubjectStaffMemberRepository SubjectStaffMembers { get; }
        ISubjectStaffMemberRoleRepository SubjectStaffMemberRoles { get; }
        ISystemSettingRepository SystemSettings { get; }
        ITaskRepository Tasks { get; }
        ITaskTypeRepository TaskTypes { get; }
        ITrainingCertificateRepository TrainingCertificates { get; }
        ITrainingCertificateStatusRepository TrainingCertificateStatus { get; }
        ITrainingCourseRepository TrainingCourses { get; }
        IUserRepository Users { get; }
        IUserRoleRepository UserRoles { get; }
        IVatRateRepository VatRates { get; }
        IYearGroupRepository YearGroups { get; }

        int BatchLimit { get; set; }
        Task BatchSaveChangesAsync();
        Task SaveChangesAsync();
    }
}
