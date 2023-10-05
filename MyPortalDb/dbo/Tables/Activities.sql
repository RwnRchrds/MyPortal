CREATE TABLE [dbo].[Activities] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]      INT              IDENTITY (1, 1) NOT NULL,
    [StudentGroupId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Activities] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Activities_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [dbo].[StudentGroups] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Activities]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Activities_StudentGroupId]
    ON [dbo].[Activities]([StudentGroupId] ASC);

