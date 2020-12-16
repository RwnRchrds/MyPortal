CREATE TABLE [dbo].[AttendanceWeeks] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [WeekPatternId]  UNIQUEIDENTIFIER NOT NULL,
    [Beginning]      DATE             NOT NULL,
    [IsNonTimetable] BIT              NOT NULL,
    CONSTRAINT [PK_AttendanceWeeks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AttendanceWeeks_AttendanceWeekPatterns_WeekPatternId] FOREIGN KEY ([WeekPatternId]) REFERENCES [dbo].[AttendanceWeekPatterns] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AttendanceWeeks_WeekPatternId]
    ON [dbo].[AttendanceWeeks]([WeekPatternId] ASC);

