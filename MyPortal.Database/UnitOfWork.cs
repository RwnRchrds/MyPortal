using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;

namespace MyPortal.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private IEnumerable<IRepository> _repositories;

        public IAcademicTermRepository AcademicTerms { get; }
        public IAcademicYearRepository AcademicYears { get; }
        public IAccountTransactionRepository AccountTransactions { get; }
        public IAchievementOutcomeRepository AchievementOutcomes { get; }
        public IAchievementRepository Achievements { get; }
        public IAchievementTypeRepository AchievementTypes { get; }
        public IActivityEventRepository ActivityEvents { get; }
        public IActivityRepository Activities { get; }
        public IAddressPersonRepository AddressPersons { get; }
        public IAddressRepository Addresses { get; }
        public IAspectRepository Aspects { get; }
        public IAspectTypeRepository AspectTypes { get; }
        public IAttendanceCodeMeaningRepository AttendanceCodeMeanings { get; }
        public IAttendanceCodeRepository AttendanceCodes { get; }
        public IAttendanceMarkRepository AttendanceMarks { get; }
        public IAttendancePeriodRepository AttendancePeriods { get; }
        public IAttendanceWeekRepository AttendanceWeeks { get; }
        public IBasketItemRepository BasketItems { get; }
        public IBehaviourOutcomeRepository BehaviourOutcomes { get; }
        public IBehaviourStatusRepository BehaviourStatus { get; }
        public IBillRepository Bills { get; }
        public IBulletinRepository Bulletins { get; }
        public IChargeDiscountRepository ChargeDiscounts { get; }
        public IChargeRepository Charges { get; }
        public IClassRepository Classes { get; }
        public ICommentBankRepository CommentBanks { get; }
        public ICommentRepository Comments { get; }
        public ICommunicationLogRepository CommunicationLogs { get; }
        public ICommunicationTypeRepository CommunicationTypes { get; }
        public IContactRepository Contacts { get; }
        public ICurriculumBandBlockAssignmentRepository CurriculumBandBlockAssignments { get; }
        public ICurriculumBandMembershipRepository CurriculumBandMemberships { get; }
        public ICurriculumBandRepository CurriculumBands { get; }
        public ICurriculumBlockRepository CurriculumBlocks { get; }
        public ICurriculumGroupMembershipRepository CurriculumGroupMemberships { get; }
        public ICurriculumGroupRepository CurriculumGroups { get; }
        public ICurriculumYearGroupRepository CurriculumYearGroups { get; }
        public IDetentionRepository Detentions { get; }
        public IDetentionTypeRepository DetentionTypes { get; }
        public IDiaryEventAttendeeRepository DiaryEventAttendees { get; }
        public IDiaryEventRepository DiaryEvents { get; }
        public IDiaryEventTemplateRepository DiaryEventTemplates { get; }
        public IDiaryEventTypeRepository DiaryEventTypes { get; }
        public IDietaryRequirementRepository DietaryRequirements { get; }
        public IDirectoryRepository Directories { get; }
        public IDocumentRepository Documents { get; }
        public IDocumentTypeRepository DocumentTypes { get; }
        public IEmailAddressRepository EmailAddresses { get; }
        public IEmailAddressTypeRepository EmailAddressTypes { get; }
        public IExclusionRepository Exclusions { get; }
        public IFileRepository Files { get; }
        public IGiftedTalentedRepository GiftedTalented { get; }
        public IGovernanceTypeRepository GovernanceTypes { get; }
        public IGradeRepository Grades { get; }
        public IGradeSetRepository GradeSets { get; }
        public IHomeworkItemRepository HomeworkItems { get; }
        public IHomeworkSubmissionRepository HomeworkSubmissions { get; }
        public IHouseRepository Houses { get; }
        public IIncidentDetentionRepository IncidentDetentions { get; }
        public IIncidentRepository Incidents { get; }
        public IIncidentTypeRepository IncidentTypes { get; }
        public IIntakeTypeRepository IntakeTypes { get; }
        public ILessonPlanRepository LessonPlans { get; }
        public ILessonPlanTemplateRepository LessonPlanTemplates { get; }
        public ILocalAuthorityRepository LocalAuthorities { get; }
        public ILocationRepository Locations { get; }
        public ILogNoteRepository LogNotes { get; }
        public ILogNoteTypeRepository LogNoteTypes { get; }
        public IMedicalConditionRepository MedicalConditions { get; }
        public IMedicalEventRepository MedicalEvents { get; }
        public IObservationOutcomeRepository ObservationOutcomes { get; }
        public IObservationRepository Observations { get; }
        public IPermissionRepository Permissions { get; }
        public IPersonConditionRepository PersonConditions { get; }
        public IPersonDietaryRequirementRepository PersonDietaryRequirements { get; }
        public IPersonRepository People { get; }
        public IRefreshTokenRepository RefreshTokens { get; }
        public IRegGroupRepository RegGroups { get; }
        public IRolePermissionRepository RolePermissions { get; }
        public ISchoolPhaseRepository SchoolPhases { get; }
        public ISchoolRepository Schools { get; }
        public ISchoolTypeRepository SchoolTypes { get; }
        public ISenEventRepository SenEvents { get; }
        public ISenEventTypeRepository SenEventTypes { get; }
        public ISenProvisionRepository SenProvisions { get; }
        public ISenProvisionTypeRepository SenProvisionTypes { get; }
        public ISenReviewRepository SenReviews { get; }
        public ISenReviewTypeRepository SenReviewTypes { get; }
        public ISenStatusRepository SenStatus { get; }
        public ISessionRepository Sessions { get; }
        public IStaffMemberRepository StaffMembers { get; }
        public IStudentChargeRepository StudentCharges { get; }
        public IStudentContactRelationshipRepository StudentContactRelationships { get; }
        public IStudentDiscountRepository StudentDiscounts { get; }
        public IStudentRepository Students { get; }
        public IStudyTopicRepository StudyTopics { get; }
        public ISubjectCodeSetRepository SubjectCodeSets { get; }
        public ISubjectRepository Subjects { get; }
        public ISubjectStaffMemberRepository SubjectStaffMembers { get; }
        public ISubjectStaffMemberRoleRepository SubjectStaffMemberRoles { get; }
        public ISystemAreaRepository SystemAreas { get; }
        public ISystemSettingRepository SystemSettings { get; }
        public ITaskRepository Tasks { get; }
        public ITaskTypeRepository TaskTypes { get; }
        public ITrainingCertificateRepository TrainingCertificates { get; }
        public ITrainingCertificateStatusRepository TrainingCertificateStatus { get; }
        public ITrainingCourseRepository TrainingCourses { get; }
        public IUserRoleRepository UserRoles { get; }
        public IUserRepository Users { get; }
        public IYearGroupRepository YearGroups { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            var dbConnection = context.Database.GetDbConnection();
            var repositories = new List<IRepository>();
            repositories.Add(AcademicTerms = new AcademicTermRepository(context));
            repositories.Add(AcademicYears = new AcademicYearRepository(context));
            repositories.Add(AccountTransactions = new AccountTransactionRepository(context));
            repositories.Add(AchievementOutcomes = new AchievementOutcomeRepository(context));
            repositories.Add(Achievements = new AchievementRepository(context));
            repositories.Add(AchievementTypes = new AchievementTypeRepository(context));
            repositories.Add(Activities = new ActivityRepository(context));
            repositories.Add(ActivityEvents = new ActivityEventRepository(context));
            repositories.Add(Addresses = new AddressRepository(context));
            repositories.Add(AddressPersons = new AddressPersonRepository(context));
            repositories.Add(Aspects = new AspectRepository(context));
            repositories.Add(AspectTypes = new AspectTypeRepository(dbConnection));
            repositories.Add(AttendanceCodeMeanings = new AttendanceCodeMeaningRepository(dbConnection));
            repositories.Add(AttendanceCodes = new AttendanceCodeRepository(dbConnection));
            repositories.Add(AttendanceMarks = new AttendanceMarkRepository(context));
            repositories.Add(AttendancePeriods = new AttendancePeriodRepository(context));
            repositories.Add(AttendanceWeeks = new AttendanceWeekRepository(context));
            repositories.Add(BasketItems = new BasketItemRepository(context));
            repositories.Add(BehaviourOutcomes = new BehaviourOutcomeRepository(context));
            repositories.Add(BehaviourStatus = new BehaviourStatusRepository(dbConnection));
            repositories.Add(Bills = new BillRepository(context));
            repositories.Add(Bulletins = new BulletinRepository(context));
            repositories.Add(ChargeDiscounts = new ChargeDiscountRepository(context));
            repositories.Add(Charges = new ChargeRepository(context));
            repositories.Add(Classes = new ClassRepository(context));
            repositories.Add(CommentBanks = new CommentBankRepository(context));
            repositories.Add(Comments = new CommentRepository(context));
            repositories.Add(CommunicationLogs = new CommunicationLogRepository(context));
            repositories.Add(CommunicationTypes = new CommunicationTypeRepository(dbConnection));
            repositories.Add(Contacts = new ContactRepository(context));
            repositories.Add(CurriculumBandBlockAssignments = new CurriculumBandBlockAssignmentRepository(context));
            repositories.Add(CurriculumBandMemberships = new CurriculumBandMembershipRepository(context));
            repositories.Add(CurriculumBands = new CurriculumBandRepository(context));
            repositories.Add(CurriculumBlocks = new CurriculumBlockRepository(context));
            repositories.Add(CurriculumGroupMemberships = new CurriculumGroupMembershipRepository(context));
            repositories.Add(CurriculumGroups = new CurriculumGroupRepository(context));
            repositories.Add(CurriculumYearGroups = new CurriculumYearGroupRepository(dbConnection));
            repositories.Add(Detentions = new DetentionRepository(context));
            repositories.Add(DetentionTypes = new DetentionTypeRepository(context));
            repositories.Add(DiaryEventAttendees = new DiaryEventAttendeeRepository(context));
            repositories.Add(DiaryEvents = new DiaryEventRepository(context));
            repositories.Add(DiaryEventTemplates = new DiaryEventTemplateRepository(context));
            repositories.Add(DiaryEventTypes = new DiaryEventTypeRepository(context));
            repositories.Add(DietaryRequirements = new DietaryRequirementRepository(dbConnection));
            repositories.Add(Directories = new DirectoryRepository(context));
            repositories.Add(Documents = new DocumentRepository(context));
            repositories.Add(DocumentTypes = new DocumentTypeRepository(dbConnection));
            repositories.Add(EmailAddresses = new EmailAddressRepository(context));
            repositories.Add(EmailAddressTypes = new EmailAddressTypeRepository(dbConnection));
            repositories.Add(Exclusions = new ExclusionRepository(context));
            repositories.Add(Files = new FileRepository(context));
            repositories.Add(GiftedTalented = new GiftedTalentedRepository(context));
            repositories.Add(GovernanceTypes = new GovernanceTypeRepository(dbConnection));
            repositories.Add(Grades = new GradeRepository(context));
            repositories.Add(GradeSets = new GradeSetRepository(context));
            repositories.Add(HomeworkItems = new HomeworkItemRepository(context));
            repositories.Add(HomeworkSubmissions = new HomeworkSubmissionRepository(context));
            repositories.Add(Houses = new HouseRepository(context));
            repositories.Add(IncidentDetentions = new IncidentDetentionRepository(context));
            repositories.Add(Incidents = new IncidentRepository(context));
            repositories.Add(IncidentTypes = new IncidentTypeRepository(context));
            repositories.Add(IntakeTypes = new IntakeTypeRepository(dbConnection));
            repositories.Add(LessonPlans = new LessonPlanRepository(context));
            repositories.Add(LessonPlanTemplates = new LessonPlanTemplateRepository(context));
            repositories.Add(LocalAuthorities = new LocalAuthorityRepository(dbConnection));
            repositories.Add(Locations = new LocationRepository(context));
            repositories.Add(LogNotes = new LogNoteRepository(context));
            repositories.Add(LogNoteTypes = new LogNoteTypeRepository(dbConnection));
            repositories.Add(MedicalConditions = new MedicalConditionRepository(context));
            repositories.Add(MedicalEvents = new MedicalEventRepository(context));
            repositories.Add(ObservationOutcomes = new ObservationOutcomeRepository(dbConnection));
            repositories.Add(Observations = new ObservationRepository(context));
            repositories.Add(People = new PersonRepository(context));
            repositories.Add(Permissions = new PermissionRepository(dbConnection));
            repositories.Add(PersonConditions = new PersonConditionRepository(context));
            repositories.Add(PersonDietaryRequirements = new PersonDietaryRequirementRepository(context));
            repositories.Add(RefreshTokens = new RefreshTokenRepository(context));
            repositories.Add(RegGroups = new RegGroupRepository(context));
            repositories.Add(RolePermissions = new RolePermissionRepository(context));
            repositories.Add(SchoolPhases = new SchoolPhaseRepository(dbConnection));
            repositories.Add(Schools = new SchoolRepository(context));
            repositories.Add(SchoolTypes = new SchoolTypeRepository(dbConnection));
            repositories.Add(SenEvents = new SenEventRepository(context));
            repositories.Add(SenEventTypes = new SenEventTypeRepository(dbConnection));
            repositories.Add(SenProvisions = new SenProvisionRepository(context));
            repositories.Add(SenProvisionTypes = new SenProvisionTypeRepository(dbConnection));
            repositories.Add(SenReviews = new SenReviewRepository(context));
            repositories.Add(SenReviewTypes = new SenReviewTypeRepository(dbConnection));
            repositories.Add(SenStatus = new SenStatusRepository(dbConnection));
            repositories.Add(Sessions = new SessionRepository(context));
            repositories.Add(StaffMembers = new StaffMemberRepository(context));
            repositories.Add(StudentCharges = new StudentChargeRepository(context));
            repositories.Add(StudentContactRelationships = new StudentContactRelationshipRepository(context));
            repositories.Add(StudentDiscounts = new StudentDiscountRepository(context));
            repositories.Add(Students = new StudentRepository(context));
            repositories.Add(StudyTopics = new StudyTopicRepository(context));
            repositories.Add(SubjectCodeSets = new SubjectCodeSetRepository(dbConnection));
            repositories.Add(Subjects = new SubjectRepository(context));
            repositories.Add(SubjectStaffMemberRoles = new SubjectStaffMemberRoleRepository(context));
            repositories.Add(SubjectStaffMembers = new SubjectStaffMemberRepository(context));
            repositories.Add(SystemAreas = new SystemAreaRepository(dbConnection));
            repositories.Add(SystemSettings = new SystemSettingRepository(context));
            repositories.Add(Tasks = new TaskRepository(context));
            repositories.Add(TaskTypes = new TaskTypeRepository(context));
            repositories.Add(TrainingCertificates = new TrainingCertificateRepository(context));
            repositories.Add(TrainingCertificateStatus = new TrainingCertificateStatusRepository(context));
            repositories.Add(TrainingCourses = new TrainingCourseRepository(context));
            repositories.Add(UserRoles = new UserRoleRepository(context));
            repositories.Add(Users = new UserRepository(context));
            repositories.Add(YearGroups = new YearGroupRepository(context));

            _repositories = repositories.ToArray();
        }

        public void Dispose()
        {
            AcademicTerms?.Dispose();
            AcademicYears?.Dispose();
            AccountTransactions?.Dispose();
            AchievementOutcomes?.Dispose();
            Achievements?.Dispose();
            AchievementTypes?.Dispose();
            Activities?.Dispose();
            ActivityEvents?.Dispose();
            Addresses?.Dispose();
            AddressPersons?.Dispose();
            Aspects?.Dispose();
            AspectTypes?.Dispose();
            AttendanceCodeMeanings?.Dispose();
            AttendanceCodes?.Dispose();
            AttendanceMarks?.Dispose();
            AttendancePeriods?.Dispose();
            AttendanceWeeks?.Dispose();
            BasketItems?.Dispose();
            BehaviourOutcomes?.Dispose();
            BehaviourStatus?.Dispose();
            Bills?.Dispose();
            Bulletins?.Dispose();
            Classes?.Dispose();
            CommentBanks?.Dispose();
            Comments?.Dispose();
            CommunicationLogs?.Dispose();
            CommunicationTypes?.Dispose();
            Contacts?.Dispose();
            CurriculumBandBlockAssignments?.Dispose();
            CurriculumBandMemberships?.Dispose();
            CurriculumBands?.Dispose();
            CurriculumBlocks?.Dispose();
            CurriculumGroupMemberships?.Dispose();
            CurriculumGroups?.Dispose();
            CurriculumYearGroups?.Dispose();
            Detentions?.Dispose();
            DetentionTypes?.Dispose();
            DiaryEventAttendees?.Dispose();
            DiaryEvents?.Dispose();
            DiaryEventTemplates?.Dispose();
            DiaryEventTypes?.Dispose();
            DietaryRequirements?.Dispose();
            Directories?.Dispose();
            Documents?.Dispose();
            DocumentTypes?.Dispose();
            EmailAddresses?.Dispose();
            EmailAddressTypes?.Dispose();
            Exclusions?.Dispose();
            Files?.Dispose();
            GiftedTalented?.Dispose();
            GovernanceTypes?.Dispose();
            Grades?.Dispose();
            GradeSets?.Dispose();
            HomeworkItems?.Dispose();
            HomeworkSubmissions?.Dispose();
            Houses?.Dispose();
            IncidentDetentions?.Dispose();
            Incidents?.Dispose();
            IncidentTypes?.Dispose();
            IntakeTypes?.Dispose();
            LessonPlans?.Dispose();
            LessonPlanTemplates?.Dispose();
            LocalAuthorities?.Dispose();
            Locations?.Dispose();
            LogNotes?.Dispose();
            LogNoteTypes?.Dispose();
            MedicalConditions?.Dispose();
            MedicalEvents?.Dispose();
            ObservationOutcomes?.Dispose();
            Observations?.Dispose();
            People?.Dispose();
            Permissions?.Dispose();
            PersonConditions?.Dispose();
            PersonDietaryRequirements?.Dispose();
            RefreshTokens?.Dispose();
            RegGroups?.Dispose();
            RolePermissions?.Dispose();
            SchoolPhases?.Dispose();
            Schools?.Dispose();
            SchoolTypes?.Dispose();
            SenEvents?.Dispose();
            SenEventTypes?.Dispose();
            SenProvisions?.Dispose();
            SenProvisionTypes?.Dispose();
            SenReviews?.Dispose();
            SenReviewTypes?.Dispose();
            SenStatus?.Dispose();
            Sessions?.Dispose();
            StaffMembers?.Dispose();
            StudentCharges?.Dispose();
            StudentContactRelationships?.Dispose();
            StudentDiscounts?.Dispose();
            Students?.Dispose();
            StudyTopics?.Dispose();
            SubjectCodeSets?.Dispose();
            Subjects?.Dispose();
            SubjectStaffMemberRoles?.Dispose();
            SubjectStaffMembers?.Dispose();
            SystemAreas?.Dispose();
            SystemSettings?.Dispose();
            Tasks?.Dispose();
            TaskTypes?.Dispose();
            TrainingCertificates?.Dispose();
            TrainingCertificateStatus?.Dispose();
            TrainingCourses?.Dispose();
            UserRoles?.Dispose();
            Users?.Dispose();
            YearGroups?.Dispose();
        }

        public async Task SaveChanges()
        {
            foreach (var repository in _repositories.OfType<IWriteRepository>())
            {
                await repository.SaveChanges();
            }
        }
    }
}
