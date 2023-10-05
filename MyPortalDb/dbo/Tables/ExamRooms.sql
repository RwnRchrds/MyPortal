CREATE TABLE [dbo].[ExamRooms] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [RoomId]    UNIQUEIDENTIFIER NOT NULL,
    [Columns]   INT              NOT NULL,
    [Rows]      INT              NOT NULL,
    CONSTRAINT [PK_ExamRooms] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamRooms_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Rooms] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamRooms]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamRooms_RoomId]
    ON [dbo].[ExamRooms]([RoomId] ASC);

