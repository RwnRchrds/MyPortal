CREATE TABLE [dbo].[ExamComponentSittings] (
    [Id]               UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ComponentId]      UNIQUEIDENTIFIER NOT NULL,
    [ExamRoomId]       UNIQUEIDENTIFIER NOT NULL,
    [ExamDate]         DATETIME2 (7)    NOT NULL,
    [ActualStartTime]  TIME (7)         NULL,
    [ExtraTimePercent] INT              NOT NULL,
    CONSTRAINT [PK_ExamComponentSittings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamComponentSittings_ExamComponents_ComponentId] FOREIGN KEY ([ComponentId]) REFERENCES [dbo].[ExamComponents] ([Id]),
    CONSTRAINT [FK_ExamComponentSittings_ExamRooms_ExamRoomId] FOREIGN KEY ([ExamRoomId]) REFERENCES [dbo].[ExamRooms] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExamComponentSittings_ComponentId]
    ON [dbo].[ExamComponentSittings]([ComponentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamComponentSittings_ExamRoomId]
    ON [dbo].[ExamComponentSittings]([ExamRoomId] ASC);

