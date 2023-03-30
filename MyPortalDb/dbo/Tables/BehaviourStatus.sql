CREATE TABLE [dbo].[BehaviourStatus] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Resolved]    BIT              NOT NULL,
    CONSTRAINT [PK_BehaviourStatus] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[BehaviourStatus]([ClusterId] ASC);

