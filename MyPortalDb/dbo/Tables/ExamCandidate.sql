CREATE TABLE [dbo].[ExamCandidate] (
    [Id]                      UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]               INT              IDENTITY (1, 1) NOT NULL,
    [StudentId]               UNIQUEIDENTIFIER NOT NULL,
    [Uci]                     NVARCHAR (MAX)   NULL,
    [CandidateNumber]         NVARCHAR (4)     NULL,
    [PreviousCandidateNumber] NVARCHAR (4)     NULL,
    [PreviousCentreNumber]    NVARCHAR (5)     NULL,
    [SpecialConsideration]    BIT              NOT NULL,
    [Note]                    NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ExamCandidate] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamCandidate_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_ExamCandidate_StudentId]
    ON [dbo].[ExamCandidate]([StudentId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamCandidate]([ClusterId] ASC);

