CREATE TABLE [dbo].[ReportCardTargetSubmissions] (
    [Id]              UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [SubmissionId]    UNIQUEIDENTIFIER NOT NULL,
    [TargetId]        UNIQUEIDENTIFIER NOT NULL,
    [TargetCompleted] BIT              NOT NULL,
    CONSTRAINT [PK_ReportCardTargetSubmissions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReportCardTargetSubmissions_ReportCardSubmissions_SubmissionId] FOREIGN KEY ([SubmissionId]) REFERENCES [dbo].[ReportCardSubmissions] ([Id]),
    CONSTRAINT [FK_ReportCardTargetSubmissions_ReportCardTargets_TargetId] FOREIGN KEY ([TargetId]) REFERENCES [dbo].[ReportCardTargets] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardTargetSubmissions_SubmissionId]
    ON [dbo].[ReportCardTargetSubmissions]([SubmissionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ReportCardTargetSubmissions_TargetId]
    ON [dbo].[ReportCardTargetSubmissions]([TargetId] ASC);

