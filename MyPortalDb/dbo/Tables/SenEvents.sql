CREATE TABLE [dbo].[SenEvents] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId]   UNIQUEIDENTIFIER NOT NULL,
    [EventTypeId] UNIQUEIDENTIFIER NOT NULL,
    [Date]        DATE             NOT NULL,
    [Note]        NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_SenEvents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SenEvents_SenEventTypes_EventTypeId] FOREIGN KEY ([EventTypeId]) REFERENCES [dbo].[SenEventTypes] ([Id]),
    CONSTRAINT [FK_SenEvents_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SenEvents_EventTypeId]
    ON [dbo].[SenEvents]([EventTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SenEvents_StudentId]
    ON [dbo].[SenEvents]([StudentId] ASC);

