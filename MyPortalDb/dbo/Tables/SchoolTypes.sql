CREATE TABLE [dbo].[SchoolTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Code]        NVARCHAR (10)    NOT NULL,
    CONSTRAINT [PK_SchoolTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

