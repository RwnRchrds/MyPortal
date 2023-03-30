CREATE TABLE [dbo].[Discounts] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]           INT              IDENTITY (1, 1) NOT NULL,
    [Description]         NVARCHAR (256)   NOT NULL,
    [Active]              BIT              NOT NULL,
    [Name]                NVARCHAR (128)   NULL,
    [Amount]              DECIMAL (10, 2)  NOT NULL,
    [Percentage]          BIT              NOT NULL,
    [BlockOtherDiscounts] BIT              NOT NULL,
    CONSTRAINT [PK_Discounts] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Discounts]([ClusterId] ASC);

