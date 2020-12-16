CREATE TABLE [dbo].[ReportCardSubmissions] (
    [Id]               UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ReportCardId]     UNIQUEIDENTIFIER NOT NULL,
    [SubmittedById]    UNIQUEIDENTIFIER NOT NULL,
    [WeekId]           UNIQUEIDENTIFIER NOT NULL,
    [PeriodId]         UNIQUEIDENTIFIER NOT NULL,
    [Comments]         NVARCHAR (256)   NULL,
    [AttendanceWeekId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_ReportCardSubmissions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReportCardSubmissions_AttendancePeriods_PeriodId] FOREIGN KEY ([PeriodId]) REFERENCES [dbo].[AttendancePeriods] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ReportCardSubmissions_AttendanceWeeks_AttendanceWeekId] FOREIGN KEY ([AttendanceWeekId]) REFERENCES [dbo].[AttendanceWeeks] ([Id]),
    CONSTRAINT [FK_ReportCardSubmissions_ReportCards_ReportCardId] FOREIGN KEY ([ReportCardId]) REFERENCES [dbo].[ReportCards] ([Id]),
    CONSTRAINT [FK_ReportCardSubmissions_Users_SubmittedById] FOREIGN KEY ([SubmittedById]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardSubmissions_AttendanceWeekId]
    ON [dbo].[ReportCardSubmissions]([AttendanceWeekId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardSubmissions_PeriodId]
    ON [dbo].[ReportCardSubmissions]([PeriodId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardSubmissions_ReportCardId]
    ON [dbo].[ReportCardSubmissions]([ReportCardId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardSubmissions_SubmittedById]
    ON [dbo].[ReportCardSubmissions]([SubmittedById] ASC);

