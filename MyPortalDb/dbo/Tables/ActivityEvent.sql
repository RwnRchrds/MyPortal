CREATE TABLE [dbo].[ActivityEvent] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ActivityId] UNIQUEIDENTIFIER NOT NULL,
    [EventId]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ActivityEvent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ActivityEvent_Activities_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[Activities] ([Id]),
    CONSTRAINT [FK_ActivityEvent_DiaryEvents_EventId] FOREIGN KEY ([EventId]) REFERENCES [dbo].[DiaryEvents] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ActivityEvent_ActivityId]
    ON [dbo].[ActivityEvent]([ActivityId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ActivityEvent_EventId]
    ON [dbo].[ActivityEvent]([EventId] ASC);

