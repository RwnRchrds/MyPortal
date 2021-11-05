CREATE TABLE [dbo].[SenTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Code]        NVARCHAR (MAX)   NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_SenTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

