CREATE TABLE [dbo].[BuildingFloors] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [BuildingId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_BuildingFloors] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BuildingFloors_Buildings_BuildingId] FOREIGN KEY ([BuildingId]) REFERENCES [dbo].[Buildings] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_BuildingFloors_BuildingId]
    ON [dbo].[BuildingFloors]([BuildingId] ASC);

