CREATE TABLE [dbo].[HomeworkSubmissions] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [HomeworkId]     UNIQUEIDENTIFIER NOT NULL,
    [StudentId]      UNIQUEIDENTIFIER NOT NULL,
    [TaskId]         UNIQUEIDENTIFIER NOT NULL,
    [DocumentId]     UNIQUEIDENTIFIER NULL,
    [MaxPoints]      INT              NOT NULL,
    [PointsAchieved] INT              NOT NULL,
    [Comments]       NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_HomeworkSubmissions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HomeworkSubmissions_Documents_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [dbo].[Documents] ([Id]),
    CONSTRAINT [FK_HomeworkSubmissions_HomeworkItems_HomeworkId] FOREIGN KEY ([HomeworkId]) REFERENCES [dbo].[HomeworkItems] ([Id]),
    CONSTRAINT [FK_HomeworkSubmissions_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id]),
    CONSTRAINT [FK_HomeworkSubmissions_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [dbo].[Tasks] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HomeworkSubmissions_DocumentId]
    ON [dbo].[HomeworkSubmissions]([DocumentId] ASC) WHERE ([DocumentId] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_HomeworkSubmissions_HomeworkId]
    ON [dbo].[HomeworkSubmissions]([HomeworkId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HomeworkSubmissions_StudentId]
    ON [dbo].[HomeworkSubmissions]([StudentId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HomeworkSubmissions_TaskId]
    ON [dbo].[HomeworkSubmissions]([TaskId] ASC);

