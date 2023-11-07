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
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Repositories;

namespace MyPortal.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private bool _auditEnabled;
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
        private IStudentDetentionRepository _studentDetentions;
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
        private ISessionExtraNameRepository _sessionExtraNames;
        private ISessionPeriodRepository _sessionPeriods;
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
        private readonly Guid _userId;

        public bool AuditEnabled
        {
            get => _auditEnabled;
            set
            {
                if (_auditEnabled == value)
                {
                    return;
                }

                _auditEnabled = value;
                ResetRepositories();
            }
        }

        public IAcademicTermRepository AcademicTerms =>
            _academicTerms ??= new AcademicTermRepository(GetDbUserWithContext());

        public IAcademicYearRepository AcademicYears =>
            _academicYears ??= new AcademicYearRepository(GetDbUserWithContext());

        public IAccountTransactionRepository AccountTransactions =>
            _accountTransactions ??= new AccountTransactionRepository(GetDbUserWithContext());

        public IAchievementOutcomeRepository AchievementOutcomes =>
            _achievementOutcomes ??= new AchievementOutcomeRepository(GetDbUserWithContext());

        public IAchievementRepository Achievements =>
            _achievements ??= new AchievementRepository(GetDbUserWithContext());

        public IAchievementTypeRepository AchievementTypes =>
            _achievementTypes ??= new AchievementTypeRepository(GetDbUserWithContext());

        public IActivityRepository Activities =>
            _activities ??= new ActivityRepository(GetDbUserWithContext());

        public IAddressAgencyRepository AddressAgencies =>
            _addressAgencies ??= new AddressAgencyRepository(GetDbUserWithContext());

        public IAddressPersonRepository AddressPeople =>
            _addressPeople ??= new AddressPersonRepository(GetDbUserWithContext());

        public IAddressRepository Addresses =>
            _addresses ??= new AddressRepository(GetDbUserWithContext());

        public IAddressTypeRepository AddressTypes =>
            _addressTypes ??= new AddressTypeRepository(GetDbUser());

        public IAgencyRepository Agencies => _agencies ??= new AgencyRepository(GetDbUserWithContext());

        public IAgentRepository Agents => _agents ??= new AgentRepository(GetDbUserWithContext());

        public IAgentTypeRepository AgentTypes => _agentTypes ??= new AgentTypeRepository(GetDbUser());

        public IAspectRepository Aspects =>
            _aspects ??= new AspectRepository(GetDbUserWithContext());

        public IAspectTypeRepository AspectTypes =>
            _aspectTypes ??= new AspectTypeRepository(GetDbUser());

        public IAttendanceCodeTypeRepository AttendanceCodeTypes =>
            _attendanceCodeTypes ??= new AttendanceCodeTypeRepository(GetDbUser());

        public IAttendanceCodeRepository AttendanceCodes =>
            _attendanceCodes ??= new AttendanceCodeRepository(GetDbUserWithContext());

        public IAttendanceMarkRepository AttendanceMarks =>
            _attendanceMarks ??= new AttendanceMarkRepository(GetDbUserWithContext());

        public IAttendancePeriodRepository AttendancePeriods =>
            _attendancePeriods ??= new AttendancePeriodRepository(GetDbUserWithContext());

        public IAttendanceWeekRepository AttendanceWeeks =>
            _attendanceWeeks ??= new AttendanceWeekRepository(GetDbUserWithContext());

        public IAttendanceWeekPatternRepository AttendanceWeekPatterns => _attendanceWeekPatterns ??=
            new AttendanceWeekPatternRepository(GetDbUserWithContext());

        public IBasketItemRepository BasketItems =>
            _basketItems ??= new BasketItemRepository(GetDbUserWithContext());

        public IBehaviourOutcomeRepository BehaviourOutcomes =>
            _behaviourOutcomes ??= new BehaviourOutcomeRepository(GetDbUserWithContext());

        public IBehaviourRoleTypeRepository BehaviourRoleTypes =>
            _behaviourRoleTypes ??= new BehaviourRoleTypeRepository(GetDbUserWithContext());

        public IBehaviourStatusRepository BehaviourStatus =>
            _behaviourStatus ??= new BehaviourStatusRepository(GetDbUser());

        public IBehaviourTargetRepository BehaviourTargets =>
            _behaviourTargets ??= new BehaviourTargetRepository(GetDbUserWithContext());

        public IBillItemRepository BillItems => _billItems ??= new BillItemRepository(GetDbUserWithContext());

        public IBillRepository Bills => _bills ??= new BillRepository(GetDbUserWithContext());

        public IBillAccountTransactionRepository BillAccountTransactions => _billAccountTransactions ??=
            new BillAccountTransactionRepository(GetDbUserWithContext());

        public IBillDiscountRepository BillDiscounts =>
            _billDiscounts ??= new BillDiscountRepository(GetDbUserWithContext());

        public IBillStudentChargeRepository BillStudentCharges =>
            _billStudentCharges ??= new BillStudentStudentChargeRepository(GetDbUserWithContext());

        public IBoarderStatusRepository BoarderStatus => _boarderStatus ??= new BoarderStatusRepository(GetDbUser());

        public IBuildingRepository Buildings => _buildings ??= new BuildingRepository(GetDbUserWithContext());

        public IBuildingFloorRepository BuildingFloors =>
            _buildingFloors ??= new BuildingFloorRepository(GetDbUserWithContext());

        public IBulletinRepository Bulletins => _bulletins ??= new BulletinRepository(GetDbUserWithContext());

        public IChargeBillingPeriodRepository ChargeBillingPeriods =>
            _chargeBillingPeriods ??= new ChargeBillingPeriodRepository(GetDbUserWithContext());

        public IChargeDiscountRepository ChargeDiscounts =>
            _chargeDiscounts ??= new ChargeDiscountRepository(GetDbUserWithContext());

        public IChargeRepository Charges => _charges ??= new ChargeRepository(GetDbUserWithContext());

        public IClassRepository Classes => _classes ??= new ClassRepository(GetDbUserWithContext());

        public ICommentBankRepository CommentBanks =>
            _commentBanks ??= new CommentBankRepository(GetDbUserWithContext());

        public ICommentBankAreaRepository CommentBankAreas =>
            _commentBankAreas ??= new CommentBankAreaRepository(GetDbUserWithContext());

        public ICommentBankSectionRepository CommentBankSections =>
            _commentBankSections ??= new CommentBankSectionRepository(GetDbUserWithContext());

        public ICommentRepository Comments => _comments ??= new CommentRepository(GetDbUserWithContext());

        public ICommunicationLogRepository CommunicationLogs =>
            _communicationLogs ??= new CommunicationLogRepository(GetDbUserWithContext());

        public ICommunicationTypeRepository CommunicationTypes =>
            _communicationTypes ??= new CommunicationTypeRepository(GetDbUser());

        public IContactRepository Contacts => _contacts ??= new ContactRepository(GetDbUserWithContext());

        public ICourseRepository Courses => _courses ??= new CourseRepository(GetDbUserWithContext());

        public ICoverArrangementRepository CoverArrangements =>
            _coverArrangements ??= new CoverArrangementRepository(GetDbUserWithContext());

        public ICurriculumBandBlockAssignmentRepository CurriculumBandBlockAssignments =>
            _curriculumBandBlockAssignments ??= new CurriculumBandBlockAssignmentRepository(GetDbUserWithContext());

        public ICurriculumBandRepository CurriculumBands =>
            _curriculumBands ??= new CurriculumBandRepository(GetDbUserWithContext());

        public ICurriculumBlockRepository CurriculumBlocks =>
            _curriculumBlocks ??= new CurriculumBlockRepository(GetDbUserWithContext());

        public ICurriculumGroupRepository CurriculumGroups =>
            _curriculumGroups ??= new CurriculumGroupRepository(GetDbUserWithContext());

        public ICurriculumYearGroupRepository CurriculumYearGroups =>
            _curriculumYearGroups ??= new CurriculumYearGroupRepository(GetDbUser());

        public IDetentionRepository Detentions => _detentions ??= new DetentionRepository(GetDbUserWithContext());

        public IDetentionTypeRepository DetentionTypes =>
            _detentionTypes ??= new DetentionTypeRepository(GetDbUserWithContext());

        public IDiaryEventAttendeeRepository DiaryEventAttendees =>
            _diaryEventAttendees ??= new DiaryEventAttendeeRepository(GetDbUserWithContext());

        public IDiaryEventAttendeeResponseRepository DiaryEventAttendeeResponses => _diaryEventAttendeeResponses ??=
            new DiaryEventAttendeeResponseRepository(GetDbUser());

        public IDiaryEventRepository DiaryEvents => _diaryEvents ??= new DiaryEventRepository(GetDbUserWithContext());

        public IDiaryEventTemplateRepository DiaryEventTemplates =>
            _diaryEventTemplates ??= new DiaryEventTemplateRepository(GetDbUserWithContext());

        public IDiaryEventTypeRepository DiaryEventTypes =>
            _diaryEventTypes ??= new DiaryEventTypeRepository(GetDbUserWithContext());

        public IDietaryRequirementRepository DietaryRequirements =>
            _dietaryRequirements ??= new DietaryRequirementRepository(GetDbUserWithContext());

        public IDirectoryRepository Directories => _directories ??= new DirectoryRepository(GetDbUserWithContext());

        public IDiscountRepository Discounts => _discounts ??= new DiscountRepository(GetDbUserWithContext());

        public IDocumentRepository Documents => _documents ??= new DocumentRepository(GetDbUserWithContext());

        public IDocumentTypeRepository DocumentTypes =>
            _documentTypes ??= new DocumentTypeRepository(GetDbUserWithContext());

        public IEmailAddressRepository EmailAddresses =>
            _emailAddresses ??= new EmailAddressRepository(GetDbUserWithContext());

        public IEmailAddressTypeRepository EmailAddressTypes =>
            _emailAddressTypes ??= new EmailAddressTypeRepository(GetDbUser());

        public IEnrolmentStatusRepository EnrolmentStatus =>
            _enrolmentStatus ??= new EnrolmentStatusRepository(GetDbUser());

        public IEthnicityRepository Ethnicities => _ethnicities ??= new EthnicityRepository(GetDbUser());

        public IExamAssessmentAspectRepository ExamAssessmentAspects => _examAssessmentAspects ??=
            new ExamAssessmentAspectRepository(GetDbUserWithContext());

        public IExamAssessmentRepository ExamAssessments =>
            _examAssessments ??= new ExamAssessmentRepository(GetDbUserWithContext());

        public IExamAssessmentModeRepository ExamAssessmentModes =>
            _examAssessmentModes ??= new ExamAssessmentModeRepository(GetDbUser());

        public IExamAwardElementRepository ExamAwardElements =>
            _examAwardElements ??= new ExamAwardElementRepository(GetDbUserWithContext());

        public IExamAwardRepository ExamAwards => _examAwards ??= new ExamAwardRepository(GetDbUserWithContext());

        public IExamAwardSeriesRepository ExamAwardSeries =>
            _examAwardSeries ??= new ExamAwardSeriesRepository(GetDbUserWithContext());

        public IExamBaseComponentRepository ExamBaseComponents =>
            _examBaseComponents ??= new ExamBaseComponentRepository(GetDbUserWithContext());

        public IExamBaseElementRepository ExamBaseElements =>
            _examBaseElements ??= new ExamBaseElementRepository(GetDbUserWithContext());

        public IExamBoardRepository ExamBoards => _examBoards ??= new ExamBoardRepository(GetDbUserWithContext());

        public IExamCandidateRepository ExamCandidates =>
            _examCandidates ??= new ExamCandidateRepository(GetDbUserWithContext());

        public IExamCandidateSeriesRepository ExamCandidateSeries =>
            _examCandidateSeries ??= new ExamCandidateSeriesRepository(GetDbUserWithContext());

        public IExamCandidateSpecialArrangementRepository ExamCandidateSpecialArrangements =>
            _examCandidateSpecialArrangements ??= new ExamCandidateSpecialArrangementRepository(GetDbUserWithContext());

        public IExamComponentRepository ExamComponents =>
            _examComponents ??= new ExamComponentRepository(GetDbUserWithContext());

        public IExamComponentSittingRepository ExamComponentSittings => _examComponentSittings ??=
            new ExamComponentSittingRepository(GetDbUserWithContext());

        public IExamDateRepository ExamDates => _examDates ??= new ExamDateRepository(GetDbUserWithContext());

        public IExamElementComponentRepository ExamElementComponents => _examElementComponents ??=
            new ExamElementComponentRepository(GetDbUserWithContext());

        public IExamElementRepository ExamElements =>
            _examElements ??= new ExamElementRepository(GetDbUserWithContext());

        public IExamEnrolmentRepository ExamEnrolments =>
            _examEnrolments ??= new ExamEnrolmentRepository(GetDbUserWithContext());

        public IExamQualificationLevelRepository ExamQualificationLevels => _examQualificationLevels ??=
            new ExamQualificationLevelRepository(GetDbUserWithContext());

        public IExamQualificationRepository ExamQualifications =>
            _examQualifications ??= new ExamQualificationRepository(GetDbUserWithContext());

        public IExamResultEmbargoRepository ExamResultEmbargoes =>
            _examResultEmbargoes ??= new ExamResultEmbargoRepository(GetDbUserWithContext());

        public IExamRoomRepository ExamRooms => _examRooms ??= new ExamRoomRepository(GetDbUserWithContext());

        public IExamRoomSeatBlockRepository ExamRoomSeatBlocks =>
            _examRoomSeatBlocks ??= new ExamRoomSeatBlockRepository(GetDbUserWithContext());

        public IExamSeasonRepository ExamSeasons => _examSeasons ??= new ExamSeasonRepository(GetDbUserWithContext());

        public IExamSeatAllocationRepository ExamSeatAllocations =>
            _examSeatAllocations ??= new ExamSeatAllocationRepository(GetDbUserWithContext());

        public IExamSeriesRepository ExamSeries => _examSeries ??= new ExamSeriesRepository(GetDbUserWithContext());

        public IExamSessionRepository ExamSessions =>
            _examSessions ??= new ExamSessionRepository(GetDbUserWithContext());

        public IExamSpecialArrangementRepository ExamSpecialArrangements => _examSpecialArrangements ??=
            new ExamSpecialArrangementRepository(GetDbUserWithContext());

        public IExclusionRepository Exclusions => _exclusions ??= new ExclusionRepository(GetDbUserWithContext());

        public IExclusionAppealResultRepository ExclusionAppealResults =>
            _exclusionAppealResults ??= new ExclusionAppealResultRepository(GetDbUser());

        public IExclusionReasonRepository ExclusionReasons =>
            _exclusionReasons ??= new ExclusionReasonRepository(GetDbUserWithContext());

        public IExclusionTypeRepository ExclusionTypes => _exclusionTypes ??= new ExclusionTypeRepository(GetDbUser());

        public IFileRepository Files => _files ??= new FileRepository(GetDbUserWithContext());

        public IGiftedTalentedRepository GiftedTalented =>
            _giftedTalented ??= new GiftedTalentedRepository(GetDbUserWithContext());

        public IGovernanceTypeRepository GovernanceTypes =>
            _governanceTypes ??= new GovernanceTypeRepository(GetDbUser());

        public IGradeRepository Grades => _grades ??= new GradeRepository(GetDbUserWithContext());

        public IGradeSetRepository GradeSets => _gradeSets ??= new GradeSetRepository(GetDbUserWithContext());

        public IHomeworkItemRepository HomeworkItems =>
            _homeworkItems ??= new HomeworkItemRepository(GetDbUserWithContext());

        public IHomeworkSubmissionRepository HomeworkSubmissions =>
            _homeworkSubmissions ??= new HomeworkSubmissionRepository(GetDbUserWithContext());

        public IHouseRepository Houses => _houses ??= new HouseRepository(GetDbUserWithContext());

        public IStudentDetentionRepository StudentDetentions =>
            _studentDetentions ??= new StudentDetentionRepository(GetDbUserWithContext());

        public IIncidentRepository Incidents => _incidents ??= new IncidentRepository(GetDbUserWithContext());

        public IIncidentTypeRepository IncidentTypes =>
            _incidentTypes ??= new IncidentTypeRepository(GetDbUserWithContext());

        public IIntakeTypeRepository IntakeTypes => _intakeTypes ??= new IntakeTypeRepository(GetDbUser());

        public ILanguageRepository Languages => _languages ??= new LanguageRepository(GetDbUser());

        public ILessonPlanRepository LessonPlans => _lessonPlans ??= new LessonPlanRepository(GetDbUserWithContext());

        public ILessonPlanHomeworkItemRepository LessonPlanHomeworkItems => _lessonPlanHomeworkItems ??=
            new LessonPlanHomeworkItemRepository(GetDbUserWithContext());

        public ILessonPlanTemplateRepository LessonPlanTemplates =>
            _lessonPlanTemplates ??= new LessonPlanTemplateRepository(GetDbUserWithContext());

        public ILocalAuthorityRepository LocalAuthorities =>
            _localAuthorities ??= new LocalAuthorityRepository(GetDbUser());

        public ILocationRepository Locations => _locations ??= new LocationRepository(GetDbUserWithContext());

        public ILogNoteRepository LogNotes => _logNotes ??= new LogNoteRepository(GetDbUserWithContext());

        public ILogNoteTypeRepository LogNoteTypes =>
            _logNoteTypes ??= new LogNoteTypeRepository(GetDbUserWithContext());

        public IMarksheetColumnRepository MarksheetColumns =>
            _marksheetColumns ??= new MarksheetColumnRepository(GetDbUserWithContext());

        public IMarksheetRepository Marksheets => _marksheets ??=
            new MarksheetRepository(GetDbUserWithContext());

        public IMarksheetTemplateRepository MarksheetTemplates =>
            _marksheetTemplates ??= new MarksheetTemplateRepository(GetDbUserWithContext());

        public IMedicalConditionRepository MedicalConditions =>
            _medicalConditions ??= new MedicalConditionRepository(GetDbUserWithContext());

        public IMedicalEventRepository MedicalEvents =>
            _medicalEvents ??= new MedicalEventRepository(GetDbUserWithContext());

        public INextOfKinRepository NextOfKin => _nextOfKin ??= new NextOfKinRepository(GetDbUserWithContext());

        public INextOfKinRelationshipTypeRepository NextOfKinRelationshipTypes => _nextOfKinRelationshipTypes ??=
            new NextOfKinRelationshipTypeRepository(GetDbUser());

        public IObservationOutcomeRepository ObservationOutcomes =>
            _observationOutcomes ??= new ObservationOutcomeRepository(GetDbUser());

        public IObservationRepository Observations =>
            _observations ??= new ObservationRepository(GetDbUserWithContext());

        public IParentEveningAppointmentRepository ParentEveningAppointments => _parentEveningAppointments ??=
            new ParentEveningAppointmentRepository(GetDbUserWithContext());

        public IParentEveningBreakRepository ParentEveningBreaks =>
            _parentEveningBreaks ??= new ParentEveningBreakRepository(GetDbUserWithContext());

        public IParentEveningGroupRepository ParentEveningGroups =>
            _parentEveningGroups ??= new ParentEveningGroupRepository(GetDbUserWithContext());

        public IParentEveningRepository ParentEvenings =>
            _parentEvenings ??= new ParentEveningRepository(GetDbUserWithContext());

        public IParentEveningStaffMemberRepository ParentEveningStaffMembers => _parentEveningStaffMembers ??=
            new ParentEveningStaffMemberRepository(GetDbUserWithContext());

        public IPersonConditionRepository PersonConditions =>
            _personConditions ??= new PersonConditionRepository(GetDbUserWithContext());

        public IPersonDietaryRequirementRepository PersonDietaryRequirements => _personDietaryRequirements ??=
            new PersonDietaryRequirementRepository(GetDbUserWithContext());

        public IPersonRepository People => _people ??= new PersonRepository(GetDbUserWithContext());

        public IPhoneNumberRepository PhoneNumbers =>
            _phoneNumbers ??= new PhoneNumberRepository(GetDbUserWithContext());

        public IPhoneNumberTypeRepository PhoneNumberTypes =>
            _phoneNumberTypes ??= new PhoneNumberTypeRepository(GetDbUser());

        public IPhotoRepository Photos => _photos ??= new PhotoRepository(GetDbUserWithContext());

        public IProductRepository Products => _products ??= new ProductRepository(GetDbUserWithContext());

        public IRegGroupRepository RegGroups => _regGroups ??= new RegGroupRepository(GetDbUserWithContext());

        public IRelationshipTypeRepository RelationshipTypes =>
            _relationshipTypes ??= new RelationshipTypeRepository(GetDbUser());

        public IReportCardEntryRepository ReportCardEntries =>
            _reportCardEntries ??= new ReportCardEntryRepository(GetDbUserWithContext());

        public IReportCardRepository ReportCards => _reportCards ??= new ReportCardRepository(GetDbUserWithContext());

        public IReportCardTargetEntryRepository ReportCardTargetEntries => _reportCardTargetEntries ??=
            new ReportCardTargetEntryRepository(GetDbUserWithContext());

        public IReportCardTargetRepository ReportCardTargets =>
            _reportCardTargets ??= new ReportCardTargetRepository(GetDbUserWithContext());

        public IResultRepository Results => _results ??= new ResultRepository(GetDbUserWithContext());

        public IResultSetRepository ResultSets => _resultSets ??= new ResultSetRepository(GetDbUserWithContext());

        public IRoleRepository Roles => _roles ??= new RoleRepository(GetDbUserWithContext());

        public IRoomRepository Rooms => _rooms ??= new RoomRepository(GetDbUserWithContext());

        public IRoomClosureReasonRepository RoomClosureReasons =>
            _roomClosureReasons ??= new RoomClosureReasonRepository(GetDbUserWithContext());

        public IRoomClosureRepository RoomClosures =>
            _roomClosures ??= new RoomClosureRepository(GetDbUserWithContext());

        public ISchoolPhaseRepository SchoolPhases => _schoolPhases ??= new SchoolPhaseRepository(GetDbUser());

        public ISchoolRepository Schools => _schools ??= new SchoolRepository(GetDbUserWithContext());

        public ISchoolTypeRepository SchoolTypes => _schoolTypes ??= new SchoolTypeRepository(GetDbUser());

        public ISenEventRepository SenEvents => _senEvents ??= new SenEventRepository(GetDbUserWithContext());

        public ISenEventTypeRepository SenEventTypes => _senEventTypes ??= new SenEventTypeRepository(GetDbUser());

        public ISenProvisionRepository SenProvisions =>
            _senProvisions ??= new SenProvisionRepository(GetDbUserWithContext());

        public ISenProvisionTypeRepository SenProvisionTypes =>
            _senProvisionTypes ??= new SenProvisionTypeRepository(GetDbUser());

        public ISenReviewRepository SenReviews => _senReviews ??= new SenReviewRepository(GetDbUserWithContext());

        public ISenReviewTypeRepository SenReviewTypes => _senReviewTypes ??= new SenReviewTypeRepository(GetDbUser());

        public ISenStatusRepository SenStatus => _senStatus ??= new SenStatusRepository(GetDbUser());

        public ISenTypeRepository SenTypes => _senTypes ??= new SenTypeRepository(GetDbUser());

        public ISessionRepository Sessions => _sessions ??= new SessionRepository(GetDbUserWithContext());

        public ISessionExtraNameRepository SessionExtraNames =>
            _sessionExtraNames ??= new SessionExtraNameRepository(GetDbUserWithContext());

        public ISessionPeriodRepository SessionPeriods =>
            _sessionPeriods ??= new SessionPeriodRepository(GetDbUserWithContext());

        public IStaffMemberRepository StaffMembers =>
            _staffMembers ??= new StaffMemberRepository(GetDbUserWithContext());

        public IStaffAbsenceRepository StaffAbsences =>
            _staffAbsences ??= new StaffAbsenceRepository(GetDbUserWithContext());

        public IStaffAbsenceTypeRepository StaffAbsenceTypes =>
            _staffAbsenceTypes ??= new StaffAbsenceTypeRepository(GetDbUserWithContext());

        public IStaffIllnessTypeRepository StaffIllnessTypes =>
            _staffIllnessTypes ??= new StaffIllnessTypeRepository(GetDbUserWithContext());

        public IStoreDiscountRepository StoreDiscounts =>
            _storeDiscounts ??= new StoreDiscountRepository(GetDbUserWithContext());

        public IStudentAchievementRepository StudentAchievements =>
            _studentAchievements ??= new StudentAchievementRepository(GetDbUserWithContext());

        public IStudentAgentRelationshipRepository StudentAgentRelationships => _studentAgentRelationships ??=
            new StudentAgentRelationshipRepository(GetDbUserWithContext());

        public IStudentChargeRepository StudentCharges =>
            _studentCharges ??= new StudentChargeRepository(GetDbUserWithContext());

        public IStudentContactRelationshipRepository StudentContactRelationships => _studentContactRelationships ??=
            new StudentContactRelationshipRepository(GetDbUserWithContext());

        public IStudentChargeDiscountRepository StudentChargeDiscounts => _studentChargeDiscounts ??=
            new StudentChargeDiscountRepository(GetDbUserWithContext());

        public IStudentChargeDiscountRepository StudentDiscounts =>
            _studentChargeDiscounts ??= new StudentChargeDiscountRepository(GetDbUserWithContext());

        public IStudentRepository Students => _students ??= new StudentRepository(GetDbUserWithContext());

        public IStudentGroupRepository StudentGroups =>
            _studentGroups ??= new StudentGroupRepository(GetDbUserWithContext());

        public IStudentGroupMembershipRepository StudentGroupMemberships => _studentGroupMemberships ??=
            new StudentGroupMembershipRepository(GetDbUserWithContext());

        public IStudentGroupSupervisorRepository StudentGroupSupervisors => _studentGroupSupervisors ??=
            new StudentGroupSupervisorRepository(GetDbUserWithContext());

        public IStudentIncidentRepository StudentIncidents =>
            _studentIncidents ??= new StudentIncidentRepository(GetDbUserWithContext());

        public IStudyTopicRepository StudyTopics => _studyTopics ??= new StudyTopicRepository(GetDbUserWithContext());

        public ISubjectCodeRepository SubjectCodes => _subjectCodes ??= new SubjectCodeRepository(GetDbUser());

        public ISubjectCodeSetRepository SubjectCodeSets =>
            _subjectCodeSets ??= new SubjectCodeSetRepository(GetDbUser());

        public ISubjectRepository Subjects => _subjects ??= new SubjectRepository(GetDbUserWithContext());

        public ISubjectStaffMemberRepository SubjectStaffMembers =>
            _subjectStaffMembers ??= new SubjectStaffMemberRepository(GetDbUserWithContext());

        public ISubjectStaffMemberRoleRepository SubjectStaffMemberRoles => _subjectStaffMemberRoles ??=
            new SubjectStaffMemberRoleRepository(GetDbUserWithContext());

        public ISystemSettingRepository SystemSettings =>
            _systemSettings ??= new SystemSettingRepository(GetDbUserWithContext());

        public ITaskRepository Tasks => _tasks ??= new TaskRepository(GetDbUserWithContext());

        public ITaskReminderRepository TaskReminders =>
            _taskReminders ??= new TaskReminderRepository(GetDbUserWithContext());

        public ITaskTypeRepository TaskTypes => _taskTypes ??= new TaskTypeRepository(GetDbUserWithContext());

        public ITrainingCertificateRepository TrainingCertificates =>
            _trainingCertificates ??= new TrainingCertificateRepository(GetDbUserWithContext());

        public ITrainingCertificateStatusRepository TrainingCertificateStatus => _trainingCertificateStatus ??=
            new TrainingCertificateStatusRepository(GetDbUserWithContext());

        public ITrainingCourseRepository TrainingCourses =>
            _trainingCourses ??= new TrainingCourseRepository(GetDbUserWithContext());

        public IUserRoleRepository UserRoles => _userRoles ??= new UserRoleRepository(GetDbUserWithContext());

        public IUserRepository Users => _users ??= new UserRepository(GetDbUserWithContext());

        public IVatRateRepository VatRates => _vatRates ??= new VatRateRepository(GetDbUserWithContext());

        public IYearGroupRepository YearGroups => _yearGroups ??= new YearGroupRepository(GetDbUserWithContext());

        public static async Task<IUnitOfWork> Create(Guid userId, ApplicationDbContext context)
        {
            var unitOfWork = new UnitOfWork(userId, context);
            var auditSetting = await unitOfWork.SystemSettings.Get(Constants.SystemSettings.AuditEnabled);
            if (auditSetting.Setting == "1")
            {
                unitOfWork.AuditEnabled = true;
            }

            await unitOfWork.Initialise();
            return unitOfWork;
        }

        private DbUser GetDbUser()
        {
            return new DbUser(_userId, _transaction, AuditEnabled);
        }

        private DbUserWithContext GetDbUserWithContext()
        {
            return new DbUserWithContext(_userId, _transaction, _context, AuditEnabled);
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

        private UnitOfWork(Guid userId, ApplicationDbContext context)
        {
            _userId = userId;
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
            _studentDetentions = null;
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
            _sessionExtraNames = null;
            _sessionPeriods = null;
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
            ResetRepositories();

            var connection = _transaction.Connection;

            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }

            if (connection != null)
            {
                await connection.DisposeAsync();
            }

            if (_context != null)
            {
                await _context.DisposeAsync();
            }
        }
    }
}