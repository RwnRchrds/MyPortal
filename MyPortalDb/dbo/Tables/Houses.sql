CREATE TABLE [dbo].[Houses] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]      INT              IDENTITY (1, 1) NOT NULL,
    [StudentGroupId] UNIQUEIDENTIFIER NOT NULL,
    [ColourCode]     NVARCHAR (10)    NULL,
    [StaffMemberId]  UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Houses] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Houses_StaffMembers_StaffMemberId] FOREIGN KEY ([StaffMemberId]) REFERENCES [dbo].[StaffMembers] ([Id]),
    CONSTRAINT [FK_Houses_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [dbo].[StudentGroups] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Houses]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Houses_StaffMemberId]
    ON [dbo].[Houses]([StaffMemberId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Houses_StudentGroupId]
    ON [dbo].[Houses]([StudentGroupId] ASC);

