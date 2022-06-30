CREATE TABLE [dbo].[GradeSets] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Name]        NVARCHAR (256)   NOT NULL,
    [System]      BIT              NOT NULL,
    CONSTRAINT [PK_GradeSets] PRIMARY KEY CLUSTERED ([Id] ASC)
);

