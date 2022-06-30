CREATE TABLE [dbo].[Courses] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [SubjectId]   UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Courses_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subjects] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Courses_SubjectId]
    ON [dbo].[Courses]([SubjectId] ASC);

