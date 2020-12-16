CREATE TABLE [dbo].[AgentRelationshipTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_AgentRelationshipTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

