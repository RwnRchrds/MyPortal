CREATE TABLE [dbo].[YearGroups] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]             INT              IDENTITY (1, 1) NOT NULL,
    [StudentGroupId]        UNIQUEIDENTIFIER NOT NULL,
    [CurriculumYearGroupId] UNIQUEIDENTIFIER NOT NULL,
    [StaffMemberId]         UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_YearGroups] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_YearGroups_CurriculumYearGroups_CurriculumYearGroupId] FOREIGN KEY ([CurriculumYearGroupId]) REFERENCES [dbo].[CurriculumYearGroups] ([Id]),
    CONSTRAINT [FK_YearGroups_StaffMembers_StaffMemberId] FOREIGN KEY ([StaffMemberId]) REFERENCES [dbo].[StaffMembers] ([Id]),
    CONSTRAINT [FK_YearGroups_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [dbo].[StudentGroups] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_YearGroups_CurriculumYearGroupId]
    ON [dbo].[YearGroups]([CurriculumYearGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_YearGroups_StaffMemberId]
    ON [dbo].[YearGroups]([StaffMemberId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_YearGroups_StudentGroupId]
    ON [dbo].[YearGroups]([StudentGroupId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[YearGroups]([ClusterId] ASC);

