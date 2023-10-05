CREATE TABLE [dbo].[Results] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [ResultSetId] UNIQUEIDENTIFIER NOT NULL,
    [StudentId]   UNIQUEIDENTIFIER NOT NULL,
    [AspectId]    UNIQUEIDENTIFIER NOT NULL,
    [CreatedById] UNIQUEIDENTIFIER NOT NULL,
    [Date]        DATE             NOT NULL,
    [GradeId]     UNIQUEIDENTIFIER NULL,
    [Mark]        DECIMAL (10, 2)  NULL,
    [Comment]     NVARCHAR (1000)  NULL,
    [ColourCode]  NVARCHAR (MAX)   NULL,
    [Note]        NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Results] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Results_Aspects_AspectId] FOREIGN KEY ([AspectId]) REFERENCES [dbo].[Aspects] ([Id]),
    CONSTRAINT [FK_Results_Grades_GradeId] FOREIGN KEY ([GradeId]) REFERENCES [dbo].[Grades] ([Id]),
    CONSTRAINT [FK_Results_ResultSets_ResultSetId] FOREIGN KEY ([ResultSetId]) REFERENCES [dbo].[ResultSets] ([Id]),
    CONSTRAINT [FK_Results_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id]),
    CONSTRAINT [FK_Results_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Results]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Results_AspectId]
    ON [dbo].[Results]([AspectId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Results_CreatedById]
    ON [dbo].[Results]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Results_GradeId]
    ON [dbo].[Results]([GradeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Results_ResultSetId]
    ON [dbo].[Results]([ResultSetId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Results_StudentId]
    ON [dbo].[Results]([StudentId] ASC);

