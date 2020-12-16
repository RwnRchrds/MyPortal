CREATE TABLE [dbo].[DiaryEvents] (
    [Id]               UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [EventTypeId]      UNIQUEIDENTIFIER NOT NULL,
    [RoomId]           UNIQUEIDENTIFIER NULL,
    [Subject]          NVARCHAR (256)   NOT NULL,
    [Description]      NVARCHAR (256)   NULL,
    [Location]         NVARCHAR (256)   NULL,
    [StartTime]        DATETIME2 (7)    NOT NULL,
    [EndTime]          DATETIME2 (7)    NOT NULL,
    [IsAllDay]         BIT              NOT NULL,
    [IsBlock]          BIT              NOT NULL,
    [IsPublic]         BIT              NOT NULL,
    [IsStudentVisible] BIT              NOT NULL,
    CONSTRAINT [PK_DiaryEvents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DiaryEvents_DiaryEventTypes_EventTypeId] FOREIGN KEY ([EventTypeId]) REFERENCES [dbo].[DiaryEventTypes] ([Id]),
    CONSTRAINT [FK_DiaryEvents_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Rooms] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_DiaryEvents_EventTypeId]
    ON [dbo].[DiaryEvents]([EventTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DiaryEvents_RoomId]
    ON [dbo].[DiaryEvents]([RoomId] ASC);

