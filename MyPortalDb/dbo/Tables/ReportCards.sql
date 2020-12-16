CREATE TABLE [dbo].[ReportCards] (
    [Id]              UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId]       UNIQUEIDENTIFIER NOT NULL,
    [BehaviourTypeId] UNIQUEIDENTIFIER NOT NULL,
    [StartDate]       DATE             NOT NULL,
    [EndDate]         DATE             NOT NULL,
    [Comments]        NVARCHAR (256)   NULL,
    [Active]          BIT              NOT NULL,
    CONSTRAINT [PK_ReportCards] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReportCards_IncidentTypes_BehaviourTypeId] FOREIGN KEY ([BehaviourTypeId]) REFERENCES [dbo].[IncidentTypes] ([Id]),
    CONSTRAINT [FK_ReportCards_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCards_BehaviourTypeId]
    ON [dbo].[ReportCards]([BehaviourTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCards_StudentId]
    ON [dbo].[ReportCards]([StudentId] ASC);

