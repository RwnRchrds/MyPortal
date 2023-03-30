CREATE TABLE [dbo].[LessonPlans] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]    INT              IDENTITY (1, 1) NOT NULL,
    [StudyTopicId] UNIQUEIDENTIFIER NOT NULL,
    [CreatedById]  UNIQUEIDENTIFIER NOT NULL,
    [DirectoryId]  UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]  DATETIME2 (7)    NOT NULL,
    [Order]        INT              NOT NULL,
    [Title]        NVARCHAR (256)   NOT NULL,
    [PlanContent]  NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_LessonPlans] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LessonPlans_Directories_DirectoryId] FOREIGN KEY ([DirectoryId]) REFERENCES [dbo].[Directories] ([Id]),
    CONSTRAINT [FK_LessonPlans_StudyTopics_StudyTopicId] FOREIGN KEY ([StudyTopicId]) REFERENCES [dbo].[StudyTopics] ([Id]),
    CONSTRAINT [FK_LessonPlans_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Users] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_LessonPlans_CreatedById]
    ON [dbo].[LessonPlans]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LessonPlans_DirectoryId]
    ON [dbo].[LessonPlans]([DirectoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LessonPlans_StudyTopicId]
    ON [dbo].[LessonPlans]([StudyTopicId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[LessonPlans]([ClusterId] ASC);

