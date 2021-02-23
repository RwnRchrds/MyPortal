CREATE VIEW [dbo].[AttendancePeriods_PossibleAttendancePeriods]
AS
SELECT        CONVERT(DATETIME, CONVERT(CHAR(8), DATEADD(DAY, CASE WHEN P.Weekday = 0 THEN 6 ELSE P.Weekday - 1 END, W.Beginning), 112) + ' ' + CONVERT(CHAR(8), P.StartTime, 108)) AS StartTime, CONVERT(DATETIME, 
                         CONVERT(CHAR(8), DATEADD(DAY, CASE WHEN P.Weekday = 0 THEN 6 ELSE P.Weekday - 1 END, W.Beginning), 112) + ' ' + CONVERT(CHAR(8), P.EndTime, 108)) AS EndTime, P.Id AS PeriodId, W.Id AS AttendanceWeekId
FROM            dbo.AttendancePeriods AS P INNER JOIN
                         dbo.AttendanceWeekPatterns AS WP ON WP.Id = P.WeekPatternId INNER JOIN
                         dbo.AttendanceWeeks AS W ON W.WeekPatternId = WP.Id
WHERE        (W.IsNonTimetable = 0)