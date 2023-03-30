CREATE TABLE [dbo].[StudyTopics] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [CourseId]    UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (128)   NOT NULL,
    [Order]       INT              NOT NULL,
    CONSTRAINT [PK_StudyTopics] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudyTopics_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_StudyTopics_CourseId]
    ON [dbo].[StudyTopics]([CourseId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[StudyTopics]([ClusterId] ASC);

