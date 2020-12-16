CREATE TABLE [dbo].[Locations] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (128)   NOT NULL,
    [System]      BIT              NOT NULL,
    CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED ([Id] ASC)
);

