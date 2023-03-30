CREATE TABLE [dbo].[ExamRoomSeatBlocks] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]  INT              NOT NULL,
    [ExamRoomId] UNIQUEIDENTIFIER NOT NULL,
    [SeatRow]    INT              NOT NULL,
    [SeatColumn] INT              NOT NULL,
    [Comments]   NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ExamRoomSeatBlocks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamRoomSeatBlocks_ExamRooms_ExamRoomId] FOREIGN KEY ([ExamRoomId]) REFERENCES [dbo].[ExamRooms] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_ExamRoomSeatBlocks_ExamRoomId]
    ON [dbo].[ExamRoomSeatBlocks]([ExamRoomId] ASC);

