CREATE TABLE [dbo].[ParentEvenings] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]     INT              IDENTITY (1, 1) NOT NULL,
    [EventId]       UNIQUEIDENTIFIER NOT NULL,
    [Name]          NVARCHAR (128)   NULL,
    [BookingOpened] DATETIME2 (7)    NOT NULL,
    [BookingClosed] DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_ParentEvenings] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParentEvenings_DiaryEvents_EventId] FOREIGN KEY ([EventId]) REFERENCES [dbo].[DiaryEvents] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_ParentEvenings_EventId]
    ON [dbo].[ParentEvenings]([EventId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ParentEvenings]([ClusterId] ASC);

