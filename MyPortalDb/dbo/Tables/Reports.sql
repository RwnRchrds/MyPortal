CREATE TABLE [dbo].[Reports] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [AreaId]      UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (128)   NOT NULL,
    [Description] NVARCHAR (128)   NOT NULL,
    [Restricted]  BIT              NOT NULL,
    CONSTRAINT [PK_Reports] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Reports_SystemAreas_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [dbo].[SystemAreas] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Reports_AreaId]
    ON [dbo].[Reports]([AreaId] ASC);

