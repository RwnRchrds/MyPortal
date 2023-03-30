CREATE TABLE [dbo].[AttendanceMarks] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [StudentId]   UNIQUEIDENTIFIER NOT NULL,
    [WeekId]      UNIQUEIDENTIFIER NOT NULL,
    [PeriodId]    UNIQUEIDENTIFIER NOT NULL,
    [CodeId]      UNIQUEIDENTIFIER NOT NULL,
    [CreatedById] UNIQUEIDENTIFIER NOT NULL,
    [Comments]    NVARCHAR (256)   NULL,
    [MinutesLate] INT              NOT NULL,
    CONSTRAINT [PK_AttendanceMarks] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AttendanceMarks_AttendanceCodes_CodeId] FOREIGN KEY ([CodeId]) REFERENCES [dbo].[AttendanceCodes] ([Id]),
    CONSTRAINT [FK_AttendanceMarks_AttendancePeriods_PeriodId] FOREIGN KEY ([PeriodId]) REFERENCES [dbo].[AttendancePeriods] ([Id]),
    CONSTRAINT [FK_AttendanceMarks_AttendanceWeeks_WeekId] FOREIGN KEY ([WeekId]) REFERENCES [dbo].[AttendanceWeeks] ([Id]),
    CONSTRAINT [FK_AttendanceMarks_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id]),
    CONSTRAINT [FK_AttendanceMarks_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_AttendanceMarks_CodeId]
    ON [dbo].[AttendanceMarks]([CodeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AttendanceMarks_CreatedById]
    ON [dbo].[AttendanceMarks]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AttendanceMarks_PeriodId]
    ON [dbo].[AttendanceMarks]([PeriodId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AttendanceMarks_StudentId]
    ON [dbo].[AttendanceMarks]([StudentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AttendanceMarks_WeekId]
    ON [dbo].[AttendanceMarks]([WeekId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[AttendanceMarks]([ClusterId] ASC);

