CREATE TABLE [dbo].[Permissions] (
    [Id]               UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [AreaId]           UNIQUEIDENTIFIER NOT NULL,
    [ShortDescription] NVARCHAR (MAX)   NULL,
    [FullDescription]  NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Permissions_SystemAreas_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [dbo].[SystemAreas] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Permissions_AreaId]
    ON [dbo].[Permissions]([AreaId] ASC);

