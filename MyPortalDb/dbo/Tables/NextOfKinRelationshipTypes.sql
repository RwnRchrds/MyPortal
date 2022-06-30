CREATE TABLE [dbo].[NextOfKinRelationshipTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [System]      BIT              NOT NULL,
    CONSTRAINT [PK_NextOfKinRelationshipTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

