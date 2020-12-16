CREATE TABLE [dbo].[ExamCandidateSpecialArrangements] (
    [Id]                   UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [CandidateId]          UNIQUEIDENTIFIER NOT NULL,
    [SpecialArrangementId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ExamCandidateSpecialArrangements] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamCandidateSpecialArrangements_ExamCandidate_CandidateId] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[ExamCandidate] ([Id]),
    CONSTRAINT [FK_ExamCandidateSpecialArrangements_ExamSpecialArrangements_SpecialArrangementId] FOREIGN KEY ([SpecialArrangementId]) REFERENCES [dbo].[ExamSpecialArrangements] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExamCandidateSpecialArrangements_CandidateId]
    ON [dbo].[ExamCandidateSpecialArrangements]([CandidateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamCandidateSpecialArrangements_SpecialArrangementId]
    ON [dbo].[ExamCandidateSpecialArrangements]([SpecialArrangementId] ASC);

