using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private int _batchSize;
        private int _batchLimit = 1000;
        private readonly string _connectionString;

        private DbTransaction _transaction;
        private IAcademicTermRepository _academicTerms;
        private IAcademicYearRepository _academicYears;
        private IAccountTransactionRepository _accountTransactions;
        private IAchievementOutcomeRepository _achievementOutcomes;
        private IAchievementRepository _achievements;
        private IAchievementTypeRepository _achievementTypes;
        private IActivityRepository _activities;
        private IAddressAgencyRepository _addressAgencies;
        private IAddressPersonRepository _addressPeople;
        private IAddressRepository _addresses;
        private IAddressTypeRepository _addressTypes;
        private IAgencyRepository _agencies;
        private IAgentRepository _agents;
        private IAgentTypeRepository _agentTypes;
        private IAspectRepository _aspects;
        private IAspectTypeRepository _aspectTypes;
        private IAttendanceCodeTypeRepository _attendanceCodeTypes;
        private IAttendanceCodeRepository _attendanceCodes;
        private IAttendanceMarkRepository _attendanceMarks;
        private IAttendancePeriodRepository _attendancePeriods;
        private IAttendanceWeekPatternRepository _attendanceWeekPatterns;
        private IAttendanceWeekRepository _attendanceWeeks;
        private IBasketItemRepository _basketItems;
        private IBehaviourOutcomeRepository _behaviourOutcomes;
        private IBehaviourRoleTypeRepository _behaviourRoleTypes;
        private IBehaviourStatusRepository _behaviourStatus;
        private IBehaviourTargetRepository _behaviourTargets;
        private IBillAccountTransactionRepository _billAccountTransactions;
        private IBillDiscountRepository _billDiscounts;
        private IBillStudentChargeRepository _billStudentCharges;
        private IBillItemRepository _billItems;
        private IBillRepository _bills;
        private IBoarderStatusRepository _boarderStatus;
        private IBuildingRepository _buildings;
        private IBuildingFloorRepository _buildingFloors;
        private IBulletinRepository _bulletins;
        private IChargeBillingPeriodRepository _chargeBillingPeriods;
        private IChargeDiscountRepository _chargeDiscounts;
        private IChargeRepository _charges;
        private IClassRepository _classes;
        private ICommentBankRepository _commentBanks;
        private ICommentBankAreaRepository _commentBankAreas;
        private ICommentBankSectionRepository _commentBankSections;
        private ICommentRepository _comments;
        private ICommunicationLogRepository _communicationLogs;
        private ICommunicationTypeRepository _communicationTypes;
        private IContactRepository _contacts;
        private ICourseRepository _courses;
        private ICoverArrangementRepository _coverArrangements;
        private ICurriculumBandBlockAssignmentRepository _curriculumBandBlockAssignments;
        private ICurriculumBandRepository _curriculumBands;
        private ICurriculumBlockRepository _curriculumBlocks;
        private ICurriculumGroupRepository _curriculumGroups;
        private ICurriculumYearGroupRepository _curriculumYearGroups;
        private IDetentionRepository _detentions;
        private IDetentionTypeRepository _detentionTypes;
        private IDiaryEventAttendeeRepository _diaryEventAttendees;
        private IDiaryEventAttendeeResponseRepository _diaryEventAttendeeResponses;
        private IDiaryEventRepository _diaryEvents;
        private IDiaryEventTemplateRepository _diaryEventTemplates;
        private IDiaryEventTypeRepository _diaryEventTypes;
        private IDietaryRequirementRepository _dietaryRequirements;
        private IDirectoryRepository _directories;
        private IDiscountRepository _discounts;
        private IDocumentRepository _documents;
        private IDocumentTypeRepository _documentTypes;
        private IEmailAddressRepository _emailAddresses;
        private IEmailAddressTypeRepository _emailAddressTypes;
        private IEnrolmentStatusRepository _enrolmentStatus;
        private IEthnicityRepository _ethnicities;
        private IExamAssessmentAspectRepository _examAssessmentAspects;
        private IExamAssessmentModeRepository _examAssessmentModes;
        private IExamAssessmentRepository _examAssessments;
        private IExamAwardElementRepository _examAwardElements;
        private IExamAwardRepository _examAwards;
        private IExamAwardSeriesRepository _examAwardSeries;
        private IExamBaseComponentRepository _examBaseComponents;
        private IExamBaseElementRepository _examBaseElements;
        private IExamBoardRepository _examBoards;
        private IExamCandidateRepository _examCandidates;
        private IExamCandidateSeriesRepository _examCandidateSeries;
        private IExamCandidateSpecialArrangementRepository _examCandidateSpecialArrangements;
        private IExamComponentRepository _examComponents;
        private IExamComponentSittingRepository _examComponentSittings;
        private IExamDateRepository _examDates;
        private IExamElementComponentRepository _examElementComponents;
        private IExamElementRepository _examElements;
        private IExamEnrolmentRepository _examEnrolments;
        private IExamQualificationLevelRepository _examQualificationLevels;
        private IExamQualificationRepository _examQualifications;
        private IExamResultEmbargoRepository _examResultEmbargoes;
        private IExamRoomRepository _examRooms;
        private IExamRoomSeatBlockRepository _examRoomSeatBlocks;
        private IExamSeasonRepository _examSeasons;
        private IExamSeatAllocationRepository _examSeatAllocations;
        private IExamSeriesRepository _examSeries;
        private IExamSessionRepository _examSessions;
        private IExamSpecialArrangementRepository _examSpecialArrangements;
        private IExclusionAppealResultRepository _exclusionAppealResults;
        private IExclusionReasonRepository _exclusionReasons;
        private IExclusionRepository _exclusions;
        private IExclusionTypeRepository _exclusionTypes;
        private IFileRepository _files;
        private IGiftedTalentedRepository _giftedTalented;
        private IGovernanceTypeRepository _governanceTypes;
        private IGradeRepository _grades;
        private IGradeSetRepository _gradeSets;
        private IHomeworkItemRepository _homeworkItems;
        private IHomeworkSubmissionRepository _homeworkSubmissions;
        private IHouseRepository _houses;
        private IIncidentDetentionRepository _incidentDetentions;
        private IIncidentRepository _incidents;
        private IIncidentTypeRepository _incidentTypes;
        private IIntakeTypeRepository _intakeTypes;
        private ILanguageRepository _languages;
        private ILessonPlanRepository _lessonPlans;
        private ILessonPlanHomeworkItemRepository _lessonPlanHomeworkItems;
        private ILessonPlanTemplateRepository _lessonPlanTemplates;
        private ILocalAuthorityRepository _localAuthorities;
        private ILocationRepository _locations;
        private ILogNoteRepository _logNotes;
        private ILogNoteTypeRepository _logNoteTypes;
        private IMarksheetColumnRepository _marksheetColumns;
        private IMarksheetRepository _marksheets;
        private IMarksheetTemplateRepository _marksheetTemplates;
        private IMedicalConditionRepository _medicalConditions;
        private IMedicalEventRepository _medicalEvents;
        private INextOfKinRelationshipTypeRepository _nextOfKinRelationshipTypes;
        private INextOfKinRepository _nextOfKin;
        private IObservationOutcomeRepository _observationOutcomes;
        private IObservationRepository _observations;
        private IParentEveningAppointmentRepository _parentEveningAppointments;
        private IParentEveningBreakRepository _parentEveningBreaks;
        private IParentEveningGroupRepository _parentEveningGroups;
        private IParentEveningRepository _parentEvenings;
        private IParentEveningStaffMemberRepository _parentEveningStaffMembers;
        private IPersonConditionRepository _personConditions;
        private IPersonDietaryRequirementRepository _personDietaryRequirements;
        private IPersonRepository _people;
        private IPhoneNumberRepository _phoneNumbers;
        private IPhoneNumberTypeRepository _phoneNumberTypes;
        private IPhotoRepository _photos;
        private IProductRepository _products;
        private IRegGroupRepository _regGroups;
        private IRelationshipTypeRepository _relationshipTypes;
        private IReportCardEntryRepository _reportCardEntries;
        private IReportCardRepository _reportCards;
        private IReportCardTargetEntryRepository _reportCardTargetEntries;
        private IReportCardTargetRepository _reportCardTargets;
        private IResultRepository _results;
        private IResultSetRepository _resultSets;
        private IRoomClosureReasonRepository _roomClosureReasons;
        private IRoomClosureRepository _roomClosures;
        private IRoleRepository _roles;
        private IRoomRepository _rooms;
        private ISchoolPhaseRepository _schoolPhases;
        private ISchoolRepository _schools;
        private ISchoolTypeRepository _schoolTypes;
        private ISenEventRepository _senEvents;
        private ISenEventTypeRepository _senEventTypes;
        private ISenProvisionRepository _senProvisions;
        private ISenProvisionTypeRepository _senProvisionTypes;
        private ISenReviewRepository _senReviews;
        private ISenReviewTypeRepository _senReviewTypes;
        private ISenStatusRepository _senStatus;
        private ISenTypeRepository _senTypes;
        private ISessionRepository _sessions;
        private IStaffAbsenceRepository _staffAbsences;
        private IStaffAbsenceTypeRepository _staffAbsenceTypes;
        private IStaffIllnessTypeRepository _staffIllnessTypes;
        private IStaffMemberRepository _staffMembers;
        private IStoreDiscountRepository _storeDiscounts;
        private IStudentAchievementRepository _studentAchievements;
        private IStudentChargeDiscountRepository _studentChargeDiscounts;
        private IStudentChargeRepository _studentCharges;
        private IStudentContactRelationshipRepository _studentContactRelationships;
        private IStudentGroupRepository _studentGroups;
        private IStudentGroupMembershipRepository _studentGroupMemberships;
        private IStudentGroupSupervisorRepository _studentGroupSupervisors;
        private IStudentIncidentRepository _studentIncidents;
        private IStudentRepository _students;
        private IStudentAgentRelationshipRepository _studentAgentRelationships;
        private IStudyTopicRepository _studyTopics;
        private ISubjectCodeRepository _subjectCodes;
        private ISubjectCodeSetRepository _subjectCodeSets;
        private ISubjectRepository _subjects;
        private ISubjectStaffMemberRepository _subjectStaffMembers;
        private ISubjectStaffMemberRoleRepository _subjectStaffMemberRoles;
        private ISystemSettingRepository _systemSettings;
        private ITaskRepository _tasks;
        private ITaskReminderRepository _taskReminders;
        private ITaskTypeRepository _taskTypes;
        private ITrainingCertificateRepository _trainingCertificates;
        private ITrainingCertificateStatusRepository _trainingCertificateStatus;
        private ITrainingCourseRepository _trainingCourses;
        private IUserRepository _users;
        private IUserRoleRepository _userRoles;
        private IVatRateRepository _vatRates;
        private IYearGroupRepository _yearGroups;

        public IAcademicTermRepository AcademicTerms =>
            _academicTerms ??= new AcademicTermRepository(_context, _transaction);

        public IAcademicYearRepository AcademicYears =>
            _academicYears ??= new AcademicYearRepository(_context, _transaction);

        public IAccountTransactionRepository AccountTransactions =>
            _accountTransactions ??= new AccountTransactionRepository(_context, _transaction);

        public IAchievementOutcomeRepository AchievementOutcomes =>
            _achievementOutcomes ??= new AchievementOutcomeRepository(_context, _transaction);

        public IAchievementRepository Achievements =>
            _achievements ??= new AchievementRepository(_context, _transaction);

        public IAchievementTypeRepository AchievementTypes =>
            _achievementTypes ??= new AchievementTypeRepository(_context, _transaction);

        public IActivityRepository Activities =>
            _activities ??= new ActivityRepository(_context, _transaction);

        public IAddressAgencyRepository AddressAgencies =>
            _addressAgencies ??= new AddressAgencyRepository(_context, _transaction);

        public IAddressPersonRepository AddressPeople =>
            _addressPeople ??= new AddressPersonRepository(_context, _transaction);

        public IAddressRepository Addresses =>
            _addresses ??= new AddressRepository(_context, _transaction);

        public IAddressTypeRepository AddressTypes =>
            _addressTypes ??= new AddressTypeRepository(_transaction);

        public IAgencyRepository Agencies => _agencies ??= new AgencyRepository(_context, _transaction);

        public IAgentRepository Agents => _agents ??= new AgentRepository(_context, _transaction);

        public IAgentTypeRepository AgentTypes => _agentTypes ??= new AgentTypeRepository(_transaction);

        public IAspectRepository Aspects =>
            _aspects ??= new AspectRepository(_context, _transaction);

        public IAspectTypeRepository AspectTypes =>
            _aspectTypes ??= new AspectTypeRepository(_transaction);

        public IAttendanceCodeTypeRepository AttendanceCodeTypes =>
            _attendanceCodeTypes ??= new AttendanceCodeTypeRepository(_transaction);

        public IAttendanceCodeRepository AttendanceCodes =>
            _attendanceCodes = new AttendanceCodeRepository(_context, _transaction);

        public IAttendanceMarkRepository AttendanceMarks =>
            _attendanceMarks ??= new AttendanceMarkRepository(_context, _transaction);

        public IAttendancePeriodRepository AttendancePeriods =>
            _attendancePeriods ??= new AttendancePeriodRepository(_context, _transaction);

        public IAttendanceWeekRepository AttendanceWeeks =>
            _attendanceWeeks ??= new AttendanceWeekRepository(_context, _transaction);

        public IAttendanceWeekPatternRepository AttendanceWeekPatterns => _attendanceWeekPatterns ??=
            new AttendanceWeekPatternRepository(_context, _transaction);

        public IBasketItemRepository BasketItems =>
            _basketItems ??= new BasketItemRepository(_context, _transaction);

        public IBehaviourOutcomeRepository BehaviourOutcomes =>
            _behaviourOutcomes ??= new BehaviourOutcomeRepository(_context, _transaction);

        public IBehaviourRoleTypeRepository BehaviourRoleTypes =>
            _behaviourRoleTypes ??= new BehaviourRoleTypeRepository(_context, _transaction);

        public IBehaviourStatusRepository BehaviourStatus =>
            _behaviourStatus ??= new BehaviourStatusRepository(_transaction);

        public IBehaviourTargetRepository BehaviourTargets =>
            _behaviourTargets ??= new BehaviourTargetRepository(_context, _transaction);

        public IBillItemRepository BillItems => _billItems ??= new BillItemRepository(_context, _transaction);

        public IBillRepository Bills => _bills ??= new BillRepository(_context, _transaction);

        public IBillAccountTransactionRepository BillAccountTransactions => _billAccountTransactions ??=
            new BillAccountTransactionRepository(_context, _transaction);

        public IBillDiscountRepository BillDiscounts =>
            _billDiscounts ??= new BillDiscountRepository(_context, _transaction);

        public IBillStudentChargeRepository BillStudentCharges => _billStudentCharges ??= new BillStudentStudentChargeRepository(_context, _transaction);

        public IBoarderStatusRepository BoarderStatus => _boarderStatus ??= new BoarderStatusRepository(_transaction);

        public IBuildingRepository Buildings => _buildings ??= new BuildingRepository(_context, _transaction);

        public IBuildingFloorRepository BuildingFloors =>
            _buildingFloors ??= new BuildingFloorRepository(_context, _transaction);

        public IBulletinRepository Bulletins => _bulletins ??= new BulletinRepository(_context, _transaction);

        public IChargeBillingPeriodRepository ChargeBillingPeriods =>
            _chargeBillingPeriods ??= new ChargeBillingPeriodRepository(_context, _transaction);

        public IChargeDiscountRepository ChargeDiscounts =>
            _chargeDiscounts ??= new ChargeDiscountRepository(_context, _transaction);

        public IChargeRepository Charges => _charges ??= new ChargeRepository(_context, _transaction);

        public IClassRepository Classes => _classes ??= new ClassRepository(_context, _transaction);

        public ICommentBankRepository CommentBanks =>
            _commentBanks ??= new CommentBankRepository(_context, _transaction);

        public ICommentBankAreaRepository CommentBankAreas =>
            _commentBankAreas ??= new CommentBankAreaRepository(_context, _transaction);

        public ICommentBankSectionRepository CommentBankSections =>
            _commentBankSections ??= new CommentBankSectionRepository(_context, _transaction);

        public ICommentRepository Comments => _comments ??= new CommentRepository(_context, _transaction);

        public ICommunicationLogRepository CommunicationLogs =>
            _communicationLogs ??= new CommunicationLogRepository(_context, _transaction);

        public ICommunicationTypeRepository CommunicationTypes =>
            _communicationTypes ??= new CommunicationTypeRepository(_transaction);

        public IContactRepository Contacts => _contacts ??= new ContactRepository(_context, _transaction);

        public ICourseRepository Courses => _courses ??= new CourseRepository(_context, _transaction);

        public ICoverArrangementRepository CoverArrangements =>
            _coverArrangements ??= new CoverArrangementRepository(_context, _transaction);

        public ICurriculumBandBlockAssignmentRepository CurriculumBandBlockAssignments =>
            _curriculumBandBlockAssignments ??= new CurriculumBandBlockAssignmentRepository(_context, _transaction);

        public ICurriculumBandRepository CurriculumBands =>
            _curriculumBands ??= new CurriculumBandRepository(_context, _transaction);

        public ICurriculumBlockRepository CurriculumBlocks =>
            _curriculumBlocks ??= new CurriculumBlockRepository(_context, _transaction);

        public ICurriculumGroupRepository CurriculumGroups =>
            _curriculumGroups ??= new CurriculumGroupRepository(_context, _transaction);

        public ICurriculumYearGroupRepository CurriculumYearGroups =>
            _curriculumYearGroups ??= new CurriculumYearGroupRepository(_transaction);

        public IDetentionRepository Detentions => _detentions ??= new DetentionRepository(_context, _transaction);

        public IDetentionTypeRepository DetentionTypes =>
            _detentionTypes ??= new DetentionTypeRepository(_context, _transaction);

        public IDiaryEventAttendeeRepository DiaryEventAttendees =>
            _diaryEventAttendees ??= new DiaryEventAttendeeRepository(_context, _transaction);

        public IDiaryEventAttendeeResponseRepository DiaryEventAttendeeResponses => _diaryEventAttendeeResponses ??=
            new DiaryEventAttendeeResponseRepository(_transaction);

        public IDiaryEventRepository DiaryEvents => _diaryEvents ??= new DiaryEventRepository(_context, _transaction);

        public IDiaryEventTemplateRepository DiaryEventTemplates =>
            _diaryEventTemplates ??= new DiaryEventTemplateRepository(_context, _transaction);

        public IDiaryEventTypeRepository DiaryEventTypes =>
            _diaryEventTypes ??= new DiaryEventTypeRepository(_context, _transaction);

        public IDietaryRequirementRepository DietaryRequirements =>
            _dietaryRequirements ??= new DietaryRequirementRepository(_context, _transaction);

        public IDirectoryRepository Directories => _directories ??= new DirectoryRepository(_context, _transaction);

        public IDiscountRepository Discounts => _discounts ??= new DiscountRepository(_context, _transaction);

        public IDocumentRepository Documents => _documents ??= new DocumentRepository(_context, _transaction);

        public IDocumentTypeRepository DocumentTypes =>
            _documentTypes ??= new DocumentTypeRepository(_context, _transaction);

        public IEmailAddressRepository EmailAddresses =>
            _emailAddresses ??= new EmailAddressRepository(_context, _transaction);

        public IEmailAddressTypeRepository EmailAddressTypes =>
            _emailAddressTypes ??= new EmailAddressTypeRepository(_transaction);

        public IEnrolmentStatusRepository EnrolmentStatus =>
            _enrolmentStatus ??= new EnrolmentStatusRepository(_transaction);

        public IEthnicityRepository Ethnicities => _ethnicities ??= new EthnicityRepository(_transaction);

        public IExamAssessmentAspectRepository ExamAssessmentAspects => _examAssessmentAspects ??=
            new ExamAssessmentAspectRepository(_context, _transaction);

        public IExamAssessmentRepository ExamAssessments =>
            _examAssessments ??= new ExamAssessmentRepository(_context, _transaction);

        public IExamAssessmentModeRepository ExamAssessmentModes =>
            _examAssessmentModes ??= new ExamAssessmentModeRepository(_transaction);

        public IExamAwardElementRepository ExamAwardElements =>
            _examAwardElements ??= new ExamAwardElementRepository(_context, _transaction);

        public IExamAwardRepository ExamAwards => _examAwards ??= new ExamAwardRepository(_context, _transaction);

        public IExamAwardSeriesRepository ExamAwardSeries =>
            _examAwardSeries ??= new ExamAwardSeriesRepository(_context, _transaction);

        public IExamBaseComponentRepository ExamBaseComponents =>
            _examBaseComponents ??= new ExamBaseComponentRepository(_context, _transaction);

        public IExamBaseElementRepository ExamBaseElements =>
            _examBaseElements ??= new ExamBaseElementRepository(_context, _transaction);

        public IExamBoardRepository ExamBoards => _examBoards ??= new ExamBoardRepository(_context, _transaction);

        public IExamCandidateRepository ExamCandidates =>
            _examCandidates ??= new ExamCandidateRepository(_context, _transaction);

        public IExamCandidateSeriesRepository ExamCandidateSeries =>
            _examCandidateSeries ??= new ExamCandidateSeriesRepository(_context, _transaction);

        public IExamCandidateSpecialArrangementRepository ExamCandidateSpecialArrangements =>
            _examCandidateSpecialArrangements ??= new ExamCandidateSpecialArrangementRepository(_context, _transaction);

        public IExamComponentRepository ExamComponents =>
            _examComponents ??= new ExamComponentRepository(_context, _transaction);

        public IExamComponentSittingRepository ExamComponentSittings => _examComponentSittings ??=
            new ExamComponentSittingRepository(_context, _transaction);

        public IExamDateRepository ExamDates => _examDates ??= new ExamDateRepository(_context, _transaction);

        public IExamElementComponentRepository ExamElementComponents => _examElementComponents ??=
            new ExamElementComponentRepository(_context, _transaction);

        public IExamElementRepository ExamElements =>
            _examElements ??= new ExamElementRepository(_context, _transaction);

        public IExamEnrolmentRepository ExamEnrolments =>
            _examEnrolments ??= new ExamEnrolmentRepository(_context, _transaction);

        public IExamQualificationLevelRepository ExamQualificationLevels => _examQualificationLevels ??=
            new ExamQualificationLevelRepository(_context, _transaction);

        public IExamQualificationRepository ExamQualifications =>
            _examQualifications ??= new ExamQualificationRepository(_context, _transaction);

        public IExamResultEmbargoRepository ExamResultEmbargoes =>
            _examResultEmbargoes ??= new ExamResultEmbargoRepository(_context, _transaction);

        public IExamRoomRepository ExamRooms => _examRooms ??= new ExamRoomRepository(_context, _transaction);

        public IExamRoomSeatBlockRepository ExamRoomSeatBlocks =>
            _examRoomSeatBlocks ??= new ExamRoomSeatBlockRepository(_context, _transaction);

        public IExamSeasonRepository ExamSeasons => _examSeasons ??= new ExamSeasonRepository(_context, _transaction);

        public IExamSeatAllocationRepository ExamSeatAllocations =>
            _examSeatAllocations ??= new ExamSeatAllocationRepository(_context, _transaction);

        public IExamSeriesRepository ExamSeries => _examSeries ??= new ExamSeriesRepository(_context, _transaction);

        public IExamSessionRepository ExamSessions =>
            _examSessions ??= new ExamSessionRepository(_context, _transaction);

        public IExamSpecialArrangementRepository ExamSpecialArrangements => _examSpecialArrangements ??=
            new ExamSpecialArrangementRepository(_context, _transaction);

        public IExclusionRepository Exclusions => _exclusions ??= new ExclusionRepository(_context, _transaction);

        public IExclusionAppealResultRepository ExclusionAppealResults =>
            _exclusionAppealResults ??= new ExclusionAppealResultRepository(_transaction);

        public IExclusionReasonRepository ExclusionReasons =>
            _exclusionReasons ??= new ExclusionReasonRepository(_context, _transaction);

        public IExclusionTypeRepository ExclusionTypes => _exclusionTypes ??= new ExclusionTypeRepository(_transaction);

        public IFileRepository Files => _files ??= new FileRepository(_context, _transaction);

        public IGiftedTalentedRepository GiftedTalented =>
            _giftedTalented ??= new GiftedTalentedRepository(_context, _transaction);

        public IGovernanceTypeRepository GovernanceTypes =>
            _governanceTypes ??= new GovernanceTypeRepository(_transaction);

        public IGradeRepository Grades => _grades ??= new GradeRepository(_context, _transaction);

        public IGradeSetRepository GradeSets => _gradeSets ??= new GradeSetRepository(_context, _transaction);

        public IHomeworkItemRepository HomeworkItems =>
            _homeworkItems ??= new HomeworkItemRepository(_context, _transaction);

        public IHomeworkSubmissionRepository HomeworkSubmissions =>
            _homeworkSubmissions ??= new HomeworkSubmissionRepository(_context, _transaction);

        public IHouseRepository Houses => _houses ??= new HouseRepository(_context, _transaction);

        public IIncidentDetentionRepository IncidentDetentions =>
            _incidentDetentions ??= new IncidentDetentionRepository(_context, _transaction);

        public IIncidentRepository Incidents => _incidents ??= new IncidentRepository(_context, _transaction);

        public IIncidentTypeRepository IncidentTypes =>
            _incidentTypes ??= new IncidentTypeRepository(_context, _transaction);

        public IIntakeTypeRepository IntakeTypes => _intakeTypes ??= new IntakeTypeRepository(_transaction);

        public ILanguageRepository Languages => _languages ??= new LanguageRepository(_transaction);

        public ILessonPlanRepository LessonPlans => _lessonPlans ??= new LessonPlanRepository(_context, _transaction);

        public ILessonPlanHomeworkItemRepository LessonPlanHomeworkItems => _lessonPlanHomeworkItems ??=
            new LessonPlanHomeworkItemRepository(_context, _transaction);

        public ILessonPlanTemplateRepository LessonPlanTemplates =>
            _lessonPlanTemplates ??= new LessonPlanTemplateRepository(_context, _transaction);

        public ILocalAuthorityRepository LocalAuthorities =>
            _localAuthorities ??= new LocalAuthorityRepository(_transaction);

        public ILocationRepository Locations => _locations ??= new LocationRepository(_context, _transaction);

        public ILogNoteRepository LogNotes => _logNotes ??= new LogNoteRepository(_context, _transaction);

        public ILogNoteTypeRepository LogNoteTypes => _logNoteTypes ??= new LogNoteTypeRepository(_context, _transaction);

        public IMarksheetColumnRepository MarksheetColumns =>
            _marksheetColumns ??= new MarksheetColumnRepository(_context, _transaction);

        public IMarksheetRepository Marksheets => _marksheets ??=
            new MarksheetRepository(_context, _transaction);

        public IMarksheetTemplateRepository MarksheetTemplates =>
            _marksheetTemplates ??= new MarksheetTemplateRepository(_context, _transaction);

        public IMedicalConditionRepository MedicalConditions =>
            _medicalConditions ??= new MedicalConditionRepository(_context, _transaction);

        public IMedicalEventRepository MedicalEvents =>
            _medicalEvents ??= new MedicalEventRepository(_context, _transaction);

        public INextOfKinRepository NextOfKin => _nextOfKin ??= new NextOfKinRepository(_context, _transaction);

        public INextOfKinRelationshipTypeRepository NextOfKinRelationshipTypes => _nextOfKinRelationshipTypes ??=
            new NextOfKinRelationshipTypeRepository(_transaction);

        public IObservationOutcomeRepository ObservationOutcomes =>
            _observationOutcomes ??= new ObservationOutcomeRepository(_transaction);

        public IObservationRepository Observations =>
            _observations ??= new ObservationRepository(_context, _transaction);

        public IParentEveningAppointmentRepository ParentEveningAppointments => _parentEveningAppointments ??=
            new ParentEveningAppointmentRepository(_context, _transaction);

        public IParentEveningBreakRepository ParentEveningBreaks =>
            _parentEveningBreaks ??= new ParentEveningBreakRepository(_context, _transaction);

        public IParentEveningGroupRepository ParentEveningGroups =>
            _parentEveningGroups ??= new ParentEveningGroupRepository(_context, _transaction);

        public IParentEveningRepository ParentEvenings =>
            _parentEvenings ??= new ParentEveningRepository(_context, _transaction);

        public IParentEveningStaffMemberRepository ParentEveningStaffMembers => _parentEveningStaffMembers ??=
            new ParentEveningStaffMemberRepository(_context, _transaction);

        public IPersonConditionRepository PersonConditions =>
            _personConditions ??= new PersonConditionRepository(_context, _transaction);

        public IPersonDietaryRequirementRepository PersonDietaryRequirements => _personDietaryRequirements ??=
            new PersonDietaryRequirementRepository(_context, _transaction);

        public IPersonRepository People => _people ??= new PersonRepository(_context, _transaction);

        public IPhoneNumberRepository PhoneNumbers =>
            _phoneNumbers ??= new PhoneNumberRepository(_context, _transaction);

        public IPhoneNumberTypeRepository PhoneNumberTypes =>
            _phoneNumberTypes ??= new PhoneNumberTypeRepository(_transaction);
        
        public IPhotoRepository Photos => _photos ??= new PhotoRepository(_context, _transaction);

        public IProductRepository Products => _products ??= new ProductRepository(_context, _transaction);

        public IRegGroupRepository RegGroups => _regGroups ??= new RegGroupRepository(_context, _transaction);

        public IRelationshipTypeRepository RelationshipTypes =>
            _relationshipTypes ??= new RelationshipTypeRepository(_transaction);

        public IReportCardEntryRepository ReportCardEntries =>
            _reportCardEntries ??= new ReportCardEntryRepository(_context, _transaction);

        public IReportCardRepository ReportCards => _reportCards ??= new ReportCardRepository(_context, _transaction);

        public IReportCardTargetEntryRepository ReportCardTargetEntries => _reportCardTargetEntries ??=
            new ReportCardTargetEntryRepository(_context, _transaction);

        public IReportCardTargetRepository ReportCardTargets =>
            _reportCardTargets ??= new ReportCardTargetRepository(_context, _transaction);

        public IResultRepository Results => _results ??= new ResultRepository(_context, _transaction);

        public IResultSetRepository ResultSets => _resultSets ??= new ResultSetRepository(_context, _transaction);

        public IRoleRepository Roles => _roles ??= new RoleRepository(_context, _transaction);

        public IRoomRepository Rooms => _rooms ??= new RoomRepository(_context, _transaction);

        public IRoomClosureReasonRepository RoomClosureReasons =>
            _roomClosureReasons ??= new RoomClosureReasonRepository(_context, _transaction);

        public IRoomClosureRepository RoomClosures =>
            _roomClosures ??= new RoomClosureRepository(_context, _transaction);

        public ISchoolPhaseRepository SchoolPhases => _schoolPhases ??= new SchoolPhaseRepository(_transaction);

        public ISchoolRepository Schools => _schools ??= new SchoolRepository(_context, _transaction);

        public ISchoolTypeRepository SchoolTypes => _schoolTypes ??= new SchoolTypeRepository(_transaction);

        public ISenEventRepository SenEvents => _senEvents ??= new SenEventRepository(_context, _transaction);

        public ISenEventTypeRepository SenEventTypes => _senEventTypes ??= new SenEventTypeRepository(_transaction);

        public ISenProvisionRepository SenProvisions =>
            _senProvisions ??= new SenProvisionRepository(_context, _transaction);

        public ISenProvisionTypeRepository SenProvisionTypes =>
            _senProvisionTypes ??= new SenProvisionTypeRepository(_transaction);

        public ISenReviewRepository SenReviews => _senReviews ??= new SenReviewRepository(_context, _transaction);

        public ISenReviewTypeRepository SenReviewTypes => _senReviewTypes ??= new SenReviewTypeRepository(_transaction);

        public ISenStatusRepository SenStatus => _senStatus ??= new SenStatusRepository(_transaction);

        public ISenTypeRepository SenTypes => _senTypes ??= new SenTypeRepository(_transaction);

        public ISessionRepository Sessions => _sessions ??= new SessionRepository(_context, _transaction);

        public IStaffMemberRepository StaffMembers =>
            _staffMembers ??= new StaffMemberRepository(_context, _transaction);

        public IStaffAbsenceRepository StaffAbsences =>
            _staffAbsences ??= new StaffAbsenceRepository(_context, _transaction);

        public IStaffAbsenceTypeRepository StaffAbsenceTypes =>
            _staffAbsenceTypes ??= new StaffAbsenceTypeRepository(_context, _transaction);

        public IStaffIllnessTypeRepository StaffIllnessTypes =>
            _staffIllnessTypes ??= new StaffIllnessTypeRepository(_context, _transaction);

        public IStoreDiscountRepository StoreDiscounts =>
            _storeDiscounts ??= new StoreDiscountRepository(_context, _transaction);

        public IStudentAchievementRepository StudentAchievements =>
            _studentAchievements ??= new StudentAchievementRepository(_context, _transaction);

        public IStudentAgentRelationshipRepository StudentAgentRelationships => _studentAgentRelationships ??=
            new StudentAgentRelationshipRepository(_context, _transaction);

        public IStudentChargeRepository StudentCharges =>
            _studentCharges ??= new StudentChargeRepository(_context, _transaction);

        public IStudentContactRelationshipRepository StudentContactRelationships => _studentContactRelationships ??=
            new StudentContactRelationshipRepository(_context, _transaction);

        public IStudentChargeDiscountRepository StudentChargeDiscounts => _studentChargeDiscounts ??=
            new StudentChargeDiscountRepository(_context, _transaction);

        public IStudentChargeDiscountRepository StudentDiscounts =>
            _studentChargeDiscounts ??= new StudentChargeDiscountRepository(_context, _transaction);

        public IStudentRepository Students => _students ??= new StudentRepository(_context, _transaction);

        public IStudentGroupRepository StudentGroups =>
            _studentGroups ??= new StudentGroupRepository(_context, _transaction);

        public IStudentGroupMembershipRepository StudentGroupMemberships => _studentGroupMemberships ??=
            new StudentGroupMembershipRepository(_context, _transaction);

        public IStudentGroupSupervisorRepository StudentGroupSupervisors => _studentGroupSupervisors ??=
            new StudentGroupSupervisorRepository(_context, _transaction);

        public IStudentIncidentRepository StudentIncidents =>
            _studentIncidents ??= new StudentIncidentRepository(_context, _transaction);

        public IStudyTopicRepository StudyTopics => _studyTopics ??= new StudyTopicRepository(_context, _transaction);

        public ISubjectCodeRepository SubjectCodes => _subjectCodes ??= new SubjectCodeRepository(_transaction);

        public ISubjectCodeSetRepository SubjectCodeSets =>
            _subjectCodeSets ??= new SubjectCodeSetRepository(_transaction);

        public ISubjectRepository Subjects => _subjects ??= new SubjectRepository(_context, _transaction);

        public ISubjectStaffMemberRepository SubjectStaffMembers =>
            _subjectStaffMembers ??= new SubjectStaffMemberRepository(_context, _transaction);

        public ISubjectStaffMemberRoleRepository SubjectStaffMemberRoles => _subjectStaffMemberRoles ??=
            new SubjectStaffMemberRoleRepository(_context, _transaction);

        public ISystemSettingRepository SystemSettings =>
            _systemSettings ??= new SystemSettingRepository(_context, _transaction);

        public ITaskRepository Tasks => _tasks ??= new TaskRepository(_context, _transaction);

        public ITaskReminderRepository TaskReminders =>
            _taskReminders ??= new TaskReminderRepository(_context, _transaction);

        public ITaskTypeRepository TaskTypes => _taskTypes ??= new TaskTypeRepository(_context, _transaction);

        public ITrainingCertificateRepository TrainingCertificates =>
            _trainingCertificates ??= new TrainingCertificateRepository(_context, _transaction);

        public ITrainingCertificateStatusRepository TrainingCertificateStatus => _trainingCertificateStatus ??=
            new TrainingCertificateStatusRepository(_context, _transaction);

        public ITrainingCourseRepository TrainingCourses =>
            _trainingCourses ??= new TrainingCourseRepository(_context, _transaction);

        public IUserRoleRepository UserRoles => _userRoles ??= new UserRoleRepository(_context, _transaction);

        public IUserRepository Users => _users ??= new UserRepository(_context, _transaction);

        public IVatRateRepository VatRates => _vatRates ??= new VatRateRepository(_context, _transaction);

        public IYearGroupRepository YearGroups => _yearGroups ??= new YearGroupRepository(_context, _transaction);

        public static async Task<IUnitOfWork> Create(ApplicationDbContext context)
        {
            var unitOfWork = new UnitOfWork(context);
            await unitOfWork.Initialise();
            return unitOfWork;
        }

        private async Task<DbTransaction> GetDbTransaction(bool useContext)
        {
            if (useContext)
            {
                // Use this to utilise the context's own transaction
                var contextTransaction = await _context.Database.BeginTransactionAsync();
                var transaction = contextTransaction.GetDbTransaction();
                return transaction;
            }
            
            // Use this to create a transaction separate from the context
            if (!string.IsNullOrWhiteSpace(_connectionString))
            {
                var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                var transaction = await connection.BeginTransactionAsync();
                return transaction;
            }

            return null;
        }

        private async Task Initialise()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
                ResetRepositories();
            }

            _transaction = await GetDbTransaction(false);
        }

        private UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _connectionString = context.Database.GetConnectionString();
        }

        public int BatchLimit
        {
            get => _batchLimit;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Batch limit cannot be less than 0.");
                }

                _batchLimit = value;
            }
        }

        public void Dispose()
        {
            var connection = _transaction.Connection;
            _transaction?.Dispose();
            connection?.Dispose();
            _context?.Dispose();
        }

        public async Task BatchSaveChangesAsync()
        {
            _batchSize++;

            if (_batchSize >= _batchLimit)
            {
                await SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();

                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
                
                _batchSize = 0;
            }
            catch (Exception)
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                }
                
                throw;
            }
            finally
            {
                await Initialise();
            }
        }

        public async Task<bool> GetLock(string name, int timeout = 0)
        {
            if (_transaction != null)
            {
                return await DatabaseHelper.TryGetApplicationLock(_transaction, name, timeout);
            }

            return false;
        }

        private void ResetRepositories()
        {
            _academicTerms = null;
            _academicYears = null;
            _accountTransactions = null;
            _achievementOutcomes = null;
            _achievements = null;
            _achievementTypes = null;
            _activities = null;
            _addressAgencies = null;
            _addressPeople = null;
            _addressTypes = null;
            _addresses = null;
            _agencies = null;
            _agents = null;
            _agentTypes = null;
            _aspects = null;
            _aspectTypes = null;
            _attendanceCodeTypes = null;
            _attendanceCodes = null;
            _attendanceMarks = null;
            _attendancePeriods = null;
            _attendanceWeeks = null;
            _attendanceWeekPatterns = null;
            _basketItems = null;
            _behaviourOutcomes = null;
            _behaviourRoleTypes = null;
            _behaviourStatus = null;
            _behaviourTargets = null;
            _billItems = null;
            _bills = null;
            _billAccountTransactions = null;
            _billDiscounts = null;
            _billStudentCharges = null;
            _boarderStatus = null;
            _buildings = null;
            _buildingFloors = null;
            _bulletins = null;
            _chargeBillingPeriods = null;
            _chargeDiscounts = null;
            _charges = null;
            _classes = null;
            _commentBanks = null;
            _commentBankAreas = null;
            _commentBankSections = null;
            _comments = null;
            _communicationLogs = null;
            _communicationTypes = null;
            _contacts = null;
            _courses = null;
            _coverArrangements = null;
            _curriculumBandBlockAssignments = null;
            _curriculumBands = null;
            _curriculumBlocks = null;
            _curriculumGroups = null;
            _curriculumYearGroups = null;
            _detentions = null;
            _detentionTypes = null;
            _diaryEventAttendees = null;
            _diaryEventAttendeeResponses = null;
            _diaryEvents = null;
            _diaryEventTemplates = null;
            _diaryEventTypes = null;
            _dietaryRequirements = null;
            _directories = null;
            _discounts = null;
            _documents = null;
            _documentTypes = null;
            _emailAddresses = null;
            _emailAddressTypes = null;
            _enrolmentStatus = null;
            _ethnicities = null;
            _examAssessmentAspects = null;
            _examAssessments = null;
            _examAssessmentModes = null;
            _examAwardElements = null;
            _examAwards = null;
            _examAwardSeries = null;
            _examBaseComponents = null;
            _examBaseElements = null;
            _examBoards = null;
            _examCandidates = null;
            _examCandidateSeries = null;
            _examCandidateSpecialArrangements = null;
            _examComponents = null;
            _examComponentSittings = null;
            _examDates = null;
            _examElements = null;
            _examEnrolments = null;
            _examQualificationLevels = null;
            _examQualifications = null;
            _examResultEmbargoes = null;
            _examRooms = null;
            _examRoomSeatBlocks = null;
            _examSeasons = null;
            _examSeatAllocations = null;
            _examSeries = null;
            _examSessions = null;
            _examSpecialArrangements = null;
            _exclusions = null;
            _exclusionAppealResults = null;
            _exclusionTypes = null;
            _files = null;
            _giftedTalented = null;
            _governanceTypes = null;
            _grades = null;
            _gradeSets = null;
            _homeworkItems = null;
            _homeworkSubmissions = null;
            _houses = null;
            _incidentDetentions = null;
            _incidents = null;
            _incidentTypes = null;
            _intakeTypes = null;
            _languages = null;
            _lessonPlans = null;
            _lessonPlanHomeworkItems = null;
            _lessonPlanTemplates = null;
            _localAuthorities = null;
            _locations = null;
            _logNotes = null;
            _logNoteTypes = null;
            _marksheetColumns = null;
            _marksheets = null;
            _marksheetTemplates = null;
            _medicalConditions = null;
            _medicalConditions = null;
            _nextOfKin = null;
            _nextOfKinRelationshipTypes = null;
            _observationOutcomes = null;
            _observations = null;
            _parentEveningAppointments = null;
            _parentEveningBreaks = null;
            _parentEveningGroups = null;
            _parentEvenings = null;
            _parentEveningStaffMembers = null;
            _personConditions = null;
            _personDietaryRequirements = null;
            _people = null;
            _phoneNumbers = null;
            _phoneNumberTypes = null;
            _photos = null;
            _products = null;
            _regGroups = null;
            _relationshipTypes = null;
            _reportCardEntries = null;
            _reportCards = null;
            _reportCardTargetEntries = null;
            _reportCardTargets = null;
            _results = null;
            _resultSets = null;
            _roles = null;
            _rooms = null;
            _roomClosureReasons = null;
            _roomClosures = null;
            _schoolPhases = null;
            _schools = null;
            _schoolTypes = null;
            _senEvents = null;
            _senEventTypes = null;
            _senProvisions = null;
            _senProvisionTypes = null;
            _senReviews = null;
            _senReviewTypes = null;
            _senStatus = null;
            _senTypes = null;
            _sessions = null;
            _staffAbsences = null;
            _staffAbsenceTypes = null;
            _staffIllnessTypes = null;
            _staffMembers = null;
            _storeDiscounts = null;
            _studentAchievements = null;
            _studentCharges = null;
            _studentContactRelationships = null;
            _studentChargeDiscounts = null;
            _studentAgentRelationships = null;
            _students = null;
            _studentGroups = null;
            _studentGroupMemberships = null;
            _studentGroupSupervisors = null;
            _studentIncidents = null;
            _studyTopics = null;
            _subjectCodes = null;
            _subjectCodeSets = null;
            _subjects = null;
            _subjectStaffMembers = null;
            _subjectStaffMemberRoles = null;
            _systemSettings = null;
            _tasks = null;
            _taskReminders = null;
            _taskTypes = null;
            _trainingCertificates = null;
            _trainingCertificateStatus = null;
            _trainingCourses = null;
            _userRoles = null;
            _users = null;
            _vatRates = null;
            _yearGroups = null;
        }

        public async ValueTask DisposeAsync()
        {
            var connection = _transaction.Connection;

            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }

            if (connection != null)
            {
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }

            if (_context != null)
            {
                await _context.DisposeAsync();
            }
        }
    }
}