CREATE TABLE [dbo].[LessonPlans] (
    [Id]                 UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudyTopicId]       UNIQUEIDENTIFIER NOT NULL,
    [AuthorId]           UNIQUEIDENTIFIER NOT NULL,
    [DirectoryId]        UNIQUEIDENTIFIER NOT NULL,
    [Title]              NVARCHAR (256)   NOT NULL,
    [LearningObjectives] NVARCHAR (MAX)   NOT NULL,
    [PlanContent]        NVARCHAR (MAX)   NOT NULL,
    [Homework]           NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_LessonPlans] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LessonPlans_Directories_DirectoryId] FOREIGN KEY ([DirectoryId]) REFERENCES [dbo].[Directories] ([Id]),
    CONSTRAINT [FK_LessonPlans_StudyTopics_StudyTopicId] FOREIGN KEY ([StudyTopicId]) REFERENCES [dbo].[StudyTopics] ([Id]),
    CONSTRAINT [FK_LessonPlans_Users_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_LessonPlans_AuthorId]
    ON [dbo].[LessonPlans]([AuthorId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LessonPlans_DirectoryId]
    ON [dbo].[LessonPlans]([DirectoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LessonPlans_StudyTopicId]
    ON [dbo].[LessonPlans]([StudyTopicId] ASC);

