CREATE TABLE [dbo].[SenStatus] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Code]        CHAR (1)         NOT NULL,
    CONSTRAINT [PK_SenStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);

