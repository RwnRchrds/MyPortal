CREATE TABLE [dbo].[UserReminderSettings] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]    INT              NOT NULL,
    [UserId]       UNIQUEIDENTIFIER NOT NULL,
    [ReminderType] UNIQUEIDENTIFIER NOT NULL,
    [RemindBefore] TIME (7)         NOT NULL,
    CONSTRAINT [PK_UserReminderSettings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserReminderSettings_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_UserReminderSettings_UserId]
    ON [dbo].[UserReminderSettings]([UserId] ASC);

