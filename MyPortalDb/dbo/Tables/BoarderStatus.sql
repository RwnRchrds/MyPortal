CREATE TABLE [dbo].[BoarderStatus] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Code]        NVARCHAR (MAX)   NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_BoarderStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);

