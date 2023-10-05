CREATE TABLE [dbo].[AuditLogs] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]     INT              IDENTITY (1, 1) NOT NULL,
    [TableName]     NVARCHAR (MAX)   NULL,
    [EntityId]      UNIQUEIDENTIFIER NOT NULL,
    [UserId]        UNIQUEIDENTIFIER NOT NULL,
    [AuditActionId] UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]   DATETIME2 (7)    NOT NULL,
    [OldValue]      NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_AuditLogs] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuditLogs_AuditActions_AuditActionId] FOREIGN KEY ([AuditActionId]) REFERENCES [dbo].[AuditActions] ([Id]),
    CONSTRAINT [FK_AuditLogs_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[AuditLogs]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AuditLogs_AuditActionId]
    ON [dbo].[AuditLogs]([AuditActionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AuditLogs_UserId]
    ON [dbo].[AuditLogs]([UserId] ASC);

