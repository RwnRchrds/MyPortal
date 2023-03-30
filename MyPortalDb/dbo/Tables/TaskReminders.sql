CREATE TABLE [dbo].[TaskReminders] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]  INT              IDENTITY (1, 1) NOT NULL,
    [TaskId]     UNIQUEIDENTIFIER NOT NULL,
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    [RemindTime] DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_TaskReminders] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaskReminders_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [dbo].[Tasks] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_TaskReminders_TaskId]
    ON [dbo].[TaskReminders]([TaskId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[TaskReminders]([ClusterId] ASC);

