CREATE TABLE [dbo].[Tasks] (
    [Id]            UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [TypeId]        UNIQUEIDENTIFIER NOT NULL,
    [AssignedToId]  UNIQUEIDENTIFIER NOT NULL,
    [AssignedById]  UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]   DATETIME2 (7)    NOT NULL,
    [DueDate]       DATETIME2 (7)    NULL,
    [CompletedDate] DATETIME2 (7)    NULL,
    [Title]         NVARCHAR (128)   NOT NULL,
    [Description]   NVARCHAR (256)   NULL,
    [Completed]     BIT              NOT NULL,
    CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tasks_People_AssignedToId] FOREIGN KEY ([AssignedToId]) REFERENCES [dbo].[People] ([Id]),
    CONSTRAINT [FK_Tasks_TaskTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[TaskTypes] ([Id]),
    CONSTRAINT [FK_Tasks_Users_AssignedById] FOREIGN KEY ([AssignedById]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Tasks_AssignedById]
    ON [dbo].[Tasks]([AssignedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tasks_AssignedToId]
    ON [dbo].[Tasks]([AssignedToId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tasks_TypeId]
    ON [dbo].[Tasks]([TypeId] ASC);

