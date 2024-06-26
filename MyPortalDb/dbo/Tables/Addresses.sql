﻿CREATE TABLE [dbo].[Addresses] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]      INT              IDENTITY (1, 1) NOT NULL,
    [BuildingNumber] NVARCHAR (128)   NULL,
    [BuildingName]   NVARCHAR (128)   NULL,
    [Apartment]      NVARCHAR (128)   NULL,
    [Street]         NVARCHAR (256)   NOT NULL,
    [District]       NVARCHAR (256)   NULL,
    [Town]           NVARCHAR (256)   NOT NULL,
    [County]         NVARCHAR (256)   NOT NULL,
    [Postcode]       NVARCHAR (128)   NOT NULL,
    [Country]        NVARCHAR (128)   NOT NULL,
    [Validated]      BIT              NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Addresses]([ClusterId] ASC);

