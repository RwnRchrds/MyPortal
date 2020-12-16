CREATE TABLE [dbo].[ExamRooms] (
    [Id]      UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [RoomId]  UNIQUEIDENTIFIER NOT NULL,
    [Columns] INT              NOT NULL,
    [Rows]    INT              NOT NULL,
    CONSTRAINT [PK_ExamRooms] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamRooms_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Rooms] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ExamRooms_RoomId]
    ON [dbo].[ExamRooms]([RoomId] ASC);

