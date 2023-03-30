CREATE TABLE [dbo].[Rooms] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]        INT              IDENTITY (1, 1) NOT NULL,
    [BuildingFloorId]  UNIQUEIDENTIFIER NULL,
    [Code]             NVARCHAR (10)    NULL,
    [Name]             NVARCHAR (256)   NULL,
    [MaxGroupSize]     INT              NOT NULL,
    [TelephoneNo]      NVARCHAR (MAX)   NULL,
    [ExcludeFromCover] BIT              NOT NULL,
    [LocationId]       UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Rooms] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Rooms_BuildingFloors_BuildingFloorId] FOREIGN KEY ([BuildingFloorId]) REFERENCES [dbo].[BuildingFloors] ([Id]),
    CONSTRAINT [FK_Rooms_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Locations] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_Rooms_BuildingFloorId]
    ON [dbo].[Rooms]([BuildingFloorId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Rooms_LocationId]
    ON [dbo].[Rooms]([LocationId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Rooms]([ClusterId] ASC);

