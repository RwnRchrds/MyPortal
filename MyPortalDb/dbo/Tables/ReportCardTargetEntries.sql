﻿CREATE TABLE [dbo].[ReportCardTargetEntries] (
    [Id]              UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [EntryId]         UNIQUEIDENTIFIER NOT NULL,
    [TargetId]        UNIQUEIDENTIFIER NOT NULL,
    [TargetCompleted] BIT              NOT NULL,
    CONSTRAINT [PK_ReportCardTargetEntries] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReportCardTargetEntries_ReportCardEntries_EntryId] FOREIGN KEY ([EntryId]) REFERENCES [dbo].[ReportCardEntries] ([Id]),
    CONSTRAINT [FK_ReportCardTargetEntries_ReportCardTargets_TargetId] FOREIGN KEY ([TargetId]) REFERENCES [dbo].[ReportCardTargets] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardTargetEntries_EntryId]
    ON [dbo].[ReportCardTargetEntries]([EntryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardTargetEntries_TargetId]
    ON [dbo].[ReportCardTargetEntries]([TargetId] ASC);
