CREATE TABLE [dbo].[BehaviourRoleTypes] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]     INT              NOT NULL,
    [Description]   NVARCHAR (256)   NOT NULL,
    [Active]        BIT              NOT NULL,
    [DefaultPoints] INT              NOT NULL,
    CONSTRAINT [PK_BehaviourRoleTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

