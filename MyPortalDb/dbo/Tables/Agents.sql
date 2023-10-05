CREATE TABLE [dbo].[Agents] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [AgencyId]    UNIQUEIDENTIFIER NOT NULL,
    [PersonId]    UNIQUEIDENTIFIER NOT NULL,
    [AgentTypeId] UNIQUEIDENTIFIER NOT NULL,
    [JobTitle]    NVARCHAR (128)   NULL,
    [Deleted]     BIT              NOT NULL,
    CONSTRAINT [PK_Agents] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Agents_Agencies_AgencyId] FOREIGN KEY ([AgencyId]) REFERENCES [dbo].[Agencies] ([Id]),
    CONSTRAINT [FK_Agents_AgentTypes_AgentTypeId] FOREIGN KEY ([AgentTypeId]) REFERENCES [dbo].[AgentTypes] ([Id]),
    CONSTRAINT [FK_Agents_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Agents]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Agents_AgencyId]
    ON [dbo].[Agents]([AgencyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Agents_AgentTypeId]
    ON [dbo].[Agents]([AgentTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Agents_PersonId]
    ON [dbo].[Agents]([PersonId] ASC);

