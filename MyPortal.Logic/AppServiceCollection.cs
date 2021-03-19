using MyPortal.Logic.Enums;
using MyPortal.Logic.FileProviders;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Services;

namespace MyPortal.Logic
{
    public class AppServiceCollection : IAppServiceCollection
    {
        private readonly IIdentityServiceCollection _identityServices;
        private IAcademicYearService _academicYears;
        private IAchievementService _achievements;
        private IActivityService _activities;
        private IAddressService _addresses;
        private IAttendanceMarkService _attendanceMarks;
        private IAttendancePeriodService _attendancePeriods;
        private IAttendanceWeekService _attendanceWeeks;
        private IBillService _bills;
        private ICalendarService _calendar;
        private IContactService _contacts;
        private ICurriculumService _curriculum;
        private IDetentionService _detentions;
        private IDirectoryService _directories;
        private IDocumentService _documents;
        private IFileService _files;
        private IHouseService _houses;
        private IIncidentService _incidents;
        private ILocationService _locations;
        private ILogNoteService _logNotes;
        private IPersonService _people;
        private IRegGroupService _regGroups;
        private IRoleService _roles;
        private ISchoolService _schools;
        private IStaffMemberService _staff;
        private IStudentService _students;
        private ISystemSettingService _systemSettings;
        private ITaskService _tasks;
        private ITokenService _tokens;
        private IUserService _users;
        private IYearGroupService _yearGroups;
        private ISenService _sen;

        public AppServiceCollection(IIdentityServiceCollection identityServices)
        {
            _identityServices = identityServices;
        }

        public IAcademicYearService AcademicYears => _academicYears ??= new AcademicYearService();

        public IAchievementService Achievements => _achievements ??= new AchievementService();

        public IActivityService Activities => _activities ??= new ActivityService();

        public IAddressService Addresses => _addresses ??= new AddressService();

        public IAttendanceMarkService AttendanceMarks => _attendanceMarks ??= new AttendanceMarkService();

        public IAttendancePeriodService AttendancePeriods => _attendancePeriods ??= new AttendancePeriodService();

        public IAttendanceWeekService AttendanceWeeks => _attendanceWeeks ??= new AttendanceWeekService();

        public IBillService Bills => _bills ??= new BillService();

        public ICalendarService Calendar => _calendar ??= new CalendarService();

        public IContactService Contacts => _contacts ??= new ContactService();

        public ICurriculumService Curriculum => _curriculum ??= new CurriculumService();

        public IDetentionService Detentions => _detentions ??= new DetentionService();

        public IDirectoryService Directories => _directories ??= new DirectoryService();

        public IDocumentService Documents => _documents ??= new DocumentService();

        public IFileService Files
        {
            get
            {
                if (_files != null)
                {
                    return _files;
                }

                if (Configuration.Instance.FileProvider == FileProvider.GoogleDrive)
                {
                    return _files = new HostedFileService(new GoogleFileProvider());
                }
                else
                {
                    return _files = new LocalFileService(new LocalFileProvider());
                }
            }
        }

        public IHouseService Houses => _houses ??= new HouseService();

        public IIncidentService Incidents => _incidents ??= new IncidentService();

        public ILocationService Locations => _locations ??= new LocationService();

        public ILogNoteService LogNotes => _logNotes ??= new LogNoteService();

        public IPersonService People => _people ??= new PersonService();

        public IRegGroupService RegGroups => _regGroups ??= new RegGroupService();

        public IRoleService Roles => _roles ??= new RoleService(_identityServices);

        public ISchoolService Schools => _schools ??= new SchoolService();

        public ISenService Sen => _sen ??= new SenService();

        public IStaffMemberService Staff => _staff ??= new StaffMemberService();

        //public IStudentGroupService StudentGroups => _studentGroups ??= new S;

        public IStudentService Students => _students ??= new StudentService();

        public ISystemSettingService SystemSettings => _systemSettings ??= new SystemSettingService();

        public ITaskService Tasks => _tasks ??= new TaskService();

        public ITokenService Tokens => _tokens ??= new TokenService();

        public IUserService Users => _users ??= new UserService(_identityServices);

        public IYearGroupService YearGroups => _yearGroups ??= new YearGroupService();

        public void Dispose()
        {
            _identityServices?.Dispose();
        }
    }
}
