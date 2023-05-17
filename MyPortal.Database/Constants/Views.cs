using SqlKata;

namespace MyPortal.Database.Constants;

public static class Views
{
    internal static Query GetAttendancePeriodInstances(string alias)
    {
        var query = new Query($"AttendancePeriodInstances as {alias}");

        query.Select($"{alias}.ActualStartTime", $"{alias}.ActualEndTime", $"{alias}.PeriodId",
            $"{alias}.AttendanceWeekId", $"{alias}.WeekPatternId", $"{alias}.Weekday", $"{alias}.Name",
            $"{alias}.StartTime", $"{alias}.EndTime", $"{alias}.AmReg", $"{alias}.PmReg");
        
        return query;
    }

    internal static Query GetSessionPeriodMetadata(string alias)
    {
        var query = new Query($"SessionPeriodMetadata as {alias}");

        query.Select($"{alias}.SessionId", $"{alias}.AttendanceWeekId", $"{alias}.PeriodId", $"{alias}.StudentGroupId",
            $"{alias}.StartTime", $"{alias}.EndTime", $"{alias}.PeriodName", $"{alias}.ClassCode", $"{alias}.TeacherId",
            $"{alias}.TeacherName", $"{alias}.RoomId", $"{alias}.RoomName", $"{alias}.IsCover");
        
        return query;
    }
}