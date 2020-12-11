using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class ActivityService : BaseService, IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IActivityEventRepository _activityEventRepository;
        private readonly IActivityMembershipRepository _activityMembershipRepository;
        private readonly IActivitySupervisorRepository _activitySupervisorRepository;
        private readonly IAttendancePeriodRepository _attendancePeriodRepository;
        private readonly IAttendanceWeekRepository _attendanceWeekRepository;

        public ActivityService(IActivityRepository activityRepository, IActivityEventRepository activityEventRepository,
            IActivityMembershipRepository activityMembershipRepository,
            IActivitySupervisorRepository activitySupervisorRepository,
            IAttendancePeriodRepository attendancePeriodRepository, IAttendanceWeekRepository attendanceWeekRepository)
        {
            _activityRepository = activityRepository;
            _activityEventRepository = activityEventRepository;
            _activityMembershipRepository = activityMembershipRepository;
            _activitySupervisorRepository = activitySupervisorRepository;
            _attendancePeriodRepository = attendancePeriodRepository;
            _attendanceWeekRepository = attendanceWeekRepository;
        }

        public override void Dispose()
        {
            _activityRepository?.Dispose();
            _activityEventRepository?.Dispose();
            _activityMembershipRepository?.Dispose();
            _activitySupervisorRepository?.Dispose();
            _attendancePeriodRepository?.Dispose();
            _attendanceWeekRepository?.Dispose();
        }
    }
}
