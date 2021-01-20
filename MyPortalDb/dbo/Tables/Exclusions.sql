CREATE TABLE [dbo].[Exclusions] (
    [Id]                UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId]         UNIQUEIDENTIFIER NOT NULL,
    [ExclusionTypeId]   UNIQUEIDENTIFIER NOT NULL,
    [ExclusionReasonId] UNIQUEIDENTIFIER NOT NULL,
    [StartDate]         DATETIME2 (7)    NOT NULL,
    [EndDate]           DATETIME2 (7)    NULL,
    [Comments]          NVARCHAR (MAX)   NULL,
    [Deleted]           BIT              NOT NULL,
    [AppealDate]        DATETIME2 (7)    NULL,
    [AppealResultDate]  DATETIME2 (7)    NULL,
    [AppealResultId]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Exclusions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Exclusions_ExclusionAppealResults_AppealResultId] FOREIGN KEY ([AppealResultId]) REFERENCES [dbo].[ExclusionAppealResults] ([Id]),
    CONSTRAINT [FK_Exclusions_ExclusionReasons_ExclusionReasonId] FOREIGN KEY ([ExclusionReasonId]) REFERENCES [dbo].[ExclusionReasons] ([Id]),
    CONSTRAINT [FK_Exclusions_ExclusionTypes_ExclusionTypeId] FOREIGN KEY ([ExclusionTypeId]) REFERENCES [dbo].[ExclusionTypes] ([Id]),
    CONSTRAINT [FK_Exclusions_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Exclusions_AppealResultId]
    ON [dbo].[Exclusions]([AppealResultId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Exclusions_ExclusionReasonId]
    ON [dbo].[Exclusions]([ExclusionReasonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Exclusions_ExclusionTypeId]
    ON [dbo].[Exclusions]([ExclusionTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Exclusions_StudentId]
    ON [dbo].[Exclusions]([StudentId] ASC);

