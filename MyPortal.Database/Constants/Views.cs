using SqlKata;

namespace MyPortal.Database.Constants;

public static class Views
{
    public static Query GetAttendancePeriodInstances(string alias)
    {
        var query = new Query($"AttendancePeriodInstances as {alias}");

        query.Select($"{alias}.ActualStartTime", $"{alias}.ActualEndTime", $"{alias}.PeriodId",
            $"{alias}.AttendanceWeekId", $"{alias}.WeekPatternId", $"{alias}.Weekday", $"{alias}.Name",
            $"{alias}.StartTime", $"{alias}.EndTime", $"{alias}.AmReg", $"{alias}.PmReg");
        
        return query;
    }
}