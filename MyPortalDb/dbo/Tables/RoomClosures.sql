CREATE TABLE [dbo].[RoomClosures] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [RoomId]    UNIQUEIDENTIFIER NOT NULL,
    [ReasonId]  UNIQUEIDENTIFIER NOT NULL,
    [StartDate] DATETIME2 (7)    NOT NULL,
    [EndDate]   DATETIME2 (7)    NOT NULL,
    [Notes]     NVARCHAR (256)   NULL,
    CONSTRAINT [PK_RoomClosures] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RoomClosures_RoomClosureReasons_ReasonId] FOREIGN KEY ([ReasonId]) REFERENCES [dbo].[RoomClosureReasons] ([Id]),
    CONSTRAINT [FK_RoomClosures_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Rooms] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_RoomClosures_ReasonId]
    ON [dbo].[RoomClosures]([ReasonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RoomClosures_RoomId]
    ON [dbo].[RoomClosures]([RoomId] ASC);

