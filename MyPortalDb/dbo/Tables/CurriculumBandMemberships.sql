CREATE TABLE [dbo].[CurriculumBandMemberships] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId] UNIQUEIDENTIFIER NOT NULL,
    [BandId]    UNIQUEIDENTIFIER NOT NULL,
    [StartDate] DATETIME2 (7)    NOT NULL,
    [EndDate]   DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_CurriculumBandMemberships] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CurriculumBandMemberships_CurriculumBands_BandId] FOREIGN KEY ([BandId]) REFERENCES [dbo].[CurriculumBands] ([Id]),
    CONSTRAINT [FK_CurriculumBandMemberships_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumBandMemberships_BandId]
    ON [dbo].[CurriculumBandMemberships]([BandId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumBandMemberships_StudentId]
    ON [dbo].[CurriculumBandMemberships]([StudentId] ASC);

