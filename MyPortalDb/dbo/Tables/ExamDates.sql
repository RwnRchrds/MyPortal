CREATE TABLE [dbo].[ExamDates] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [SessionId]   UNIQUEIDENTIFIER NOT NULL,
    [Duration]    INT              NOT NULL,
    [SittingDate] DATE             NOT NULL,
    CONSTRAINT [PK_ExamDates] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamDates_ExamSessions_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[ExamSessions] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_ExamDates_SessionId]
    ON [dbo].[ExamDates]([SessionId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamDates]([ClusterId] ASC);

