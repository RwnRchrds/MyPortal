CREATE TABLE [dbo].[ExamSessions] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [StartTime]   TIME (7)         NOT NULL,
    CONSTRAINT [PK_ExamSessions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

