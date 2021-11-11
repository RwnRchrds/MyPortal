CREATE TABLE [dbo].[StudentGroupSupervisors] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentGroupId] UNIQUEIDENTIFIER NOT NULL,
    [SupervisorId]   UNIQUEIDENTIFIER NOT NULL,
    [Title]          NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_StudentGroupSupervisors] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentGroupSupervisors_StaffMembers_SupervisorId] FOREIGN KEY ([SupervisorId]) REFERENCES [dbo].[StaffMembers] ([Id]),
    CONSTRAINT [FK_StudentGroupSupervisors_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [dbo].[StudentGroups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_StudentGroupSupervisors_StudentGroupId]
    ON [dbo].[StudentGroupSupervisors]([StudentGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentGroupSupervisors_SupervisorId]
    ON [dbo].[StudentGroupSupervisors]([SupervisorId] ASC);

