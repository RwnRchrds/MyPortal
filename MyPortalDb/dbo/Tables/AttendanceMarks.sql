CREATE TABLE [dbo].[AttendanceMarks] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId]   UNIQUEIDENTIFIER NOT NULL,
    [WeekId]      UNIQUEIDENTIFIER NOT NULL,
    [PeriodId]    UNIQUEIDENTIFIER NOT NULL,
    [Mark]        NVARCHAR (1)     NOT NULL,
    [Comments]    NVARCHAR (256)   NULL,
    [MinutesLate] INT              NOT NULL,
    CONSTRAINT [PK_AttendanceMarks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AttendanceMarks_AttendancePeriods_PeriodId] FOREIGN KEY ([PeriodId]) REFERENCES [dbo].[AttendancePeriods] ([Id]),
    CONSTRAINT [FK_AttendanceMarks_AttendanceWeeks_WeekId] FOREIGN KEY ([WeekId]) REFERENCES [dbo].[AttendanceWeeks] ([Id]),
    CONSTRAINT [FK_AttendanceMarks_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AttendanceMarks_PeriodId]
    ON [dbo].[AttendanceMarks]([PeriodId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AttendanceMarks_StudentId]
    ON [dbo].[AttendanceMarks]([StudentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AttendanceMarks_WeekId]
    ON [dbo].[AttendanceMarks]([WeekId] ASC);

