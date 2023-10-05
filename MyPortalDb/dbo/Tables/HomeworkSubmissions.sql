CREATE TABLE [dbo].[HomeworkSubmissions] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]      INT              IDENTITY (1, 1) NOT NULL,
    [HomeworkId]     UNIQUEIDENTIFIER NOT NULL,
    [StudentId]      UNIQUEIDENTIFIER NOT NULL,
    [TaskId]         UNIQUEIDENTIFIER NOT NULL,
    [DocumentId]     UNIQUEIDENTIFIER NULL,
    [PointsAchieved] INT              NOT NULL,
    [Comments]       NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_HomeworkSubmissions] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HomeworkSubmissions_Documents_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [dbo].[Documents] ([Id]),
    CONSTRAINT [FK_HomeworkSubmissions_HomeworkItems_HomeworkId] FOREIGN KEY ([HomeworkId]) REFERENCES [dbo].[HomeworkItems] ([Id]),
    CONSTRAINT [FK_HomeworkSubmissions_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id]),
    CONSTRAINT [FK_HomeworkSubmissions_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [dbo].[Tasks] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[HomeworkSubmissions]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HomeworkSubmissions_DocumentId]
    ON [dbo].[HomeworkSubmissions]([DocumentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HomeworkSubmissions_HomeworkId]
    ON [dbo].[HomeworkSubmissions]([HomeworkId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HomeworkSubmissions_StudentId]
    ON [dbo].[HomeworkSubmissions]([StudentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HomeworkSubmissions_TaskId]
    ON [dbo].[HomeworkSubmissions]([TaskId] ASC);

