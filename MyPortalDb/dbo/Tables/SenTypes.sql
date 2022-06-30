CREATE TABLE [dbo].[SenTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Code]        NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_SenTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

