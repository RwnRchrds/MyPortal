CREATE TABLE [dbo].[ActivitySupervisors] (
    [Id]           UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ActivityId]   UNIQUEIDENTIFIER NOT NULL,
    [SupervisorId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ActivitySupervisors] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ActivitySupervisors_Activities_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[Activities] ([Id]),
    CONSTRAINT [FK_ActivitySupervisors_StaffMembers_SupervisorId] FOREIGN KEY ([SupervisorId]) REFERENCES [dbo].[StaffMembers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ActivitySupervisors_ActivityId]
    ON [dbo].[ActivitySupervisors]([ActivityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ActivitySupervisors_SupervisorId]
    ON [dbo].[ActivitySupervisors]([SupervisorId] ASC);

