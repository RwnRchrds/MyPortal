CREATE TABLE [dbo].[CoverArrangements] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [WeekId]    UNIQUEIDENTIFIER NOT NULL,
    [SessionId] UNIQUEIDENTIFIER NOT NULL,
    [TeacherId] UNIQUEIDENTIFIER NULL,
    [RoomId]    UNIQUEIDENTIFIER NULL,
    [Comments]  NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_CoverArrangements] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CoverArrangements_AttendanceWeeks_WeekId] FOREIGN KEY ([WeekId]) REFERENCES [dbo].[AttendanceWeeks] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CoverArrangements_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Rooms] ([Id]),
    CONSTRAINT [FK_CoverArrangements_Sessions_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Sessions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CoverArrangements_StaffMembers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[StaffMembers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CoverArrangements_RoomId]
    ON [dbo].[CoverArrangements]([RoomId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CoverArrangements_SessionId]
    ON [dbo].[CoverArrangements]([SessionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CoverArrangements_TeacherId]
    ON [dbo].[CoverArrangements]([TeacherId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CoverArrangements_WeekId]
    ON [dbo].[CoverArrangements]([WeekId] ASC);

