CREATE TABLE [dbo].[Activities] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentGroupId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Activities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Activities_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [dbo].[StudentGroups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Activities_StudentGroupId]
    ON [dbo].[Activities]([StudentGroupId] ASC);

