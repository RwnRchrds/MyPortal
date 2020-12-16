CREATE TABLE [dbo].[ExamSeatAllocations] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [SittingId]   UNIQUEIDENTIFIER NOT NULL,
    [SeatId]      UNIQUEIDENTIFIER NOT NULL,
    [CandidateId] UNIQUEIDENTIFIER NOT NULL,
    [Active]      BIT              NOT NULL,
    [Attended]    BIT              NOT NULL,
    CONSTRAINT [PK_ExamSeatAllocations] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamSeatAllocations_ExamCandidate_CandidateId] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[ExamCandidate] ([Id]),
    CONSTRAINT [FK_ExamSeatAllocations_ExamComponentSittings_SittingId] FOREIGN KEY ([SittingId]) REFERENCES [dbo].[ExamComponentSittings] ([Id]),
    CONSTRAINT [FK_ExamSeatAllocations_ExamRoomSeats_SeatId] FOREIGN KEY ([SeatId]) REFERENCES [dbo].[ExamRoomSeats] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExamSeatAllocations_CandidateId]
    ON [dbo].[ExamSeatAllocations]([CandidateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamSeatAllocations_SeatId]
    ON [dbo].[ExamSeatAllocations]([SeatId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamSeatAllocations_SittingId]
    ON [dbo].[ExamSeatAllocations]([SittingId] ASC);

