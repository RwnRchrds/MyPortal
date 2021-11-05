CREATE TABLE [dbo].[RegGroups] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentGroupId] UNIQUEIDENTIFIER NOT NULL,
    [YearGroupId]    UNIQUEIDENTIFIER NOT NULL,
    [StaffMemberId]  UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_RegGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RegGroups_StaffMembers_StaffMemberId] FOREIGN KEY ([StaffMemberId]) REFERENCES [dbo].[StaffMembers] ([Id]),
    CONSTRAINT [FK_RegGroups_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [dbo].[StudentGroups] ([Id]),
    CONSTRAINT [FK_RegGroups_YearGroups_YearGroupId] FOREIGN KEY ([YearGroupId]) REFERENCES [dbo].[YearGroups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_RegGroups_StaffMemberId]
    ON [dbo].[RegGroups]([StaffMemberId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RegGroups_StudentGroupId]
    ON [dbo].[RegGroups]([StudentGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RegGroups_YearGroupId]
    ON [dbo].[RegGroups]([YearGroupId] ASC);

