CREATE TABLE [dbo].[SessionPeriods] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [SessionId] UNIQUEIDENTIFIER NOT NULL,
    [PeriodId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_SessionPeriods] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SessionPeriods_AttendancePeriods_PeriodId] FOREIGN KEY ([PeriodId]) REFERENCES [dbo].[AttendancePeriods] ([Id]),
    CONSTRAINT [FK_SessionPeriods_Sessions_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Sessions] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[SessionPeriods]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SessionPeriods_PeriodId]
    ON [dbo].[SessionPeriods]([PeriodId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SessionPeriods_SessionId]
    ON [dbo].[SessionPeriods]([SessionId] ASC);

