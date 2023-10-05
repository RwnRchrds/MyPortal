CREATE TABLE [dbo].[Exclusions] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]         INT              IDENTITY (1, 1) NOT NULL,
    [StudentId]         UNIQUEIDENTIFIER NOT NULL,
    [ExclusionTypeId]   UNIQUEIDENTIFIER NOT NULL,
    [ExclusionReasonId] UNIQUEIDENTIFIER NOT NULL,
    [StartDate]         DATE             NOT NULL,
    [EndDate]           DATE             NULL,
    [Comments]          NVARCHAR (MAX)   NULL,
    [Deleted]           BIT              NOT NULL,
    [AppealDate]        DATE             NULL,
    [AppealResultDate]  DATE             NULL,
    [AppealResultId]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Exclusions] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Exclusions_ExclusionAppealResults_AppealResultId] FOREIGN KEY ([AppealResultId]) REFERENCES [dbo].[ExclusionAppealResults] ([Id]),
    CONSTRAINT [FK_Exclusions_ExclusionReasons_ExclusionReasonId] FOREIGN KEY ([ExclusionReasonId]) REFERENCES [dbo].[ExclusionReasons] ([Id]),
    CONSTRAINT [FK_Exclusions_ExclusionTypes_ExclusionTypeId] FOREIGN KEY ([ExclusionTypeId]) REFERENCES [dbo].[ExclusionTypes] ([Id]),
    CONSTRAINT [FK_Exclusions_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Exclusions]([ClusterId] ASC);


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

