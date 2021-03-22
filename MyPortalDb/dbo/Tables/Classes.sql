CREATE TABLE [dbo].[Classes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [CourseId]    UNIQUEIDENTIFIER NOT NULL,
    [GroupId]     UNIQUEIDENTIFIER NOT NULL,
    [DirectoryId] UNIQUEIDENTIFIER NOT NULL,
    [Code]        NVARCHAR (10)    NOT NULL,
    CONSTRAINT [PK_Classes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Classes_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id]),
    CONSTRAINT [FK_Classes_CurriculumGroups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[CurriculumGroups] ([Id]),
    CONSTRAINT [FK_Classes_Directories_DirectoryId] FOREIGN KEY ([DirectoryId]) REFERENCES [dbo].[Directories] ([Id]) ON DELETE CASCADE
);






GO



GO
CREATE NONCLUSTERED INDEX [IX_Classes_CourseId]
    ON [dbo].[Classes]([CourseId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Classes_GroupId]
    ON [dbo].[Classes]([GroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Classes_DirectoryId]
    ON [dbo].[Classes]([DirectoryId] ASC);

