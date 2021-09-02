﻿using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private int _batchSize;
        private int _batchLimit = 1000;

        private DbTransaction _transaction;
        private IAcademicTermRepository _academicTerms;
        private IAcademicYearRepository _academicYears;
        private IAccountTransactionRepository _accountTransactions;
        private IAchievementOutcomeRepository _achievementOutcomes;
        private IAchievementRepository _achievements;
        private IAchievementTypeRepository _achievementTypes;
        private IActivityEventRepository _activityEvents;
        private IActivityRepository _activities;
        private IAddressPersonRepository _addressPersons;
        private IAddressRepository _addresses;
        private IAddressTypeRepository _addressTypes;
        private IAgencyRepository _agencies;
        private IAgentRepository _agents;
        private IAgentTypeRepository _agentTypes;
        private IAspectRepository _aspects;
        private IAspectTypeRepository _aspectTypes;
        private IAttendanceCodeMeaningRepository _attendanceCodeMeanings;
        private IAttendanceCodeRepository _attendanceCodes;
        private IAttendanceMarkRepository _attendanceMarks;
        private IAttendancePeriodRepository _attendancePeriods;
        private IAttendanceWeekRepository _attendanceWeeks;
        private IBasketItemRepository _basketItems;
        private IBehaviourOutcomeRepository _behaviourOutcomes;
        private IBehaviourStatusRepository _behaviourStatus;
        private IBillItemRepository _billItems;
        private IBillRepository _bills;
        private IBulletinRepository _bulletins;
        private IChargeDiscountRepository _chargeDiscounts;
        private IChargeRepository _charges;
        private IClassRepository _classes;
        private ICommentBankRepository _commentBanks;
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
        private IDiaryEventRepository _diaryEvents;
        private IDiaryEventTemplateRepository _diaryEventTemplates;
        private IDiaryEventTypeRepository _diaryEventTypes;
        private IDietaryRequirementRepository _dietaryRequirements;
        private IDirectoryRepository _directories;
        private IDocumentRepository _documents;
        private IDocumentTypeRepository _documentTypes;
        private IEmailAddressRepository _emailAddresses;
        private IEmailAddressTypeRepository _emailAddressTypes;
        private IExamAssessmentAspectRepository _examAssessmentAspects;
        private IExamAssessmentModeRepository _examAssessmentModes;
        private IExamAssessmentRepository _examAssessments;
        private IExamAwardElementRepository _examAwardElements;
        private IExamAwardRepository _examAwards;
        private IExamAwardSeriesRepository _examAwardSeries;
        private IExamBaseComponentRepository _examBaseComponents;
        private IExclusionRepository _exclusions;
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
        private ILessonPlanRepository _lessonPlans;
        private ILessonPlanTemplateRepository _lessonPlanTemplates;
        private ILocalAuthorityRepository _localAuthorities;
        private ILocationRepository _locations;
        private ILogNoteRepository _logNotes;
        private ILogNoteTypeRepository _logNoteTypes;
        private IMedicalConditionRepository _medicalConditions;
        private IMedicalEventRepository _medicalEvents;
        private IObservationOutcomeRepository _observationOutcomes;
        private IObservationRepository _observations;
        private IPermissionRepository _permissions;
        private IPersonConditionRepository _personConditions;
        private IPersonDietaryRequirementRepository _personDietaryRequirements;
        private IPersonRepository _people;
        private IPhoneNumberRepository _phoneNumbers;
        private IProductRepository _products;
        private IRefreshTokenRepository _refreshTokens;
        private IRegGroupRepository _regGroups;
        private IResultRepository _results;
        private IRoleRepository _roles;
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
        private ISessionRepository _sessions;
        private IStaffMemberRepository _staffMembers;
        private IStudentChargeDiscountRepository _studentChargeDiscounts;
        private IStudentChargeRepository _studentCharges;
        private IStudentContactRelationshipRepository _studentContactRelationships;
        private IStudentGroupRepository _studentGroups;
        private IStudentRepository _students;
        private IStudyTopicRepository _studyTopics;
        private ISubjectCodeSetRepository _subjectCodeSets;
        private ISubjectRepository _subjects;
        private ISubjectStaffMemberRepository _subjectStaffMembers;
        private ISubjectStaffMemberRoleRepository _subjectStaffMemberRoles;
        private ISystemAreaRepository _systemAreas;
        private ISystemSettingRepository _systemSettings;
        private ITaskRepository _tasks;
        private ITaskTypeRepository _taskTypes;
        private ITrainingCertificateRepository _trainingCertificates;
        private ITrainingCertificateStatusRepository _trainingCertificateStatus;
        private ITrainingCourseRepository _trainingCourses;
        private IUserRepository _users;
        private IUserRoleRepository _userRoles;
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

        public IActivityEventRepository ActivityEvents =>
            _activityEvents ??= new ActivityEventRepository(_context, _transaction);

        public IActivityRepository Activities =>
            _activities ??= new ActivityRepository(_context, _transaction);

        public IAddressPersonRepository AddressPersons =>
            _addressPersons ??= new AddressPersonRepository(_context, _transaction);

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

        public IAttendanceCodeMeaningRepository AttendanceCodeMeanings =>
            _attendanceCodeMeanings ??= new AttendanceCodeMeaningRepository(_transaction);

        public IAttendanceCodeRepository AttendanceCodes =>
            _attendanceCodes = new AttendanceCodeRepository(_context, _transaction);

        public IAttendanceMarkRepository AttendanceMarks =>
            _attendanceMarks ??= new AttendanceMarkRepository(_context, _transaction);

        public IAttendancePeriodRepository AttendancePeriods =>
            _attendancePeriods ??= new AttendancePeriodRepository(_context, _transaction);

        public IAttendanceWeekRepository AttendanceWeeks =>
            _attendanceWeeks ??= new AttendanceWeekRepository(_context, _transaction);

        public IBasketItemRepository BasketItems =>
            _basketItems ??= new BasketItemRepository(_context, _transaction);

        public IBehaviourOutcomeRepository BehaviourOutcomes =>
            _behaviourOutcomes ??= new BehaviourOutcomeRepository(_context, _transaction);

        public IBehaviourStatusRepository BehaviourStatus =>
            _behaviourStatus ??= new BehaviourStatusRepository(_transaction);

        public IBillItemRepository BillItems => _billItems ??= new BillItemRepository(_context, _transaction);

        public IBillRepository Bills => _bills ??= new BillRepository(_context, _transaction);

        public IBulletinRepository Bulletins => _bulletins ??= new BulletinRepository(_context, _transaction);

        public IChargeDiscountRepository ChargeDiscounts =>
            _chargeDiscounts ??= new ChargeDiscountRepository(_context, _transaction);

        public IChargeRepository Charges => _charges ??= new ChargeRepository(_context, _transaction);

        public IClassRepository Classes => _classes ??= new ClassRepository(_context, _transaction);

        public ICommentBankRepository CommentBanks =>
            _commentBanks ??= new CommentBankRepository(_context, _transaction);

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

        public IDiaryEventRepository DiaryEvents => _diaryEvents ??= new DiaryEventRepository(_context, _transaction);

        public IDiaryEventTemplateRepository DiaryEventTemplates =>
            _diaryEventTemplates ??= new DiaryEventTemplateRepository(_context, _transaction);

        public IDiaryEventTypeRepository DiaryEventTypes =>
            _diaryEventTypes ??= new DiaryEventTypeRepository(_context, _transaction);

        public IDietaryRequirementRepository DietaryRequirements =>
            _dietaryRequirements ??= new DietaryRequirementRepository(_transaction);

        public IDirectoryRepository Directories => _directories ??= new DirectoryRepository(_context, _transaction);

        public IDocumentRepository Documents => _documents ??= new DocumentRepository(_context, _transaction);

        public IDocumentTypeRepository DocumentTypes =>
            _documentTypes ??= new DocumentTypeRepository(_context, _transaction);

        public IEmailAddressRepository EmailAddresses =>
            _emailAddresses ??= new EmailAddressRepository(_context, _transaction);

        public IEmailAddressTypeRepository EmailAddressTypes =>
            _emailAddressTypes ??= new EmailAddressTypeRepository(_transaction);

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

        public IExclusionRepository Exclusions => _exclusions ??= new ExclusionRepository(_context, _transaction);

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

        public ILessonPlanRepository LessonPlans => _lessonPlans ??= new LessonPlanRepository(_context, _transaction);

        public ILessonPlanTemplateRepository LessonPlanTemplates =>
            _lessonPlanTemplates ??= new LessonPlanTemplateRepository(_context, _transaction);

        public ILocalAuthorityRepository LocalAuthorities =>
            _localAuthorities ??= new LocalAuthorityRepository(_transaction);

        public ILocationRepository Locations => _locations ??= new LocationRepository(_context, _transaction);

        public ILogNoteRepository LogNotes => _logNotes ??= new LogNoteRepository(_context, _transaction);

        public ILogNoteTypeRepository LogNoteTypes => _logNoteTypes ??= new LogNoteTypeRepository(_transaction);

        public IMedicalConditionRepository MedicalConditions =>
            _medicalConditions ??= new MedicalConditionRepository(_context, _transaction);

        public IMedicalEventRepository MedicalEvents =>
            _medicalEvents ??= new MedicalEventRepository(_context, _transaction);

        public IObservationOutcomeRepository ObservationOutcomes =>
            _observationOutcomes ??= new ObservationOutcomeRepository(_transaction);

        public IObservationRepository Observations =>
            _observations ??= new ObservationRepository(_context, _transaction);

        public IPermissionRepository Permissions => _permissions ??= new PermissionRepository(_transaction);

        public IPersonConditionRepository PersonConditions =>
            _personConditions ??= new PersonConditionRepository(_context, _transaction);

        public IPersonDietaryRequirementRepository PersonDietaryRequirements => _personDietaryRequirements ??=
            new PersonDietaryRequirementRepository(_context, _transaction);

        public IPersonRepository People => _people ??= new PersonRepository(_context, _transaction);

        public IPhoneNumberRepository PhoneNumbers =>
            _phoneNumbers ??= new PhoneNumberRepository(_context, _transaction);

        public IProductRepository Products => _products ??= new ProductRepository(_context, _transaction);

        public IRefreshTokenRepository RefreshTokens =>
            _refreshTokens ??= new RefreshTokenRepository(_context, _transaction);

        public IRegGroupRepository RegGroups => _regGroups ??= new RegGroupRepository(_context, _transaction);

        public IResultRepository Results => _results ??= new ResultRepository(_context, _transaction);

        public IRoleRepository Roles => _roles ??= new RoleRepository(_context, _transaction);

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

        public ISessionRepository Sessions => _sessions ??= new SessionRepository(_context, _transaction);

        public IStaffMemberRepository StaffMembers =>
            _staffMembers ??= new StaffMemberRepository(_context, _transaction);

        public IStudentChargeRepository StudentCharges =>
            _studentCharges ??= new StudentChargeRepository(_context, _transaction);

        public IStudentContactRelationshipRepository StudentContactRelationships => _studentContactRelationships ??=
            new StudentContactRelationshipRepository(_context, _transaction);

        public IStudentChargeDiscountRepository StudentChargeDiscounts { get; }

        public IStudentChargeDiscountRepository StudentDiscounts =>
            _studentChargeDiscounts ??= new StudentChargeDiscountRepository(_context, _transaction);

        public IStudentRepository Students => _students ??= new StudentRepository(_context, _transaction);

        public IStudentGroupRepository StudentGroups =>
            _studentGroups ??= new StudentGroupRepository(_context, _transaction);

        public IStudyTopicRepository StudyTopics => _studyTopics ??= new StudyTopicRepository(_context, _transaction);

        public ISubjectCodeSetRepository SubjectCodeSets =>
            _subjectCodeSets ??= new SubjectCodeSetRepository(_transaction);

        public ISubjectRepository Subjects => _subjects ??= new SubjectRepository(_context, _transaction);

        public ISubjectStaffMemberRepository SubjectStaffMembers =>
            _subjectStaffMembers ??= new SubjectStaffMemberRepository(_context, _transaction);

        public ISubjectStaffMemberRoleRepository SubjectStaffMemberRoles => _subjectStaffMemberRoles ??=
            new SubjectStaffMemberRoleRepository(_context, _transaction);

        public ISystemAreaRepository SystemAreas => _systemAreas ??= new SystemAreaRepository(_transaction);

        public ISystemSettingRepository SystemSettings =>
            _systemSettings ??= new SystemSettingRepository(_context, _transaction);

        public ITaskRepository Tasks => _tasks ??= new TaskRepository(_context, _transaction);

        public ITaskTypeRepository TaskTypes => _taskTypes ??= new TaskTypeRepository(_context, _transaction);

        public ITrainingCertificateRepository TrainingCertificates =>
            _trainingCertificates ??= new TrainingCertificateRepository(_context, _transaction);

        public ITrainingCertificateStatusRepository TrainingCertificateStatus => _trainingCertificateStatus ??=
            new TrainingCertificateStatusRepository(_context, _transaction);

        public ITrainingCourseRepository TrainingCourses =>
            _trainingCourses ??= new TrainingCourseRepository(_context, _transaction);

        public IUserRoleRepository UserRoles => _userRoles ??= new UserRoleRepository(_context, _transaction);

        public IUserRepository Users => _users ??= new UserRepository(_context, _transaction);

        public IYearGroupRepository YearGroups => _yearGroups ??= new YearGroupRepository(_context, _transaction);

        public static async Task<IUnitOfWork> Create(ApplicationDbContext context)
        {
            var unitOfWork = new UnitOfWork(context);
            await unitOfWork.Initialise();
            return unitOfWork;
        }

        private async Task<DbTransaction> GetDbTransaction(ApplicationDbContext context)
        {
            var contextTransaction = await _context.Database.BeginTransactionAsync();

            var transaction = contextTransaction.GetDbTransaction();

            return transaction;
        }

        private async Task Initialise()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
                ResetRepositories();
            }

            _transaction = await GetDbTransaction(_context);
        }

        private UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
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
            _transaction?.Dispose();
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
                await _context.Database.CurrentTransaction.CommitAsync();
                _batchSize = 0;
            }
            catch (Exception)
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
                throw;
            }
            finally
            {
                await Initialise();
            }
        }

        private void ResetRepositories()
        {
            _academicTerms = null;
            _academicYears = null;
            _accountTransactions = null;
            _achievementOutcomes = null;
            _achievements = null;
            _achievementTypes = null;
            _activityEvents = null;
            _activities = null;
            _addressPersons = null;
            _addressTypes = null;
            _addresses = null;
            _agencies = null;
            _agents = null;
            _agentTypes = null;
            _aspects = null;
            _aspectTypes = null;
            _attendanceCodeMeanings = null;
            _attendanceCodes = null;
            _attendanceMarks = null;
            _attendancePeriods = null;
            _attendanceWeeks = null;
            _basketItems = null;
            _behaviourOutcomes = null;
            _behaviourStatus = null;
            _billItems = null;
            _bills = null;
            _bulletins = null;
            _chargeDiscounts = null;
            _charges = null;
            _classes = null;
            _commentBanks = null;
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
            _diaryEvents = null;
            _diaryEventTemplates = null;
            _diaryEventTypes = null;
            _dietaryRequirements = null;
            _directories = null;
            _documents = null;
            _documentTypes = null;
            _emailAddresses = null;
            _emailAddressTypes = null;
            _examAssessmentAspects = null;
            _examAssessments = null;
            _examAssessmentModes = null;
            _examAwardElements = null;
            _examAwards = null;
            _examAwardSeries = null;
            _examBaseComponents = null;
            _exclusions = null;
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
            _lessonPlans = null;
            _lessonPlanTemplates = null;
            _localAuthorities = null;
            _locations = null;
            _logNotes = null;
            _logNoteTypes = null;
            _medicalConditions = null;
            _medicalConditions = null;
            _observationOutcomes = null;
            _observations = null;
            _permissions = null;
            _personConditions = null;
            _personDietaryRequirements = null;
            _people = null;
            _phoneNumbers = null;
            _products = null;
            _refreshTokens = null;
            _regGroups = null;
            _results = null;
            _roles = null;
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
            _sessions = null;
            _staffMembers = null;
            _studentCharges = null;
            _studentContactRelationships = null;
            _studentChargeDiscounts = null;
            _students = null;
            _studentGroups = null;
            _studyTopics = null;
            _subjectCodeSets = null;
            _subjects = null;
            _subjectStaffMembers = null;
            _subjectStaffMemberRoles = null;
            _systemAreas = null;
            _systemSettings = null;
            _tasks = null;
            _taskTypes = null;
            _trainingCertificates = null;
            _trainingCertificateStatus = null;
            _trainingCourses = null;
            _userRoles = null;
            _users = null;
            _yearGroups = null;
        }
    }
}