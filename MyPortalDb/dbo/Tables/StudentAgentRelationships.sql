CREATE TABLE [dbo].[StudentAgentRelationships] (
    [Id]                 UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId]          UNIQUEIDENTIFIER NOT NULL,
    [AgentId]            UNIQUEIDENTIFIER NOT NULL,
    [RelationshipTypeId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_StudentAgentRelationships] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentAgentRelationships_Agents_AgentId] FOREIGN KEY ([AgentId]) REFERENCES [dbo].[Agents] ([Id]),
    CONSTRAINT [FK_StudentAgentRelationships_RelationshipTypes_RelationshipTypeId] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [dbo].[RelationshipTypes] ([Id]),
    CONSTRAINT [FK_StudentAgentRelationships_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_StudentAgentRelationships_AgentId]
    ON [dbo].[StudentAgentRelationships]([AgentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentAgentRelationships_RelationshipTypeId]
    ON [dbo].[StudentAgentRelationships]([RelationshipTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentAgentRelationships_StudentId]
    ON [dbo].[StudentAgentRelationships]([StudentId] ASC);

