CREATE TABLE [dbo].[StudentGroups] (
    [Id]               UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description]      NVARCHAR (256)   NOT NULL,
    [Active]           BIT              NOT NULL,
    [Code]             NVARCHAR (50)    NOT NULL,
    [PromoteToGroupId] UNIQUEIDENTIFIER NULL,
    [MainSupervisorId] UNIQUEIDENTIFIER NULL,
    [MaxMembers]       INT              NULL,
    [Notes]            NVARCHAR (256)   NULL,
    [System]           BIT              NOT NULL,
    CONSTRAINT [PK_StudentGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentGroups_StudentGroups_PromoteToGroupId] FOREIGN KEY ([PromoteToGroupId]) REFERENCES [dbo].[StudentGroups] ([Id]),
    CONSTRAINT [FK_StudentGroups_StudentGroupSupervisors_MainSupervisorId] FOREIGN KEY ([MainSupervisorId]) REFERENCES [dbo].[StudentGroupSupervisors] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_StudentGroups_MainSupervisorId]
    ON [dbo].[StudentGroups]([MainSupervisorId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentGroups_PromoteToGroupId]
    ON [dbo].[StudentGroups]([PromoteToGroupId] ASC);

