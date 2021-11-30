CREATE TABLE [dbo].[ExamDates] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [SessionId]   UNIQUEIDENTIFIER NOT NULL,
    [Duration]    INT              NOT NULL,
    [SittingDate] DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_ExamDates] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamDates_ExamSessions_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[ExamSessions] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExamDates_SessionId]
    ON [dbo].[ExamDates]([SessionId] ASC);

