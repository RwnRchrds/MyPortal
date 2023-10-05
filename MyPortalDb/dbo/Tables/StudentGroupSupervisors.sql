CREATE TABLE [dbo].[StudentGroupSupervisors] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]      INT              IDENTITY (1, 1) NOT NULL,
    [StudentGroupId] UNIQUEIDENTIFIER NOT NULL,
    [SupervisorId]   UNIQUEIDENTIFIER NOT NULL,
    [Title]          NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_StudentGroupSupervisors] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentGroupSupervisors_StaffMembers_SupervisorId] FOREIGN KEY ([SupervisorId]) REFERENCES [dbo].[StaffMembers] ([Id]),
    CONSTRAINT [FK_StudentGroupSupervisors_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [dbo].[StudentGroups] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[StudentGroupSupervisors]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentGroupSupervisors_StudentGroupId]
    ON [dbo].[StudentGroupSupervisors]([StudentGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentGroupSupervisors_SupervisorId]
    ON [dbo].[StudentGroupSupervisors]([SupervisorId] ASC);

