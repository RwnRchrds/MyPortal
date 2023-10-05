CREATE TABLE [dbo].[Sessions] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [ClassId]   UNIQUEIDENTIFIER NOT NULL,
    [TeacherId] UNIQUEIDENTIFIER NOT NULL,
    [RoomId]    UNIQUEIDENTIFIER NULL,
    [StartDate] DATE             NOT NULL,
    [EndDate]   DATE             NOT NULL,
    CONSTRAINT [PK_Sessions] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Sessions_Classes_ClassId] FOREIGN KEY ([ClassId]) REFERENCES [dbo].[Classes] ([Id]),
    CONSTRAINT [FK_Sessions_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Rooms] ([Id]),
    CONSTRAINT [FK_Sessions_StaffMembers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[StaffMembers] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Sessions]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sessions_ClassId]
    ON [dbo].[Sessions]([ClassId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sessions_RoomId]
    ON [dbo].[Sessions]([RoomId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sessions_TeacherId]
    ON [dbo].[Sessions]([TeacherId] ASC);

