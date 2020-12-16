CREATE TABLE [dbo].[ActivityMemberships] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ActivityId] UNIQUEIDENTIFIER NOT NULL,
    [StudentId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ActivityMemberships] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ActivityMemberships_Activities_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[Activities] ([Id]),
    CONSTRAINT [FK_ActivityMemberships_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ActivityMemberships_ActivityId]
    ON [dbo].[ActivityMemberships]([ActivityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ActivityMemberships_StudentId]
    ON [dbo].[ActivityMemberships]([StudentId] ASC);

