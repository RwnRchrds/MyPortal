CREATE TABLE [dbo].[RelationshipTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_RelationshipTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

