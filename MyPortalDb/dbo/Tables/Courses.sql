CREATE TABLE [dbo].[Courses] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [SubjectId]   UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Courses_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subjects] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Courses]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Courses_SubjectId]
    ON [dbo].[Courses]([SubjectId] ASC);

