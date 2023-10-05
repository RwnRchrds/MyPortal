CREATE TABLE [dbo].[ReportCardEntries] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]        INT              IDENTITY (1, 1) NOT NULL,
    [ReportCardId]     UNIQUEIDENTIFIER NOT NULL,
    [CreatedById]      UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]      DATETIME2 (7)    NOT NULL,
    [WeekId]           UNIQUEIDENTIFIER NOT NULL,
    [PeriodId]         UNIQUEIDENTIFIER NOT NULL,
    [Comments]         NVARCHAR (256)   NULL,
    [AttendanceWeekId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_ReportCardEntries] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReportCardEntries_AttendancePeriods_PeriodId] FOREIGN KEY ([PeriodId]) REFERENCES [dbo].[AttendancePeriods] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ReportCardEntries_AttendanceWeeks_AttendanceWeekId] FOREIGN KEY ([AttendanceWeekId]) REFERENCES [dbo].[AttendanceWeeks] ([Id]),
    CONSTRAINT [FK_ReportCardEntries_ReportCards_ReportCardId] FOREIGN KEY ([ReportCardId]) REFERENCES [dbo].[ReportCards] ([Id]),
    CONSTRAINT [FK_ReportCardEntries_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ReportCardEntries]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardEntries_AttendanceWeekId]
    ON [dbo].[ReportCardEntries]([AttendanceWeekId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardEntries_CreatedById]
    ON [dbo].[ReportCardEntries]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardEntries_PeriodId]
    ON [dbo].[ReportCardEntries]([PeriodId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardEntries_ReportCardId]
    ON [dbo].[ReportCardEntries]([ReportCardId] ASC);

