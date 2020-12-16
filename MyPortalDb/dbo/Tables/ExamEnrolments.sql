CREATE TABLE [dbo].[ExamEnrolments] (
    [Id]                 UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [AwardId]            UNIQUEIDENTIFIER NOT NULL,
    [CandidateId]        UNIQUEIDENTIFIER NOT NULL,
    [StartDate]          DATETIME2 (7)    NOT NULL,
    [EndDate]            DATETIME2 (7)    NULL,
    [RegistrationNumber] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ExamEnrolments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamEnrolments_ExamAwards_AwardId] FOREIGN KEY ([AwardId]) REFERENCES [dbo].[ExamAwards] ([Id]),
    CONSTRAINT [FK_ExamEnrolments_ExamCandidate_CandidateId] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[ExamCandidate] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExamEnrolments_AwardId]
    ON [dbo].[ExamEnrolments]([AwardId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamEnrolments_CandidateId]
    ON [dbo].[ExamEnrolments]([CandidateId] ASC);

