CREATE TABLE [dbo].[ProductTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [IsMeal]      BIT              NOT NULL,
    CONSTRAINT [PK_ProductTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

