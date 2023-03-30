CREATE TABLE [dbo].[Tasks] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]     INT              IDENTITY (1, 1) NOT NULL,
    [TypeId]        UNIQUEIDENTIFIER NOT NULL,
    [AssignedToId]  UNIQUEIDENTIFIER NULL,
    [CreatedById]   UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]   DATETIME2 (7)    NOT NULL,
    [DueDate]       DATETIME2 (7)    NULL,
    [CompletedDate] DATETIME2 (7)    NULL,
    [Title]         NVARCHAR (128)   NULL,
    [Description]   NVARCHAR (256)   NULL,
    [Completed]     BIT              NOT NULL,
    [AllowEdit]     BIT              NOT NULL,
    [System]        BIT              NOT NULL,
    CONSTRAINT [PK_Tasks] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tasks_People_AssignedToId] FOREIGN KEY ([AssignedToId]) REFERENCES [dbo].[People] ([Id]),
    CONSTRAINT [FK_Tasks_TaskTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[TaskTypes] ([Id]),
    CONSTRAINT [FK_Tasks_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Users] ([Id])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_Tasks_AssignedToId]
    ON [dbo].[Tasks]([AssignedToId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tasks_TypeId]
    ON [dbo].[Tasks]([TypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tasks_CreatedById]
    ON [dbo].[Tasks]([CreatedById] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Tasks]([ClusterId] ASC);

