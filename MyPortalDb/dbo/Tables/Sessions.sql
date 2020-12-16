CREATE TABLE [dbo].[Sessions] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ClassId]   UNIQUEIDENTIFIER NOT NULL,
    [PeriodId]  UNIQUEIDENTIFIER NOT NULL,
    [TeacherId] UNIQUEIDENTIFIER NOT NULL,
    [RoomId]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Sessions_AttendancePeriods_PeriodId] FOREIGN KEY ([PeriodId]) REFERENCES [dbo].[AttendancePeriods] ([Id]),
    CONSTRAINT [FK_Sessions_Classes_ClassId] FOREIGN KEY ([ClassId]) REFERENCES [dbo].[Classes] ([Id]),
    CONSTRAINT [FK_Sessions_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Rooms] ([Id]),
    CONSTRAINT [FK_Sessions_StaffMembers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[StaffMembers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Sessions_ClassId]
    ON [dbo].[Sessions]([ClassId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sessions_PeriodId]
    ON [dbo].[Sessions]([PeriodId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sessions_RoomId]
    ON [dbo].[Sessions]([RoomId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sessions_TeacherId]
    ON [dbo].[Sessions]([TeacherId] ASC);

