CREATE TABLE [dbo].[ChargeBillingPeriods] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX)   NULL,
    [StartDate] DATE             NOT NULL,
    [EndDate]   DATE             NOT NULL,
    CONSTRAINT [PK_ChargeBillingPeriods] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ChargeBillingPeriods]([ClusterId] ASC);

