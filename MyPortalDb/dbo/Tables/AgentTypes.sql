CREATE TABLE [dbo].[AgentTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_AgentTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

