using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortal.Logic.Interfaces
{
    public interface IAppServiceCollection : IDisposable
    {
        IAcademicYearService AcademicYears { get; }
        IAchievementService Achievements { get; }
        IActivityService Activities { get; }
        IAddressService Addresses { get; }
        IAttendanceMarkService AttendanceMarks { get; }
        IAttendancePeriodService AttendancePeriods { get; }
        IAttendanceWeekService AttendanceWeeks { get; }
        IBillService Bills { get; }
        ICalendarService Calendar { get; }
        IContactService Contacts { get; }
        ICurriculumService Curriculum { get; }
        IDetentionService Detentions { get; }
        IDirectoryService Directories { get; }
        IDocumentService Documents { get; }
        IFileService Files { get; }
        IHouseService Houses { get; }
        IIncidentService Incidents { get; }
        ILocationService Locations { get; }
        ILogNoteService LogNotes { get; }
        IPersonService People { get; }
        IRegGroupService RegGroups { get; }
        IRoleService Roles { get; }
        ISchoolService Schools { get; }
        ISenService Sen { get; }
        IStaffMemberService Staff { get; }
        //IStudentGroupService StudentGroups { get; }
        IStudentService Students { get; }
        ISystemSettingService SystemSettings { get; }
        ITaskService Tasks { get; }
        ITokenService Tokens { get; }
        IUserService Users { get; }
        IYearGroupService YearGroups { get; }
    }
}
