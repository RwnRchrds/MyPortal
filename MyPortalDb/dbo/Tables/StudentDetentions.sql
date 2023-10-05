CREATE TABLE [dbo].[StudentDetentions] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]        INT              IDENTITY (1, 1) NOT NULL,
    [StudentId]        UNIQUEIDENTIFIER NOT NULL,
    [DetentionId]      UNIQUEIDENTIFIER NOT NULL,
    [LinkedIncidentId] UNIQUEIDENTIFIER NULL,
    [Attended]         BIT              NOT NULL,
    [Notes]            NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_StudentDetentions] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentDetentions_Detentions_DetentionId] FOREIGN KEY ([DetentionId]) REFERENCES [dbo].[Detentions] ([Id]),
    CONSTRAINT [FK_StudentDetentions_StudentIncidents_LinkedIncidentId] FOREIGN KEY ([LinkedIncidentId]) REFERENCES [dbo].[StudentIncidents] ([Id]),
    CONSTRAINT [FK_StudentDetentions_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[StudentDetentions]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentDetentions_DetentionId]
    ON [dbo].[StudentDetentions]([DetentionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentDetentions_LinkedIncidentId]
    ON [dbo].[StudentDetentions]([LinkedIncidentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentDetentions_StudentId]
    ON [dbo].[StudentDetentions]([StudentId] ASC);

