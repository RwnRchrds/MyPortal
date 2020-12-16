CREATE TABLE [dbo].[Incidents] (
    [Id]              UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [AcademicYearId]  UNIQUEIDENTIFIER NOT NULL,
    [BehaviourTypeId] UNIQUEIDENTIFIER NOT NULL,
    [StudentId]       UNIQUEIDENTIFIER NOT NULL,
    [LocationId]      UNIQUEIDENTIFIER NULL,
    [RecordedById]    UNIQUEIDENTIFIER NOT NULL,
    [OutcomeId]       UNIQUEIDENTIFIER NOT NULL,
    [StatusId]        UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]     DATE             NOT NULL,
    [Comments]        NVARCHAR (MAX)   NULL,
    [Points]          INT              NOT NULL,
    [Deleted]         BIT              NOT NULL,
    CONSTRAINT [PK_Incidents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Incidents_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [dbo].[AcademicYears] ([Id]),
    CONSTRAINT [FK_Incidents_BehaviourOutcomes_OutcomeId] FOREIGN KEY ([OutcomeId]) REFERENCES [dbo].[BehaviourOutcomes] ([Id]),
    CONSTRAINT [FK_Incidents_BehaviourStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[BehaviourStatus] ([Id]),
    CONSTRAINT [FK_Incidents_IncidentTypes_BehaviourTypeId] FOREIGN KEY ([BehaviourTypeId]) REFERENCES [dbo].[IncidentTypes] ([Id]),
    CONSTRAINT [FK_Incidents_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Locations] ([Id]),
    CONSTRAINT [FK_Incidents_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id]),
    CONSTRAINT [FK_Incidents_Users_RecordedById] FOREIGN KEY ([RecordedById]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Incidents_AcademicYearId]
    ON [dbo].[Incidents]([AcademicYearId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Incidents_BehaviourTypeId]
    ON [dbo].[Incidents]([BehaviourTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Incidents_LocationId]
    ON [dbo].[Incidents]([LocationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Incidents_OutcomeId]
    ON [dbo].[Incidents]([OutcomeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Incidents_RecordedById]
    ON [dbo].[Incidents]([RecordedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Incidents_StatusId]
    ON [dbo].[Incidents]([StatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Incidents_StudentId]
    ON [dbo].[Incidents]([StudentId] ASC);

