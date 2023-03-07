CREATE VIEW AttendancePeriodInstances
AS
SELECT CONVERT(DATETIME, CONVERT(CHAR(8), DATEADD(DAY, CASE WHEN AP.Weekday = 0 THEN 6 ELSE AP.Weekday - 1 END, AW.Beginning), 112) + ' ' + CONVERT(CHAR(8), AP.StartTime, 108)) AS ActualStartTime,
       CONVERT(DATETIME, CONVERT(CHAR(8), DATEADD(DAY, CASE WHEN AP.Weekday = 0 THEN 6 ELSE AP.Weekday - 1 END, AW.Beginning), 112) + ' ' + CONVERT(CHAR(8), AP.EndTime, 108)) AS ActualEndTime,
       AP.Id as PeriodId,
       AW.Id as AttendanceWeekId,
       AP.WeekPatternId as WeekPatternId,
       AP.Weekday as Weekday,
       AP.Name as Name,
       AP.StartTime as StartTime,
       AP.EndTime as EndTime,
       AP.AmReg as AmReg,
       AP.PmReg as PmReg
FROM dbo.AttendancePeriods AP
         LEFT JOIN dbo.AttendanceWeekPatterns AWP ON AWP.Id = AP.WeekPatternId
         LEFT JOIN dbo.AttendanceWeeks AW ON AW.WeekPatternId = AWP.Id
WHERE AW.IsNonTimetable = 0 OR AP.AmReg = 1 OR AP.PmReg = 1;