CREATE TABLE [dbo].[ExamRoomSeats] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ExamRoomId] UNIQUEIDENTIFIER NOT NULL,
    [Column]     INT              NOT NULL,
    [Row]        INT              NOT NULL,
    [DoNotUse]   BIT              NOT NULL,
    CONSTRAINT [PK_ExamRoomSeats] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamRoomSeats_ExamRooms_ExamRoomId] FOREIGN KEY ([ExamRoomId]) REFERENCES [dbo].[ExamRooms] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExamRoomSeats_ExamRoomId]
    ON [dbo].[ExamRoomSeats]([ExamRoomId] ASC);

