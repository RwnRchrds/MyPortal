CREATE TABLE [dbo].[StudyTopics] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [CourseId]    UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (128)   NOT NULL,
    CONSTRAINT [PK_StudyTopics] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudyTopics_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_StudyTopics_CourseId]
    ON [dbo].[StudyTopics]([CourseId] ASC);

