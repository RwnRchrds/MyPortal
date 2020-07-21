using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyPortal.Database.Constants;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Authorisation;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Services;

namespace MyPortalCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            DataConnectionFactory.ConnectionString = Configuration.GetConnectionString("MyPortal");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MyPortal")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddDefaultTokenProviders();

            services.Configure<CookieAuthenticationOptions>(options =>
            {
                options.AccessDeniedPath = "/AccessDenied";
            });

            services.AddControllersWithViews();
            services.AddMvc();
            services.AddRazorPages();

            services
                .AddTransient<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.UserType.Student,
                    p => p.RequireClaim(ClaimTypes.UserType, UserTypes.Student));
                options.AddPolicy(Policies.UserType.Staff, p => p.RequireClaim(ClaimTypes.UserType, UserTypes.Staff));
                options.AddPolicy(Policies.UserType.Parent, p => p.RequireClaim(ClaimTypes.UserType, UserTypes.Parent));

                // Get claim values for permission-based authorisation
                using (var context = DataConnectionFactory.CreateContext())
                {
                    var permissionsInDb = context.ApplicationPermissions.ToList();

                    Permissions.PopulateClaimValues();

                    foreach (var perm in permissionsInDb)
                    {
                        Permissions.ClaimValues[perm.Id] = perm.ClaimValue;
                    }

                    if (Permissions.ClaimValues.Any(x => x.Value == string.Empty))
                    {
                        throw new Exception(
                            "Some permissions have not been mapped correctly. Please contact your MyPortal support team.");
                    }
                }
            });

            // MyPortal database connection
            services.AddScoped<IDbConnection>(connection =>
                new SqlConnection(Configuration.GetConnectionString("MyPortal")));

            // MyPortal entity repositories (used by business services)
            services.AddScoped<IAcademicYearRepository, AcademicYearRepository>();
            services.AddScoped<IAchievementRepository, AchievementRepository>();
            services.AddScoped<IAchievementOutcomeRepository, AchievementOutcomeRepository>();
            services.AddScoped<IAchievementTypeRepository, AchievementTypeRepository>();
            services.AddScoped<IAddressPersonRepository, AddressPersonRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IApplicationPermissionRepository, ApplicationPermissionRepository>();
            services.AddScoped<IApplicationRolePermissionRepository, ApplicationRolePermissionRepository>();
            services.AddScoped<IAspectRepository, AspectRepository>();
            services.AddScoped<IAspectTypeRepository, AspectTypeRepository>();
            services.AddScoped<IAttendanceCodeMeaningRepository, AttendanceCodeMeaningRepository>();
            services.AddScoped<IAttendanceCodeRepository, AttendanceCodeRepository>();
            services.AddScoped<IAttendanceMarkRepository, AttendanceMarkRepository>();
            services.AddScoped<IAttendanceWeekRepository, AttendanceWeekRepository>();
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
            services.AddScoped<IBehaviourOutcomeRepository, BehaviourOutcomeRepository>();
            services.AddScoped<IBehaviourStatusRepository, BehaviourStatusRepository>();
            services.AddScoped<IBulletinRepository, BulletinRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<ICommentBankRepository, CommentBankRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommunicationLogRepository, CommunicationLogRepository>();
            services.AddScoped<ICommunicationTypeRepository, CommunicationTypeRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
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
            services.AddScoped<IEnrolmentRepository, EnrolmentRepository>();
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
            services.AddScoped<IPeriodRepository, PeriodRepository>();
            services.AddScoped<IPersonConditionRepository, PersonConditionRepository>();
            services.AddScoped<IPersonDietaryRequirementRepository, PersonDietaryRequirementRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPhaseRepository, PhaseRepository>();
            services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
            services.AddScoped<IPhoneNumberTypeRepository, PhoneNumberTypeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IRegGroupRepository, RegGroupRepository>();
            services.AddScoped<IRelationshipTypeRepository, RelationshipTypeRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
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
            services.AddScoped<IStudentContactRepository, StudentContactRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudyTopicRepository, StudyTopicRepository>();
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
            services.AddScoped<IYearGroupRepository, YearGroupRepository>();

            // MyPortal business services

            services.AddScoped<IGoogleHelper, GoogleHelper>();
                        
            services.AddScoped<IAcademicYearService, AcademicYearService>();
            services.AddScoped<IAchievementService, AchievementService>();
            services.AddScoped<IApplicationRolePermissionService, ApplicationRolePermissionService>();
            services.AddScoped<IApplicationRoleService, ApplicationRoleService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IAttendanceMarkService, AttendanceMarkService>();
            services.AddScoped<IAttendanceWeekService, AttendanceWeekService>();
            services.AddScoped<IDirectoryService, DirectoryService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IIncidentService, IncidentService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ILogNoteService, LogNoteService>();
            services.AddScoped<IPeriodService, PeriodService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IStaffMemberService, StaffMemberService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISystemSettingService, SystemSettingService>();
            services.AddScoped<ITaskService, TaskService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapAreaControllerRoute(
                    "Staff",
                    "Staff",
                    "Staff/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                    "Students",
                    "Students",
                    "Students/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                    "Parents",
                    "Parents",
                    "Parents/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
