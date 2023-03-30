CREATE TABLE [dbo].[BuildingFloors] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [BuildingId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_BuildingFloors] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BuildingFloors_Buildings_BuildingId] FOREIGN KEY ([BuildingId]) REFERENCES [dbo].[Buildings] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_BuildingFloors_BuildingId]
    ON [dbo].[BuildingFloors]([BuildingId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[BuildingFloors]([ClusterId] ASC);

