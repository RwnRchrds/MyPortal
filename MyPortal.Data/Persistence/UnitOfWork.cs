using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;
using MyPortal.Data.Repositories;

namespace MyPortal.Data.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyPortalDbContext _context;

        public UnitOfWork()
        {
            _context = new MyPortalDbContext();
            Aspects = new AspectRepository(_context);
            AspectTypes = new AspectTypeRepository(_context);
            Grades = new GradeRepository(_context);
            GradeSets = new GradeSetRepository(_context);
            Results = new ResultRepository(_context);
            ResultSets = new ResultSetRepository(_context);
            AttendanceCodes = new AttendanceCodeRepository(_context);
            AttendanceCodeMeanings = new AttendanceCodeMeaningRepository(_context);
            AttendanceMarks = new AttendanceMarkRepository(_context);
            Periods = new PeriodRepository(_context);
            AttendanceWeeks = new AttendanceWeekRepository(_context);
            Achievements = new AchievementRepository(_context);
            AchievementTypes = new AchievementTypeRepository(_context);
            Incidents = new IncidentRepository(_context);
            IncidentTypes = new IncidentTypeRepository(_context);
            AddressPersons = new AddressPersonRepository(_context);
            Addresses = new AddressRepository(_context);
            EmailAddresses = new EmailAddressRepository(_context);
            EmailAddressTypes = new EmailAddressTypeRepository(_context);
            CommunicationLogs = new CommunicationLogRepository(_context);
            PhoneNumbers = new PhoneNumberRepository(_context);
            PhoneNumberTypes = new PhoneNumberTypeRepository(_context);
            CommunicationTypes = new CommunicationTypeRepository(_context);
            Contacts = new ContactRepository(_context);
            RelationshipTypes = new RelationshipTypeRepository(_context);
            AcademicYears = new AcademicYearRepository(_context);
            Classes = new ClassRepository(_context);
            Enrolments = new EnrolmentRepository(_context);
            LessonPlans = new LessonPlanRepository(_context);
            LessonPlanTemplates = new LessonPlanTemplateRepository(_context);
            Sessions = new SessionRepository(_context);
            StudyTopics = new StudyTopicRepository(_context);
            Subjects = new SubjectRepository(_context);
            SubjectStaffMembers = new SubjectStaffMemberRepository(_context);
            SubjectStaffMemberRoles = new SubjectStaffMemberRoleRepository(_context);
            Documents = new DocumentRepository(_context);
            DocumentTypes = new DocumentTypeRepository(_context);
            BasketItems = new BasketItemRepository(_context);
            Products = new ProductRepository(_context);
            ProductTypes = new ProductTypeRepository(_context);
            Sales = new SaleRepository(_context);
            Conditions = new ConditionRepository(_context);
            DietaryRequirements = new DietaryRequirementRepository(_context);
            MedicalEvent = new MedicalEventRepository(_context);
            PersonConditions = new PersonConditionRepository(_context);
            PersonDietaryRequirements = new PersonDietaryRequirementRepository(_context);
            Houses = new HouseRepository(_context);
            RegGroups = new RegGroupRepository(_context);
            YearGroups = new YearGroupRepository(_context);
            PersonAttachments = new PersonAttachmentRepository(_context);
            Observations = new ObservationRepository(_context);
            ObservationOutcomes = new ObservationOutcomeRepository(_context);
            TrainingCertificates = new TrainingCertificateRepository(_context);
            TrainingCertificateStatus = new TrainingCertificateStatusRepository(_context);
            TrainingCourses = new TrainingCourseRepository(_context);
            People = new PersonRepository(_context);
            CommentBanks = new CommentBankRepository(_context);
            Comments = new CommentRepository(_context);
            ProfileLogNotes = new ProfileLogNoteRepository(_context);
            ProfileLogNoteTypes = new ProfileLogNoteTypeRepository(_context);
            GovernanceTypes = new GovernanceTypeRepository(_context);
            IntakeTypes = new IntakeTypeRepository(_context);
            Locations = new LocationRepository(_context);
            Phases = new PhaseRepository(_context);
            SchoolTypes = new SchoolTypeRepository(_context);
            SenEvents = new SenEventRepository(_context);
            SenEventTypes = new SenEventTypeRepository(_context);
            GiftedTalented = new GiftedTalentedRepository(_context);
            SenProvisions = new SenProvisionRepository(_context);
            SenProvisionTypes = new SenProvisionTypeRepository(_context);
            SenReviewTypes = new SenReviewTypeRepository(_context);
            SenStatus = new SenStatusRepository(_context);
            StaffMembers = new StaffMemberRepository(_context);
            StudentContacts = new StudentContactRepository(_context);
            Students = new StudentRepository(_context);
            SystemAreas = new SystemAreaRepository(_context);
            Bulletins = new BulletinRepository(_context);
            Reports = new ReportRepository(_context);
            Schools = new SchoolRepository(_context);
            Detentions = new DetentionRepository(_context);
            DetentionTypes = new DetentionTypeRepository(_context);
            DiaryEvents = new DiaryEventRepository(_context);
            IncidentDetentions = new IncidentDetentionRepository(_context);
            DetentionAttendanceStatus = new DetentionAttendanceStatusRepository(_context);
        }

        public UnitOfWork(MyPortalDbContext context)
        {
            _context = context;
            Aspects = new AspectRepository(_context);
            AspectTypes = new AspectTypeRepository(_context);
            Grades = new GradeRepository(_context);
            GradeSets = new GradeSetRepository(_context);
            Results = new ResultRepository(_context);
            ResultSets = new ResultSetRepository(_context);
            AttendanceCodes = new AttendanceCodeRepository(_context);
            AttendanceCodeMeanings = new AttendanceCodeMeaningRepository(_context);
            AttendanceMarks = new AttendanceMarkRepository(_context);
            Periods = new PeriodRepository(_context);
            AttendanceWeeks = new AttendanceWeekRepository(_context);
            Achievements = new AchievementRepository(_context);
            AchievementTypes = new AchievementTypeRepository(_context);
            Incidents = new IncidentRepository(_context);
            IncidentTypes = new IncidentTypeRepository(_context);
            AddressPersons = new AddressPersonRepository(_context);
            Addresses = new AddressRepository(_context);
            EmailAddresses = new EmailAddressRepository(_context);
            EmailAddressTypes = new EmailAddressTypeRepository(_context);
            CommunicationLogs = new CommunicationLogRepository(_context);
            PhoneNumbers = new PhoneNumberRepository(_context);
            PhoneNumberTypes = new PhoneNumberTypeRepository(_context);
            CommunicationTypes = new CommunicationTypeRepository(_context);
            Contacts = new ContactRepository(_context);
            RelationshipTypes = new RelationshipTypeRepository(_context);
            AcademicYears = new AcademicYearRepository(_context);
            Classes = new ClassRepository(_context);
            Enrolments = new EnrolmentRepository(_context);
            LessonPlans = new LessonPlanRepository(_context);
            LessonPlanTemplates = new LessonPlanTemplateRepository(_context);
            Sessions = new SessionRepository(_context);
            StudyTopics = new StudyTopicRepository(_context);
            Subjects = new SubjectRepository(_context);
            SubjectStaffMembers = new SubjectStaffMemberRepository(_context);
            SubjectStaffMemberRoles = new SubjectStaffMemberRoleRepository(_context);
            Documents = new DocumentRepository(_context);
            DocumentTypes = new DocumentTypeRepository(_context);
            BasketItems = new BasketItemRepository(_context);
            Products = new ProductRepository(_context);
            ProductTypes = new ProductTypeRepository(_context);
            Sales = new SaleRepository(_context);
            Conditions = new ConditionRepository(_context);
            DietaryRequirements = new DietaryRequirementRepository(_context);
            MedicalEvent = new MedicalEventRepository(_context);
            PersonConditions = new PersonConditionRepository(_context);
            PersonDietaryRequirements = new PersonDietaryRequirementRepository(_context);
            Houses = new HouseRepository(_context);
            RegGroups = new RegGroupRepository(_context);
            YearGroups = new YearGroupRepository(_context);
            PersonAttachments = new PersonAttachmentRepository(_context);
            Observations = new ObservationRepository(_context);
            ObservationOutcomes = new ObservationOutcomeRepository(_context);
            TrainingCertificates = new TrainingCertificateRepository(_context);
            TrainingCertificateStatus = new TrainingCertificateStatusRepository(_context);
            TrainingCourses = new TrainingCourseRepository(_context);
            People = new PersonRepository(_context);
            CommentBanks = new CommentBankRepository(_context);
            Comments = new CommentRepository(_context);
            ProfileLogNotes = new ProfileLogNoteRepository(_context);
            ProfileLogNoteTypes = new ProfileLogNoteTypeRepository(_context);
            GovernanceTypes = new GovernanceTypeRepository(_context);
            IntakeTypes = new IntakeTypeRepository(_context);
            Locations = new LocationRepository(_context);
            Phases = new PhaseRepository(_context);
            SchoolTypes = new SchoolTypeRepository(_context);
            SenEvents = new SenEventRepository(_context);
            SenEventTypes = new SenEventTypeRepository(_context);
            GiftedTalented = new GiftedTalentedRepository(_context);
            SenProvisions = new SenProvisionRepository(_context);
            SenProvisionTypes = new SenProvisionTypeRepository(_context);
            SenReviewTypes = new SenReviewTypeRepository(_context);
            SenStatus = new SenStatusRepository(_context);
            StaffMembers = new StaffMemberRepository(_context);
            StudentContacts = new StudentContactRepository(_context);
            Students = new StudentRepository(_context);
            SystemAreas = new SystemAreaRepository(_context);
            Bulletins = new BulletinRepository(_context);
            Reports = new ReportRepository(_context);
            Schools = new SchoolRepository(_context);
            Detentions = new DetentionRepository(_context);
            DetentionTypes = new DetentionTypeRepository(_context);
            DiaryEvents = new DiaryEventRepository(_context);
            IncidentDetentions = new IncidentDetentionRepository(_context);
            DetentionAttendanceStatus = new DetentionAttendanceStatusRepository(_context);
        }

        public IAspectRepository Aspects { get; }
        public IAspectTypeRepository AspectTypes { get; }
        public IGradeRepository Grades { get; }
        public IGradeSetRepository GradeSets { get; }
        public IResultRepository Results { get; }
        public IResultSetRepository ResultSets { get; }
        public IAttendanceCodeRepository AttendanceCodes { get; }
        public IAttendanceCodeMeaningRepository AttendanceCodeMeanings { get; }
        public IAttendanceMarkRepository AttendanceMarks { get; }
        public IPeriodRepository Periods { get; }
        public IAttendanceWeekRepository AttendanceWeeks { get; }
        public IAchievementRepository Achievements { get; }
        public IAchievementTypeRepository AchievementTypes { get; }
        public IIncidentRepository Incidents { get; }
        public IIncidentTypeRepository IncidentTypes { get; }
        public IAddressPersonRepository AddressPersons { get; }
        public IAddressRepository Addresses { get; }
        public IEmailAddressRepository EmailAddresses { get; }
        public IEmailAddressTypeRepository EmailAddressTypes { get; }
        public ICommunicationLogRepository CommunicationLogs { get; }
        public IPhoneNumberRepository PhoneNumbers { get; }
        public IPhoneNumberTypeRepository PhoneNumberTypes { get; }
        public ICommunicationTypeRepository CommunicationTypes { get; }
        public IContactRepository Contacts { get; }
        public IRelationshipTypeRepository RelationshipTypes { get; }
        public IAcademicYearRepository AcademicYears { get; }
        public IClassRepository Classes { get; }
        public IEnrolmentRepository Enrolments { get; }
        public ILessonPlanRepository LessonPlans { get; }
        public ILessonPlanTemplateRepository LessonPlanTemplates { get; }
        public ISessionRepository Sessions { get; }
        public IStudyTopicRepository StudyTopics { get; }
        public ISubjectRepository Subjects { get; }
        public ISubjectStaffMemberRepository SubjectStaffMembers { get; }
        public ISubjectStaffMemberRoleRepository SubjectStaffMemberRoles { get; }
        public IDocumentRepository Documents { get; }
        public IDocumentTypeRepository DocumentTypes { get; }
        public IBasketItemRepository BasketItems { get; }
        public IProductRepository Products { get; }
        public IProductTypeRepository ProductTypes { get; }
        public ISaleRepository Sales { get; }
        public IConditionRepository Conditions { get; }
        public IDietaryRequirementRepository DietaryRequirements { get; }
        public IMedicalEventRepository MedicalEvent { get; }
        public IPersonConditionRepository PersonConditions { get; }
        public IPersonDietaryRequirementRepository PersonDietaryRequirements { get; }
        public IHouseRepository Houses { get; }
        public IRegGroupRepository RegGroups { get; }
        public IYearGroupRepository YearGroups { get; }
        public IPersonAttachmentRepository PersonAttachments { get; }
        public IObservationRepository Observations { get; }
        public IObservationOutcomeRepository ObservationOutcomes { get; }
        public ITrainingCertificateRepository TrainingCertificates { get; }
        public ITrainingCertificateStatusRepository TrainingCertificateStatus { get; }
        public ITrainingCourseRepository TrainingCourses { get; }
        public IPersonRepository People { get; }
        public ICommentBankRepository CommentBanks { get; }
        public ICommentRepository Comments { get; }
        public IProfileLogNoteRepository ProfileLogNotes { get; }
        public IProfileLogNoteTypeRepository ProfileLogNoteTypes { get; }
        public IGovernanceTypeRepository GovernanceTypes { get; }
        public IIntakeTypeRepository IntakeTypes { get; }
        public ILocationRepository Locations { get; }
        public IPhaseRepository Phases { get; }
        public ISchoolTypeRepository SchoolTypes { get; }
        public ISenEventRepository SenEvents { get; }
        public ISenEventTypeRepository SenEventTypes { get; }
        public IGiftedTalentedRepository GiftedTalented { get; }
        public ISenProvisionRepository SenProvisions { get; }
        public ISenProvisionTypeRepository SenProvisionTypes { get; }
        public ISenReviewTypeRepository SenReviewTypes { get; }
        public ISenStatusRepository SenStatus { get; }
        public IStaffMemberRepository StaffMembers { get; }
        public IStudentContactRepository StudentContacts { get; }
        public IStudentRepository Students { get; }
        public ISystemAreaRepository SystemAreas { get; }
        public IBulletinRepository Bulletins { get; }
        public IReportRepository Reports { get; }
        public ISchoolRepository Schools { get; }
        public IDetentionRepository Detentions { get; }
        public IDetentionTypeRepository DetentionTypes { get; }
        public IDiaryEventRepository DiaryEvents { get; }
        public IIncidentDetentionRepository IncidentDetentions { get; }
        public IDetentionAttendanceStatusRepository DetentionAttendanceStatus { get; }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}