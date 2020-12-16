CREATE TABLE [dbo].[Results] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ResultSetId] UNIQUEIDENTIFIER NOT NULL,
    [StudentId]   UNIQUEIDENTIFIER NOT NULL,
    [AspectId]    UNIQUEIDENTIFIER NOT NULL,
    [Date]        DATE             NOT NULL,
    [GradeId]     UNIQUEIDENTIFIER NULL,
    [Mark]        DECIMAL (10, 2)  NULL,
    [Comments]    NVARCHAR (MAX)   NULL,
    [Note]        NVARCHAR (MAX)   NULL,
    [ColourCode]  NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Results] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Results_Aspects_AspectId] FOREIGN KEY ([AspectId]) REFERENCES [dbo].[Aspects] ([Id]),
    CONSTRAINT [FK_Results_Grades_GradeId] FOREIGN KEY ([GradeId]) REFERENCES [dbo].[Grades] ([Id]),
    CONSTRAINT [FK_Results_ResultSets_ResultSetId] FOREIGN KEY ([ResultSetId]) REFERENCES [dbo].[ResultSets] ([Id]),
    CONSTRAINT [FK_Results_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Results_AspectId]
    ON [dbo].[Results]([AspectId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Results_GradeId]
    ON [dbo].[Results]([GradeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Results_ResultSetId]
    ON [dbo].[Results]([ResultSetId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Results_StudentId]
    ON [dbo].[Results]([StudentId] ASC);

