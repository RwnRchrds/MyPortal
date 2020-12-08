using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IActivityEventRepository _activityEventRepository;
        private readonly IActivityMembershipRepository _activityMembershipRepository;
        private readonly IActivitySupervisorRepository _activitySupervisorRepository;
        private readonly IAttendancePeriodRepository _attendancePeriodRepository;
        private readonly IAttendanceWeekRepository _attendanceWeekRepository;

        public ActivityService()
        {

        }

        public void Dispose()
        {
            _activityRepository?.Dispose();
            _activityEventRepository?.Dispose();
            _activityMembershipRepository?.Dispose();
            _activitySupervisorRepository?.Dispose();
        }
    }
}
