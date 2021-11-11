CREATE TABLE [dbo].[SchoolPhases] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Code]        NVARCHAR (10)    NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_SchoolPhases] PRIMARY KEY CLUSTERED ([Id] ASC)
);

