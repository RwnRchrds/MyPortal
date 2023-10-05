CREATE TABLE [dbo].[ReportCards] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]       INT              IDENTITY (1, 1) NOT NULL,
    [StudentId]       UNIQUEIDENTIFIER NOT NULL,
    [BehaviourTypeId] UNIQUEIDENTIFIER NOT NULL,
    [StartDate]       DATE             NOT NULL,
    [EndDate]         DATE             NOT NULL,
    [Comments]        NVARCHAR (256)   NULL,
    [Active]          BIT              NOT NULL,
    CONSTRAINT [PK_ReportCards] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReportCards_IncidentTypes_BehaviourTypeId] FOREIGN KEY ([BehaviourTypeId]) REFERENCES [dbo].[IncidentTypes] ([Id]),
    CONSTRAINT [FK_ReportCards_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ReportCards]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCards_BehaviourTypeId]
    ON [dbo].[ReportCards]([BehaviourTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCards_StudentId]
    ON [dbo].[ReportCards]([StudentId] ASC);

