CREATE TABLE [dbo].[RegGroups] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]      INT              IDENTITY (1, 1) NOT NULL,
    [StudentGroupId] UNIQUEIDENTIFIER NOT NULL,
    [YearGroupId]    UNIQUEIDENTIFIER NOT NULL,
    [RoomId]         UNIQUEIDENTIFIER NULL,
    [StaffMemberId]  UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_RegGroups] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RegGroups_StaffMembers_StaffMemberId] FOREIGN KEY ([StaffMemberId]) REFERENCES [dbo].[StaffMembers] ([Id]),
    CONSTRAINT [FK_RegGroups_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [dbo].[StudentGroups] ([Id]),
    CONSTRAINT [FK_RegGroups_YearGroups_YearGroupId] FOREIGN KEY ([YearGroupId]) REFERENCES [dbo].[YearGroups] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[RegGroups]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RegGroups_StaffMemberId]
    ON [dbo].[RegGroups]([StaffMemberId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RegGroups_StudentGroupId]
    ON [dbo].[RegGroups]([StudentGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RegGroups_YearGroupId]
    ON [dbo].[RegGroups]([YearGroupId] ASC);

