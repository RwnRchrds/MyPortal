using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Repositories;

namespace MyPortal.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyPortalDbContext _context;

        public UnitOfWork(MyPortalDbContext context)
        {
            _context = context;
            AssessmentAspects = new AssessmentAspectRepository(context);
            AssessmentAspectTypes = new AssessmentAspectTypeRepository(context);
            AssessmentGrades = new AssessmentGradeRepository(context);
            AssessmentGradeSets = new AssessmentGradeSetRepository(context);
            AssessmentResults = new AssessmentResultRepository(context);
            AssessmentResultSets = new AssessmentResultSetRepository(context);
            AttendanceCodes = new AttendanceCodeRepository(context);
            AttendanceMarks = new AttendanceMarkRepository(context);
            AttendancePeriods = new AttendancePeriodRepository(context);
            AttendanceWeeks = new AttendanceWeekRepository(context);
            BehaviourAchievements = new BehaviourAchievementRepository(context);
            BehaviourAchievementTypes = new BehaviourAchievementTypeRepository(context);
            BehaviourIncidents = new BehaviourIncidentRepository(context);
            BehaviourIncidentTypes = new BehaviourIncidentTypeRepository(context);
            CommunicationAddresses = new CommunicationAddressRepository(context);
            CommunicationAddressPersons = new CommunicationAddressPersonRepository(context);
            CommunicationEmailAddresses = new CommunicationEmailAddressRepository(context);
            CommunicationLogs = new CommunicationLogRepository(context);
            CommunicationPhoneNumbers = new CommunicationPhoneNumberRepository(context);
            CommunicationPhoneNumberTypes = new CommunicationPhoneNumberTypeRepository(context);
            CommunicationTypes = new CommunicationTypeRepository(context);
            Contacts = new ContactRepository(context);
            CurriculumAcademicYears = new CurriculumAcademicYearRepository(context);
            CurriculumClasses = new CurriculumClassRepository(context);
            CurriculumEnrolments = new CurriculumEnrolmentRepository(context);
            CurriculumLessonPlans = new CurriculumLessonPlanRepository(context);
            CurriculumLessonPlanTemplates = new CurriculumLessonPlanTemplateRepository(context);
            CurriculumSessions = new CurriculumSessionRepository(context);
            CurriculumStudyTopics = new CurriculumStudyTopicRepository(context);
            CurriculumSubjects = new CurriculumSubjectRepository(context);
            Documents =  new DocumentRepository(context);
            DocumentTypes = new DocumentTypeRepository(context);
            FinanceBasketItems = new FinanceBasketItemRepository(context);
            FinanceProducts = new FinanceProductRepository(context);
            FinanceProductTypes = new FinanceProductTypeRepository(context);
            FinanceSales = new FinanceSaleRepository(context);
            MedicalConditions = new MedicalConditionRepository(context);
            MedicalDietaryRequirements =  new MedicalDietaryRequirementRepository(context);
            MedicalPersonConditions = new MedicalPersonConditionRepository(context);
            MedicalPersonDietaryRequirements = new MedicalPersonDietaryRequirementRepository(context);
            PastoralHouses = new PastoralHouseRepository(context);
            PastoralRegGroups = new PastoralRegGroupRepository(context);
            PastoralYearGroups = new PastoralYearGroupRepository(context);
            People = new PersonRepository(context);
            PersonDocuments = new PersonDocumentRepository(context);
            PersonnelObservations = new PersonnelObservationRepository(context);
            PersonnelTrainingCertificates = new PersonnelTrainingCertificateRepository(context);
            PersonnelTrainingCourses = new PersonnelTrainingCourseRepository(context);
            ProfileComments = new ProfileCommentRepository(context);
            ProfileCommentBanks = new ProfileCommentBankRepository(context);
            ProfileLogs = new ProfileLogRepository(context);
            ProfileLogTypes = new ProfileLogTypeRepository(context);
            RelationshipTypes = new RelationshipTypeRepository(context);
            SchoolGovernanceTypes = new SchoolGovernanceTypeRepository(context);
            SchoolIntakeTypes = new SchoolIntakeTypeRepository(context);
            SchoolLocations = new SchoolLocationRepository(context);
            SchoolPhases = new SchoolPhaseRepository(context);
            SchoolTypes = new SchoolTypeRepository(context);
            SenEventTypes = new SenEventTypeRepository(context);
            SenEvents = new SenEventRepository(context);
            SenGiftedTalented = new SenGiftedTalentedRepository(context);
            SenProvisionTypes = new SenProvisionTypeRepository(context);
            SenProvisions = new SenProvisionRepository(context);
            SenReviewTypes = new SenReviewTypeRepository(context);
            SenStatus = new SenStatusRepository(context);
            StaffMembers = new StaffMemberRepository(context);
            Students = new StudentRepository(context);
            SystemAreas = new SystemAreaRepository(context);
            SystemBulletins = new SystemBulletinRepository(context);
            SystemReports = new SystemReportRepository(context);
            SystemSchools = new SystemSchoolRepository(context);
        }

        public IAssessmentAspectRepository AssessmentAspects { get; private set; }
        public IAssessmentAspectTypeRepository AssessmentAspectTypes { get; private set; }
        public IAssessmentGradeRepository AssessmentGrades { get; private set; }
        public IAssessmentGradeSetRepository AssessmentGradeSets { get; private set; }
        public IAssessmentResultRepository AssessmentResults { get; private set; }
        public IAssessmentResultSetRepository AssessmentResultSets { get; private set; }
        public IAttendanceCodeRepository AttendanceCodes { get; private set; }
        public IAttendanceMarkRepository AttendanceMarks { get; private set; }
        public IAttendancePeriodRepository AttendancePeriods { get; private set; }
        public IAttendanceWeekRepository AttendanceWeeks { get; private set; }
        public IBehaviourAchievementRepository BehaviourAchievements { get; private set; }
        public IBehaviourAchievementTypeRepository BehaviourAchievementTypes { get; private set; }
        public IBehaviourIncidentRepository BehaviourIncidents { get; private set; }
        public IBehaviourIncidentTypeRepository BehaviourIncidentTypes { get; private set; }
        public ICommunicationAddressRepository CommunicationAddresses { get; private set; }
        public ICommunicationAddressPersonRepository CommunicationAddressPersons { get; private set; }
        public ICommunicationEmailAddressRepository CommunicationEmailAddresses { get; private set; }
        public ICommunicationLogRepository CommunicationLogs { get; private set; }
        public ICommunicationPhoneNumberRepository CommunicationPhoneNumbers { get; private set; }
        public ICommunicationPhoneNumberTypeRepository CommunicationPhoneNumberTypes { get; private set; }
        public ICommunicationTypeRepository CommunicationTypes { get; private set; }
        public IContactRepository Contacts { get; private set; }
        public ICurriculumAcademicYearRepository CurriculumAcademicYears { get; private set; }
        public ICurriculumClassRepository CurriculumClasses { get; private set; }
        public ICurriculumEnrolmentRepository CurriculumEnrolments { get; private set; }
        public ICurriculumLessonPlanRepository CurriculumLessonPlans { get; private set; }
        public ICurriculumLessonPlanTemplateRepository CurriculumLessonPlanTemplates { get; private set; }
        public ICurriculumSessionRepository CurriculumSessions { get; private set; }
        public ICurriculumStudyTopicRepository CurriculumStudyTopics { get; private set; }
        public ICurriculumSubjectRepository CurriculumSubjects { get; private set; }
        public IDocumentRepository Documents { get; private set; }
        public IDocumentTypeRepository DocumentTypes { get; private set; }
        public IFinanceBasketItemRepository FinanceBasketItems { get; private set; }
        public IFinanceProductRepository FinanceProducts { get; private set; }
        public IFinanceProductTypeRepository FinanceProductTypes { get; private set; }
        public IFinanceSaleRepository FinanceSales { get; private set; }
        public IMedicalConditionRepository MedicalConditions { get; private set; }
        public IMedicalDietaryRequirementRepository MedicalDietaryRequirements { get; private set; }
        public IMedicalEventRepository MedicalEvent { get; private set; }
        public IMedicalPersonConditionRepository MedicalPersonConditions { get; private set; }
        public IMedicalPersonDietaryRequirementRepository MedicalPersonDietaryRequirements { get; private set; }
        public IPastoralHouseRepository PastoralHouses { get; private set; }
        public IPastoralRegGroupRepository PastoralRegGroups { get; private set; }
        public IPastoralYearGroupRepository PastoralYearGroups { get; private set; }
        public IPersonRepository People { get; private set; }
        public IPersonDocumentRepository PersonDocuments { get; private set; }
        public IPersonnelObservationRepository PersonnelObservations { get; private set; }
        public IPersonnelTrainingCertificateRepository PersonnelTrainingCertificates { get; private set; }
        public IPersonnelTrainingCourseRepository PersonnelTrainingCourses { get; private set; }
        public IProfileCommentRepository ProfileComments { get; private set; }
        public IProfileCommentBankRepository ProfileCommentBanks { get; private set; }
        public IProfileLogRepository ProfileLogs { get; private set; }
        public IProfileLogTypeRepository ProfileLogTypes { get; private set; }
        public IRelationshipTypeRepository RelationshipTypes { get; private set; }
        public ISchoolGovernanceTypeRepository SchoolGovernanceTypes { get; private set; }
        public ISchoolIntakeTypeRepository SchoolIntakeTypes { get; private set; }
        public ISchoolLocationRepository SchoolLocations { get; private set; }
        public ISchoolPhaseRepository SchoolPhases { get; private set; }
        public ISchoolTypeRepository SchoolTypes { get; private set; }
        public ISenEventTypeRepository SenEventTypes { get; private set; }
        public ISenEventRepository SenEvents { get; private set; }
        public ISenGiftedTalentedRepository SenGiftedTalented { get; private set; }
        public ISenProvisionTypeRepository SenProvisionTypes { get; private set; }
        public ISenProvisionRepository SenProvisions { get; private set; }
        public ISenReviewTypeRepository SenReviewTypes { get; private set; }
        public ISenStatusRepository SenStatus { get; private set; }
        public IStaffMemberRepository StaffMembers { get; private set; }
        public IStudentRepository Students { get; private set; }
        public ISystemAreaRepository SystemAreas { get; private set; }
        public ISystemBulletinRepository SystemBulletins { get; private set; }
        public ISystemReportRepository SystemReports { get; private set; }
        public ISystemSchoolRepository SystemSchools { get; private set; }

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