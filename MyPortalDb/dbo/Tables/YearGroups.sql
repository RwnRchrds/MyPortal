CREATE TABLE [dbo].[YearGroups] (
    [Id]                    UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Name]                  NVARCHAR (256)   NOT NULL,
    [HeadId]                UNIQUEIDENTIFIER NULL,
    [CurriculumYearGroupId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_YearGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_YearGroups_CurriculumYearGroups_CurriculumYearGroupId] FOREIGN KEY ([CurriculumYearGroupId]) REFERENCES [dbo].[CurriculumYearGroups] ([Id]),
    CONSTRAINT [FK_YearGroups_StaffMembers_HeadId] FOREIGN KEY ([HeadId]) REFERENCES [dbo].[StaffMembers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_YearGroups_CurriculumYearGroupId]
    ON [dbo].[YearGroups]([CurriculumYearGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_YearGroups_HeadId]
    ON [dbo].[YearGroups]([HeadId] ASC);

