CREATE TABLE [dbo].[StudentIncidentDetentions] (
    [Id]                UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentIncidentId] UNIQUEIDENTIFIER NOT NULL,
    [DetentionId]       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_StudentIncidentDetentions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentIncidentDetentions_Detentions_DetentionId] FOREIGN KEY ([DetentionId]) REFERENCES [dbo].[Detentions] ([Id]),
    CONSTRAINT [FK_StudentIncidentDetentions_StudentIncidents_StudentIncidentId] FOREIGN KEY ([StudentIncidentId]) REFERENCES [dbo].[StudentIncidents] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_StudentIncidentDetentions_DetentionId]
    ON [dbo].[StudentIncidentDetentions]([DetentionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentIncidentDetentions_StudentIncidentId]
    ON [dbo].[StudentIncidentDetentions]([StudentIncidentId] ASC);

