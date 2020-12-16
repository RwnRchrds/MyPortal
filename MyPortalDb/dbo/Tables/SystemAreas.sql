CREATE TABLE [dbo].[SystemAreas] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (128)   NOT NULL,
    [ParentId]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_SystemAreas] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SystemAreas_SystemAreas_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[SystemAreas] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SystemAreas_ParentId]
    ON [dbo].[SystemAreas]([ParentId] ASC);

