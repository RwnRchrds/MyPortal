using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Authorisation;
using MyPortal.Logic.Dictionaries;
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("LiveConnection")));


            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                    {
                        options.SignIn.RequireConfirmedAccount = false;
                    })
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyDictionary.UserType.Student, p => p.RequireClaim(ClaimTypeDictionary.UserType, UserTypeDictionary.Student));
                options.AddPolicy(PolicyDictionary.UserType.Staff, p => p.RequireClaim(ClaimTypeDictionary.UserType, UserTypeDictionary.Staff));
                options.AddPolicy(PolicyDictionary.UserType.Parent, p => p.RequireClaim(ClaimTypeDictionary.UserType, UserTypeDictionary.Parent));
            });

            // MyPortal database connection
            services.AddTransient<IDbConnection>(connection =>
                new SqlConnection(Configuration.GetConnectionString("LiveConnection")));

            // MyPortal entity repositories (used by business services)
            services.AddTransient<IAcademicYearRepository, AcademicYearRepository>();
            services.AddTransient<IAchievementRepository, AchievementRepository>();
            services.AddTransient<IAchievementTypeRepository, AchievementTypeRepository>();
            services.AddTransient<IAddressPersonRepository, AddressPersonRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddTransient<IAspectRepository, AspectRepository>();
            services.AddTransient<IAspectTypeRepository, AspectTypeRepository>();
            services.AddTransient<IAttendanceCodeMeaningRepository, AttendanceCodeMeaningRepository>();
            services.AddTransient<IAttendanceCodeRepository, AttendanceCodeRepository>();
            services.AddTransient<IAttendanceMarkRepository, AttendanceMarkRepository>();
            services.AddTransient<IAttendanceWeekRepository, AttendanceWeekRepository>();
            services.AddTransient<IBasketItemRepository, BasketItemRepository>();
            services.AddTransient<IBulletinRepository, BulletinRepository>();
            services.AddTransient<IClassRepository, ClassRepository>();
            services.AddTransient<ICommentBankRepository, CommentBankRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICommunicationLogRepository, CommunicationLogRepository>();
            services.AddTransient<ICommunicationTypeRepository, CommunicationTypeRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IDetentionRepository, DetentionRepository>();
            services.AddTransient<IDetentionTypeRepository, DetentionTypeRepository>();
            services.AddTransient<IDiaryEventAttendeeRepository, DiaryEventAttendeeRepository>();
            services.AddTransient<IDiaryEventRepository, DiaryEventRepository>();
            services.AddTransient<IDiaryEventTemplateRepository, DiaryEventTemplateRepository>();
            services.AddTransient<IDiaryEventTypeRepository, DiaryEventTypeRepository>();
            services.AddTransient<IDietaryRequirementRepository, DietaryRequirementRepository>();
            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddTransient<IEmailAddressRepository, EmailAddressRepository>();
            services.AddTransient<IEmailAddressTypeRepository, EmailAddressTypeRepository>();
            services.AddTransient<IEnrolmentRepository, EnrolmentRepository>();
            services.AddTransient<IGiftedTalentedRepository, GiftedTalentedRepository>();
            services.AddTransient<IGovernanceTypeRepository, GovernanceTypeRepository>();
            services.AddTransient<IGradeRepository, GradeRepository>();
            services.AddTransient<IGradeSetRepository, GradeSetRepository>();
            services.AddTransient<IHomeworkAttachmentRepository, HomeworkAttachmentRepository>();
            services.AddTransient<IHomeworkRepository, HomeworkRepository>();
            services.AddTransient<IHomeworkSubmissionRepository, HomeworkSubmissionRepository>();
            services.AddTransient<IHouseRepository, HouseRepository>();
            services.AddTransient<IIncidentDetentionRepository, IncidentDetentionRepository>();
            services.AddTransient<IIncidentRepository, IncidentRepository>();
            services.AddTransient<IIncidentTypeRepository, IncidentTypeRepository>();
            services.AddTransient<IIntakeTypeRepository, IntakeTypeRepository>();
            services.AddTransient<ILessonPlanRepository, LessonPlanRepository>();
            services.AddTransient<ILessonPlanTemplateRepository, LessonPlanTemplateRepository>();
            services.AddTransient<ILocalAuthorityRepository, LocalAuthorityRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IMedicalConditionRepository, MedicalConditionRepository>();
            services.AddTransient<IMedicalEventRepository, MedicalEventRepository>();
            services.AddTransient<IObservationOutcomeRepository, ObservationOutcomeRepository>();
            services.AddTransient<IObservationRepository, ObservationRepository>();
            services.AddTransient<IPeriodRepository, PeriodRepository>();
            services.AddTransient<IPersonAttachmentRepository, PersonAttachmentRepository>();
            services.AddTransient<IPersonConditionRepository, PersonConditionRepository>();
            services.AddTransient<IPersonDietaryRequirementRepository, PersonDietaryRequirementRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IPhaseRepository, PhaseRepository>();
            services.AddTransient<IPhoneNumberRepository, PhoneNumberRepository>();
            services.AddTransient<IPhoneNumberTypeRepository, PhoneNumberTypeRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
            services.AddTransient<IProfileLogNoteRepository, ProfileLogNoteRepository>();
            services.AddTransient<IProfileLogNoteTypeRepository, ProfileLogNoteTypeRepository>();
            services.AddTransient<IRegGroupRepository, RegGroupRepository>();
            services.AddTransient<IRelationshipTypeRepository, RelationshipTypeRepository>();
            services.AddTransient<IReportRepository, ReportRepository>();
            services.AddTransient<IResultRepository, ResultRepository>();
            services.AddTransient<ISaleRepository, SaleRepository>();
            services.AddTransient<ISchoolRepository, SchoolRepository>();
            services.AddTransient<ISchoolTypeRepository, SchoolTypeRepository>();
            services.AddTransient<ISenEventRepository, SenEventRepository>();
            services.AddTransient<ISenEventTypeRepository, SenEventTypeRepository>();
            services.AddTransient<ISenProvisionRepository, SenProvisionRepository>();
            services.AddTransient<ISenProvisionTypeRepository, SenProvisionTypeRepository>();
            services.AddTransient<ISenReviewRepository, SenReviewRepository>();
            services.AddTransient<ISenReviewTypeRepository, SenReviewTypeRepository>();
            services.AddTransient<ISenStatusRepository, SenStatusRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddTransient<IStaffMemberRepository, StaffMemberRepository>();
            services.AddTransient<IStudentContactRepository, StudentContactRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudyTopicRepository, StudyTopicRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<ISubjectStaffMemberRepository, SubjectStaffMemberRepository>();
            services.AddTransient<ISubjectStaffMemberRoleRepository, SubjectStaffMemberRoleRepository>();
            services.AddTransient<ISystemAreaRepository, SystemAreaRepository>();
            services.AddTransient<ISystemResourceRepository, SystemResourceRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<ITrainingCertificateRepository, TrainingCertificateRepository>();
            services.AddTransient<ITrainingCertificateStatusRepository, TrainingCertificateStatusRepository>();
            services.AddTransient<ITrainingCourseRepository, TrainingCourseRepository>();
            services.AddTransient<IYearGroupRepository, YearGroupRepository>();
            
            // MyPortal business services
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<ISchoolService, SchoolService>();
            services.AddTransient<IStudentService, StudentService>();
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
