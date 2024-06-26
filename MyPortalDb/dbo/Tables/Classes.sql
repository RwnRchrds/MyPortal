﻿CREATE TABLE [dbo].[Classes] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]         INT              IDENTITY (1, 1) NOT NULL,
    [CourseId]          UNIQUEIDENTIFIER NOT NULL,
    [CurriculumGroupId] UNIQUEIDENTIFIER NOT NULL,
    [DirectoryId]       UNIQUEIDENTIFIER NOT NULL,
    [Code]              NVARCHAR (10)    NOT NULL,
    CONSTRAINT [PK_Classes] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Classes_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id]),
    CONSTRAINT [FK_Classes_CurriculumGroups_CurriculumGroupId] FOREIGN KEY ([CurriculumGroupId]) REFERENCES [dbo].[CurriculumGroups] ([Id]),
    CONSTRAINT [FK_Classes_Directories_DirectoryId] FOREIGN KEY ([DirectoryId]) REFERENCES [dbo].[Directories] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Classes]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Classes_CourseId]
    ON [dbo].[Classes]([CourseId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Classes_CurriculumGroupId]
    ON [dbo].[Classes]([CurriculumGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Classes_DirectoryId]
    ON [dbo].[Classes]([DirectoryId] ASC);

