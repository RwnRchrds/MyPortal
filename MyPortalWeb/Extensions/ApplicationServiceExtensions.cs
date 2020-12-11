using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Caching;
using MyPortal.Logic.FileProviders;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Services;
using IFileProvider = MyPortal.Logic.Interfaces.IFileProvider;

namespace MyPortalWeb.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            DataConnectionFactory.ConnectionString = config.GetConnectionString("MyPortal");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("MyPortal")));

            // MyPortal Database Connection
            services.AddScoped<IDbConnection>(connection =>
                new SqlConnection(config.GetConnectionString("MyPortal")));

            // MyPortal Database Repositories
            services.AddScoped<IAcademicYearRepository, AcademicYearRepository>();
            services.AddScoped<IAchievementOutcomeRepository, AchievementOutcomeRepository>();
            services.AddScoped<IAchievementRepository, AchievementRepository>();
            services.AddScoped<IAchievementTypeRepository, AchievementTypeRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IAddressPersonRepository, AddressPersonRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAspectRepository, AspectRepository>();
            services.AddScoped<IAspectTypeRepository, AspectTypeRepository>();
            services.AddScoped<IAttendanceCodeMeaningRepository, AttendanceCodeMeaningRepository>();
            services.AddScoped<IAttendanceCodeRepository, AttendanceCodeRepository>();
            services.AddScoped<IAttendanceMarkRepository, AttendanceMarkRepository>();
            services.AddScoped<IAttendancePeriodRepository, AttendancePeriodRepository>();
            services.AddScoped<IAttendanceWeekRepository, AttendanceWeekRepository>();
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
            services.AddScoped<IBehaviourOutcomeRepository, BehaviourOutcomeRepository>();
            services.AddScoped<IBehaviourStatusRepository, BehaviourStatusRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IBulletinRepository, BulletinRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<ICommentBankRepository, CommentBankRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommunicationLogRepository, CommunicationLogRepository>();
            services.AddScoped<ICommunicationTypeRepository, CommunicationTypeRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ICurriculumBandBlockAssignmentRepository, CurriculumBandBlockAssignmentRepository>();
            services.AddScoped<ICurriculumBandMembershipRepository, CurriculumBandMembershipRepository>();
            services.AddScoped<ICurriculumBandRepository, CurriculumBandRepository>();
            services.AddScoped<ICurriculumBlockRepository, CurriculumBlockRepository>();
            services.AddScoped<ICurriculumGroupMembershipRepository, CurriculumGroupMembershipRepository>();
            services.AddScoped<ICurriculumGroupRepository, CurriculumGroupRepository>();
            services.AddScoped<ICurriculumYearGroupRepository, CurriculumYearGroupRepository>();
            services.AddScoped<IDetentionRepository, DetentionRepository>();
            services.AddScoped<IDetentionTypeRepository, DetentionTypeRepository>();
            services.AddScoped<IDiaryEventAttendeeRepository, DiaryEventAttendeeRepository>();
            services.AddScoped<IDiaryEventRepository, DiaryEventRepository>();
            services.AddScoped<IDiaryEventTemplateRepository, DiaryEventTemplateRepository>();
            services.AddScoped<IDiaryEventTypeRepository, DiaryEventTypeRepository>();
            services.AddScoped<IDietaryRequirementRepository, DietaryRequirementRepository>();
            services.AddScoped<IDirectoryRepository, DirectoryRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddScoped<IEmailAddressRepository, EmailAddressRepository>();
            services.AddScoped<IEmailAddressTypeRepository, EmailAddressTypeRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IGiftedTalentedRepository, GiftedTalentedRepository>();
            services.AddScoped<IGovernanceTypeRepository, GovernanceTypeRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<IGradeSetRepository, GradeSetRepository>();
            services.AddScoped<IHomeworkRepository, HomeworkRepository>();
            services.AddScoped<IHomeworkSubmissionRepository, HomeworkSubmissionRepository>();
            services.AddScoped<IHouseRepository, HouseRepository>();
            services.AddScoped<IIncidentDetentionRepository, IncidentDetentionRepository>();
            services.AddScoped<IIncidentRepository, IncidentRepository>();
            services.AddScoped<IIncidentTypeRepository, IncidentTypeRepository>();
            services.AddScoped<IIntakeTypeRepository, IntakeTypeRepository>();
            services.AddScoped<ILessonPlanRepository, LessonPlanRepository>();
            services.AddScoped<ILessonPlanTemplateRepository, LessonPlanTemplateRepository>();
            services.AddScoped<ILocalAuthorityRepository, LocalAuthorityRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ILogNoteRepository, LogNoteRepository>();
            services.AddScoped<ILogNoteTypeRepository, LogNoteTypeRepository>();
            services.AddScoped<IMedicalConditionRepository, MedicalConditionRepository>();
            services.AddScoped<IMedicalEventRepository, MedicalEventRepository>();
            services.AddScoped<IObservationOutcomeRepository, ObservationOutcomeRepository>();
            services.AddScoped<IObservationRepository, ObservationRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPersonConditionRepository, PersonConditionRepository>();
            services.AddScoped<IPersonDietaryRequirementRepository, PersonDietaryRequirementRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPhaseRepository, PhaseRepository>();
            services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
            services.AddScoped<IPhoneNumberTypeRepository, PhoneNumberTypeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IRegGroupRepository, RegGroupRepository>();
            services.AddScoped<IRelationshipTypeRepository, RelationshipTypeRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<IResultSetRepository, ResultSetRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<ISchoolTypeRepository, SchoolTypeRepository>();
            services.AddScoped<ISenEventRepository, SenEventRepository>();
            services.AddScoped<ISenEventTypeRepository, SenEventTypeRepository>();
            services.AddScoped<ISenProvisionRepository, SenProvisionRepository>();
            services.AddScoped<ISenProvisionTypeRepository, SenProvisionTypeRepository>();
            services.AddScoped<ISenReviewRepository, SenReviewRepository>();
            services.AddScoped<ISenReviewTypeRepository, SenReviewTypeRepository>();
            services.AddScoped<ISenStatusRepository, SenStatusRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IStaffMemberRepository, StaffMemberRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentContactRepository, StudentContactRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ISubjectStaffMemberRepository, SubjectStaffMemberRepository>();
            services.AddScoped<ISubjectStaffMemberRoleRepository, SubjectStaffMemberRoleRepository>();
            services.AddScoped<ISystemAreaRepository, SystemAreaRepository>();
            services.AddScoped<ISystemSettingRepository, SystemSettingRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskTypeRepository, TaskTypeRepository>();
            services.AddScoped<ITrainingCertificateRepository, TrainingCertificateRepository>();
            services.AddScoped<ITrainingCertificateStatusRepository, TrainingCertificateStatusRepository>();
            services.AddScoped<ITrainingCourseRepository, TrainingCourseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IYearGroupRepository, YearGroupRepository>();

            // MyPortal Business Services
            services.AddScoped<IAcademicYearService, AcademicYearService>();
            services.AddScoped<IAchievementService, AchievementService>();
            //services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IAttendanceMarkService, AttendanceMarkService>();
            services.AddScoped<IAttendanceWeekService, AttendanceWeekService>();
            //services.AddScoped<IBillService, BillService>();
            services.AddScoped<IDirectoryService, DirectoryService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IHouseService, HouseService>();
            services.AddScoped<IIncidentService, IncidentService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ILogNoteService, LogNoteService>();
            services.AddScoped<IPeriodService, AttendancePeriodService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IRegGroupService, RegGroupService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IStaffMemberService, StaffMemberService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISystemSettingService, SystemSettingService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IYearGroupService, YearGroupService>();

            // MyPortal File Provider
            services.AddScoped<IFileProvider, LocalFileProvider>();

            // MyPortal Cache
            services.AddScoped<IRolePermissionsCache, RolePermissionsCache>();

            return services;
        }
    }
}
