CREATE TABLE [dbo].[ActivityEvents] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ActivityId] UNIQUEIDENTIFIER NOT NULL,
    [EventId]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ActivityEvents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ActivityEvents_Activities_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[Activities] ([Id]),
    CONSTRAINT [FK_ActivityEvents_DiaryEvents_EventId] FOREIGN KEY ([EventId]) REFERENCES [dbo].[DiaryEvents] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ActivityEvents_ActivityId]
    ON [dbo].[ActivityEvents]([ActivityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ActivityEvents_EventId]
    ON [dbo].[ActivityEvents]([EventId] ASC);

