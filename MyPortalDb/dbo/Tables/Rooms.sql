CREATE TABLE [dbo].[Rooms] (
    [Id]               UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [LocationId]       UNIQUEIDENTIFIER NULL,
    [Code]             NVARCHAR (10)    NULL,
    [Name]             NVARCHAR (256)   NULL,
    [MaxGroupSize]     INT              NOT NULL,
    [TelephoneNo]      NVARCHAR (MAX)   NULL,
    [ExcludeFromCover] BIT              NOT NULL,
    CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Rooms_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Locations] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Rooms_LocationId]
    ON [dbo].[Rooms]([LocationId] ASC);

