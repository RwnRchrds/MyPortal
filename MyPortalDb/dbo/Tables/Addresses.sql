CREATE TABLE [dbo].[Addresses] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [HouseNumber] NVARCHAR (128)   NULL,
    [HouseName]   NVARCHAR (128)   NULL,
    [Apartment]   NVARCHAR (128)   NULL,
    [Street]      NVARCHAR (256)   NOT NULL,
    [District]    NVARCHAR (256)   NULL,
    [Town]        NVARCHAR (256)   NOT NULL,
    [County]      NVARCHAR (256)   NOT NULL,
    [Postcode]    NVARCHAR (128)   NOT NULL,
    [Country]     NVARCHAR (128)   NOT NULL,
    [Validated]   BIT              NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED ([Id] ASC)
);

