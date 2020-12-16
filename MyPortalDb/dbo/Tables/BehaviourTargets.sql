CREATE TABLE [dbo].[BehaviourTargets] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_BehaviourTargets] PRIMARY KEY CLUSTERED ([Id] ASC)
);

