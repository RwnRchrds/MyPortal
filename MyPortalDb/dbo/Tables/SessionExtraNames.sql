CREATE TABLE [dbo].[SessionExtraNames] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]        INT              IDENTITY (1, 1) NOT NULL,
    [AttendanceWeekId] UNIQUEIDENTIFIER NOT NULL,
    [SessionId]        UNIQUEIDENTIFIER NOT NULL,
    [StudentId]        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_SessionExtraNames] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SessionExtraNames_AttendanceWeeks_AttendanceWeekId] FOREIGN KEY ([AttendanceWeekId]) REFERENCES [dbo].[AttendanceWeeks] ([Id]),
    CONSTRAINT [FK_SessionExtraNames_Sessions_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Sessions] ([Id]),
    CONSTRAINT [FK_SessionExtraNames_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SessionExtraNames_StudentId]
    ON [dbo].[SessionExtraNames]([StudentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SessionExtraNames_SessionId]
    ON [dbo].[SessionExtraNames]([SessionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SessionExtraNames_AttendanceWeekId]
    ON [dbo].[SessionExtraNames]([AttendanceWeekId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[SessionExtraNames]([ClusterId] ASC);

