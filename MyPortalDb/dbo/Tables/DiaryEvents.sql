CREATE TABLE [dbo].[DiaryEvents] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [EventTypeId] UNIQUEIDENTIFIER NOT NULL,
    [CreatedById] UNIQUEIDENTIFIER NULL,
    [CreatedDate] DATETIME2 (7)    NOT NULL,
    [RoomId]      UNIQUEIDENTIFIER NULL,
    [Subject]     NVARCHAR (256)   NOT NULL,
    [Description] NVARCHAR (256)   NULL,
    [Location]    NVARCHAR (256)   NULL,
    [StartTime]   DATETIME2 (7)    NOT NULL,
    [EndTime]     DATETIME2 (7)    NOT NULL,
    [AllDay]      BIT              NOT NULL,
    [Public]      BIT              NOT NULL,
    [System]      BIT              NOT NULL,
    CONSTRAINT [PK_DiaryEvents] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DiaryEvents_DiaryEventTypes_EventTypeId] FOREIGN KEY ([EventTypeId]) REFERENCES [dbo].[DiaryEventTypes] ([Id]),
    CONSTRAINT [FK_DiaryEvents_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Rooms] ([Id]),
    CONSTRAINT [FK_DiaryEvents_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Users] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_DiaryEvents_CreatedById]
    ON [dbo].[DiaryEvents]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DiaryEvents_EventTypeId]
    ON [dbo].[DiaryEvents]([EventTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DiaryEvents_RoomId]
    ON [dbo].[DiaryEvents]([RoomId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[DiaryEvents]([ClusterId] ASC);

