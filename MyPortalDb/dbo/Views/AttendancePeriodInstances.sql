CREATE VIEW AttendancePeriodInstances
AS
SELECT CONVERT(DATETIME,
               CONVERT(CHAR(8), DATEADD(DAY, CASE WHEN AP.Weekday = 0 THEN 6 ELSE AP.Weekday - 1 END, AW.Beginning),
                       112) + ' ' + CONVERT(CHAR(8), AP.StartTime, 108)) AS ActualStartTime,
       CONVERT(DATETIME,
               CONVERT(CHAR(8), DATEADD(DAY, CASE WHEN AP.Weekday = 0 THEN 6 ELSE AP.Weekday - 1 END, AW.Beginning),
                       112) + ' ' + CONVERT(CHAR(8), AP.EndTime, 108))   AS ActualEndTime,
       AP.Id                                                                          as PeriodId,
       AW.Id                                                                          as AttendanceWeekId,
       AP.WeekPatternId                                                               as WeekPatternId,
       AP.Weekday                                                                     as Weekday,
       AP.Name                                                                        as Name,
       AP.StartTime                                                                   as StartTime,
       AP.EndTime                                                                     as EndTime,
       AP.AmReg                                                                       as AmReg,
       AP.PmReg                                                                       as PmReg
FROM dbo.AttendancePeriods AP
         LEFT JOIN dbo.AttendanceWeekPatterns AWP ON AWP.Id = AP.WeekPatternId
         LEFT JOIN dbo.AttendanceWeeks AW ON AW.WeekPatternId = AWP.Id
        LEFT JOIN dbo.AcademicTerms AT ON AT.Id = AW.AcademicTermId
    OUTER APPLY GetOverlappingEvents(CONVERT(DATETIME, CONVERT(CHAR(8), DATEADD(DAY, CASE
                                                                                              WHEN AP.Weekday = 0 THEN 6
                                                                                              ELSE AP.Weekday - 1 END,
                                                                                     AW.Beginning), 112) + ' ' +
                                                            CONVERT(CHAR(8), AP.StartTime, 108)),
                                          CONVERT(DATETIME, CONVERT(CHAR(8), DATEADD(DAY, CASE
                                                                                              WHEN AP.Weekday = 0 THEN 6
                                                                                              ELSE AP.Weekday - 1 END,
                                                                                     AW.Beginning), 112) + ' ' +
                                                            CONVERT(CHAR(8), AP.EndTime, 108)),
                                          '84E9DDA4-1BCB-4A2F-8082-FCE51DD04F28') OE
WHERE (CONVERT(DATETIME,
               CONVERT(CHAR(8), DATEADD(DAY, CASE WHEN AP.Weekday = 0 THEN 6 ELSE AP.Weekday - 1 END, AW.Beginning),
                       112) + ' ' + CONVERT(CHAR(8), AP.StartTime, 108)) >= AT.StartDate AND CONVERT(DATETIME,
               CONVERT(CHAR(8), DATEADD(DAY, CASE WHEN AP.Weekday = 0 THEN 6 ELSE AP.Weekday - 1 END, AW.Beginning),
                       112) + ' ' + CONVERT(CHAR(8), AP.EndTime, 108)) < DATEADD(dd, 1, AT.EndDate))
    AND ((OE.Id IS NULL AND AW.IsNonTimetable = 0)
   OR AP.AmReg = 1
   OR AP.PmReg = 1)
GROUP BY CONVERT(DATETIME, CONVERT(CHAR(8),
                                   DATEADD(DAY, CASE WHEN AP.Weekday = 0 THEN 6 ELSE AP.Weekday - 1 END, AW.Beginning),
                                   112) + ' ' + CONVERT(CHAR(8), AP.StartTime, 108)),
         CONVERT(DATETIME, CONVERT(CHAR(8),
                                   DATEADD(DAY, CASE WHEN AP.Weekday = 0 THEN 6 ELSE AP.Weekday - 1 END, AW.Beginning),
                                   112) + ' ' + CONVERT(CHAR(8), AP.EndTime, 108)),
         AP.Id,
         AW.Id,
         AP.WeekPatternId,
         AP.Weekday,
         AP.Name,
         AP.StartTime,
         AP.EndTime,
         AP.AmReg,
         AP.PmReg;