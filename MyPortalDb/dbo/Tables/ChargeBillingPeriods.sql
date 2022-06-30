CREATE TABLE [dbo].[ChargeBillingPeriods] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Name]      NVARCHAR (MAX)   NULL,
    [StartDate] DATETIME2 (7)    NOT NULL,
    [EndDate]   DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_ChargeBillingPeriods] PRIMARY KEY CLUSTERED ([Id] ASC)
);

