CREATE TABLE [dbo].[ParentEvenings] (
    [Id]            UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [EventId]       UNIQUEIDENTIFIER NOT NULL,
    [BookingOpened] DATETIME2 (7)    NOT NULL,
    [BookingClosed] DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_ParentEvenings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParentEvenings_DiaryEvents_EventId] FOREIGN KEY ([EventId]) REFERENCES [dbo].[DiaryEvents] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ParentEvenings_EventId]
    ON [dbo].[ParentEvenings]([EventId] ASC);

