CREATE TABLE [dbo].[BasketItems] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [StudentId] UNIQUEIDENTIFIER NOT NULL,
    [ProductId] UNIQUEIDENTIFIER NOT NULL,
    [Quantity]  INT              NOT NULL,
    CONSTRAINT [PK_BasketItems] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BasketItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]),
    CONSTRAINT [FK_BasketItems_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[BasketItems]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BasketItems_ProductId]
    ON [dbo].[BasketItems]([ProductId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BasketItems_StudentId]
    ON [dbo].[BasketItems]([StudentId] ASC);

