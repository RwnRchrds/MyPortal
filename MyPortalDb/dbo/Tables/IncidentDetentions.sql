CREATE TABLE [dbo].[IncidentDetentions] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [IncidentId]  UNIQUEIDENTIFIER NOT NULL,
    [DetentionId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_IncidentDetentions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IncidentDetentions_Detentions_DetentionId] FOREIGN KEY ([DetentionId]) REFERENCES [dbo].[Detentions] ([Id]),
    CONSTRAINT [FK_IncidentDetentions_Incidents_IncidentId] FOREIGN KEY ([IncidentId]) REFERENCES [dbo].[Incidents] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_IncidentDetentions_DetentionId]
    ON [dbo].[IncidentDetentions]([DetentionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_IncidentDetentions_IncidentId]
    ON [dbo].[IncidentDetentions]([IncidentId] ASC);

