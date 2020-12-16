CREATE TABLE [dbo].[Discounts] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Name]        NVARCHAR (128)   NULL,
    [Amount]      DECIMAL (10, 2)  NOT NULL,
    [Percentage]  BIT              NOT NULL,
    [Global]      BIT              NOT NULL,
    CONSTRAINT [PK_Discounts] PRIMARY KEY CLUSTERED ([Id] ASC)
);

