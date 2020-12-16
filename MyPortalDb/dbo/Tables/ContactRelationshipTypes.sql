CREATE TABLE [dbo].[ContactRelationshipTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_ContactRelationshipTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

