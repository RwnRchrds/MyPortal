CREATE TABLE [dbo].[BehaviourStatus] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Resolved]    BIT              NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_BehaviourStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);

