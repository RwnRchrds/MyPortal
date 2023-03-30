CREATE TABLE [dbo].[StudentIncidents] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]  INT              IDENTITY (1, 1) NOT NULL,
    [StudentId]  UNIQUEIDENTIFIER NOT NULL,
    [IncidentId] UNIQUEIDENTIFIER NOT NULL,
    [RoleTypeId] UNIQUEIDENTIFIER NOT NULL,
    [OutcomeId]  UNIQUEIDENTIFIER NOT NULL,
    [StatusId]   UNIQUEIDENTIFIER NOT NULL,
    [Points]     INT              NOT NULL,
    CONSTRAINT [PK_StudentIncidents] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentIncidents_BehaviourOutcomes_OutcomeId] FOREIGN KEY ([OutcomeId]) REFERENCES [dbo].[BehaviourOutcomes] ([Id]),
    CONSTRAINT [FK_StudentIncidents_BehaviourRoleTypes_RoleTypeId] FOREIGN KEY ([RoleTypeId]) REFERENCES [dbo].[BehaviourRoleTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_StudentIncidents_BehaviourStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[BehaviourStatus] ([Id]),
    CONSTRAINT [FK_StudentIncidents_Incidents_IncidentId] FOREIGN KEY ([IncidentId]) REFERENCES [dbo].[Incidents] ([Id]),
    CONSTRAINT [FK_StudentIncidents_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_StudentIncidents_IncidentId]
    ON [dbo].[StudentIncidents]([IncidentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentIncidents_OutcomeId]
    ON [dbo].[StudentIncidents]([OutcomeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentIncidents_RoleTypeId]
    ON [dbo].[StudentIncidents]([RoleTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentIncidents_StatusId]
    ON [dbo].[StudentIncidents]([StatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentIncidents_StudentId]
    ON [dbo].[StudentIncidents]([StudentId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[StudentIncidents]([ClusterId] ASC);

