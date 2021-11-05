CREATE TABLE [dbo].[ExclusionTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Code]        NVARCHAR (MAX)   NULL,
    [System]      BIT              NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_ExclusionTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

