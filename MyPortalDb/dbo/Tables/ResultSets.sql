CREATE TABLE [dbo].[ResultSets] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Name]        NVARCHAR (256)   NOT NULL,
    CONSTRAINT [PK_ResultSets] PRIMARY KEY CLUSTERED ([Id] ASC)
);

