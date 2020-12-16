CREATE TABLE [dbo].[BasketItems] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId] UNIQUEIDENTIFIER NOT NULL,
    [ProductId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_BasketItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BasketItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]),
    CONSTRAINT [FK_BasketItems_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_BasketItems_ProductId]
    ON [dbo].[BasketItems]([ProductId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BasketItems_StudentId]
    ON [dbo].[BasketItems]([StudentId] ASC);

