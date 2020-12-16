CREATE TABLE [dbo].[ReportCardTargets] (
    [Id]           UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ReportCardId] UNIQUEIDENTIFIER NOT NULL,
    [TargetId]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ReportCardTargets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReportCardTargets_BehaviourTargets_TargetId] FOREIGN KEY ([TargetId]) REFERENCES [dbo].[BehaviourTargets] ([Id]),
    CONSTRAINT [FK_ReportCardTargets_ReportCards_ReportCardId] FOREIGN KEY ([ReportCardId]) REFERENCES [dbo].[ReportCards] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardTargets_ReportCardId]
    ON [dbo].[ReportCardTargets]([ReportCardId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardTargets_TargetId]
    ON [dbo].[ReportCardTargets]([TargetId] ASC);

