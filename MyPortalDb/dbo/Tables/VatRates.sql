CREATE TABLE [dbo].[VatRates] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Value]       DECIMAL (10, 2)  NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_VatRates] PRIMARY KEY CLUSTERED ([Id] ASC)
);

