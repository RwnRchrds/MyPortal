CREATE TABLE [dbo].[DiaryEventAttendees] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [EventId]    UNIQUEIDENTIFIER NOT NULL,
    [PersonId]   UNIQUEIDENTIFIER NOT NULL,
    [ResponseId] UNIQUEIDENTIFIER NULL,
    [Required]   BIT              NOT NULL,
    [Attended]   BIT              NOT NULL,
    CONSTRAINT [PK_DiaryEventAttendees] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DiaryEventAttendees_DiaryEventAttendeeResponses_ResponseId] FOREIGN KEY ([ResponseId]) REFERENCES [dbo].[DiaryEventAttendeeResponses] ([Id]),
    CONSTRAINT [FK_DiaryEventAttendees_DiaryEvents_EventId] FOREIGN KEY ([EventId]) REFERENCES [dbo].[DiaryEvents] ([Id]),
    CONSTRAINT [FK_DiaryEventAttendees_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_DiaryEventAttendees_EventId]
    ON [dbo].[DiaryEventAttendees]([EventId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DiaryEventAttendees_PersonId]
    ON [dbo].[DiaryEventAttendees]([PersonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DiaryEventAttendees_ResponseId]
    ON [dbo].[DiaryEventAttendees]([ResponseId] ASC);

