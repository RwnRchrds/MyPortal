CREATE TABLE [dbo].[Detentions] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]       INT              IDENTITY (1, 1) NOT NULL,
    [DetentionTypeId] UNIQUEIDENTIFIER NOT NULL,
    [EventId]         UNIQUEIDENTIFIER NOT NULL,
    [SupervisorId]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Detentions] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Detentions_DetentionTypes_DetentionTypeId] FOREIGN KEY ([DetentionTypeId]) REFERENCES [dbo].[DetentionTypes] ([Id]),
    CONSTRAINT [FK_Detentions_DiaryEvents_EventId] FOREIGN KEY ([EventId]) REFERENCES [dbo].[DiaryEvents] ([Id]),
    CONSTRAINT [FK_Detentions_StaffMembers_SupervisorId] FOREIGN KEY ([SupervisorId]) REFERENCES [dbo].[StaffMembers] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_Detentions_DetentionTypeId]
    ON [dbo].[Detentions]([DetentionTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Detentions_EventId]
    ON [dbo].[Detentions]([EventId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Detentions_SupervisorId]
    ON [dbo].[Detentions]([SupervisorId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Detentions]([ClusterId] ASC);

