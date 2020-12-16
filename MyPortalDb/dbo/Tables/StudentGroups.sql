CREATE TABLE [dbo].[StudentGroups] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [GroupType]   UNIQUEIDENTIFIER NOT NULL,
    [BaseGroupId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_StudentGroups] PRIMARY KEY CLUSTERED ([Id] ASC)
);

