CREATE TABLE [dbo].[Incidents] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]       INT              IDENTITY (1, 1) NOT NULL,
    [AcademicYearId]  UNIQUEIDENTIFIER NOT NULL,
    [BehaviourTypeId] UNIQUEIDENTIFIER NOT NULL,
    [LocationId]      UNIQUEIDENTIFIER NULL,
    [CreatedById]     UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]     DATE             NOT NULL,
    [Comments]        NVARCHAR (MAX)   NULL,
    [Deleted]         BIT              NOT NULL,
    CONSTRAINT [PK_Incidents] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Incidents_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [dbo].[AcademicYears] ([Id]),
    CONSTRAINT [FK_Incidents_IncidentTypes_BehaviourTypeId] FOREIGN KEY ([BehaviourTypeId]) REFERENCES [dbo].[IncidentTypes] ([Id]),
    CONSTRAINT [FK_Incidents_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Locations] ([Id]),
    CONSTRAINT [FK_Incidents_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Users] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_Incidents_AcademicYearId]
    ON [dbo].[Incidents]([AcademicYearId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Incidents_BehaviourTypeId]
    ON [dbo].[Incidents]([BehaviourTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Incidents_CreatedById]
    ON [dbo].[Incidents]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Incidents_LocationId]
    ON [dbo].[Incidents]([LocationId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Incidents]([ClusterId] ASC);

