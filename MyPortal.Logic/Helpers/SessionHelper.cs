using System.Collections.Generic;
using System.Linq;
using MyPortal.Database.Models.QueryResults.Curriculum;
using MyPortal.Logic.Models.Data.Curriculum;

namespace MyPortal.Logic.Helpers;

public static class SessionHelper
{
    public static SessionDataModel[] GetSessionData(SessionPeriodDetailModel[] sessionPeriods)
    {
        var sessionData = new List<SessionDataModel>();
            
        var sessions = sessionPeriods.GroupBy(sp => sp.SessionId).ToArray();

        foreach (var sessionGroup in sessions)
        {
            var firstPeriod = sessionGroup.FirstOrDefault();

            if (firstPeriod != null)
            {
                var periodData = new List<SessionPeriodDataModel>();
                
                var sessionDataItem = new SessionDataModel
                {
                    SessionId = firstPeriod.SessionId,
                    ClassCode = firstPeriod.ClassCode,
                    IsCover = firstPeriod.IsCover,
                    RoomId = firstPeriod.RoomId,
                    RoomName = firstPeriod.RoomName,
                    TeacherId = firstPeriod.TeacherId,
                    TeacherName = firstPeriod.TeacherName,
                    AttendanceWeekId = firstPeriod.AttendanceWeekId,
                    StudentGroupId = firstPeriod.StudentGroupId
                };

                foreach (var period in sessionGroup)
                {
                    periodData.Add(new SessionPeriodDataModel
                    {
                        PeriodId = period.PeriodId,
                        PeriodName = period.PeriodName,
                        StartTime = period.StartTime,
                        EndTime = period.EndTime
                    });
                }

                sessionDataItem.Periods = periodData.ToArray();
                sessionData.Add(sessionDataItem);
            }
        }

        return sessionData.ToArray();
    }
}