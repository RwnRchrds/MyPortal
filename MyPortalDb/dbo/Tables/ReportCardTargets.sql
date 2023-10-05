CREATE TABLE [dbo].[ReportCardTargets] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]    INT              IDENTITY (1, 1) NOT NULL,
    [ReportCardId] UNIQUEIDENTIFIER NOT NULL,
    [TargetId]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ReportCardTargets] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReportCardTargets_BehaviourTargets_TargetId] FOREIGN KEY ([TargetId]) REFERENCES [dbo].[BehaviourTargets] ([Id]),
    CONSTRAINT [FK_ReportCardTargets_ReportCards_ReportCardId] FOREIGN KEY ([ReportCardId]) REFERENCES [dbo].[ReportCards] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ReportCardTargets]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardTargets_ReportCardId]
    ON [dbo].[ReportCardTargets]([ReportCardId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardTargets_TargetId]
    ON [dbo].[ReportCardTargets]([TargetId] ASC);

