CREATE TABLE [dbo].[ExamSeatAllocations] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [SittingId]   UNIQUEIDENTIFIER NOT NULL,
    [SeatRow]     INT              NOT NULL,
    [SeatColumn]  INT              NOT NULL,
    [CandidateId] UNIQUEIDENTIFIER NOT NULL,
    [Active]      BIT              NOT NULL,
    [Attended]    BIT              NOT NULL,
    CONSTRAINT [PK_ExamSeatAllocations] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamSeatAllocations_ExamCandidate_CandidateId] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[ExamCandidate] ([Id]),
    CONSTRAINT [FK_ExamSeatAllocations_ExamComponentSittings_SittingId] FOREIGN KEY ([SittingId]) REFERENCES [dbo].[ExamComponentSittings] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamSeatAllocations]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamSeatAllocations_CandidateId]
    ON [dbo].[ExamSeatAllocations]([CandidateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamSeatAllocations_SittingId]
    ON [dbo].[ExamSeatAllocations]([SittingId] ASC);

