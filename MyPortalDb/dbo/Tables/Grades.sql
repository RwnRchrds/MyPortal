CREATE TABLE [dbo].[Grades] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [GradeSetId]  UNIQUEIDENTIFIER NOT NULL,
    [Code]        NVARCHAR (25)    NOT NULL,
    [Description] NVARCHAR (50)    NULL,
    [Value]       DECIMAL (10, 2)  NOT NULL,
    CONSTRAINT [PK_Grades] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Grades_GradeSets_GradeSetId] FOREIGN KEY ([GradeSetId]) REFERENCES [dbo].[GradeSets] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Grades]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Grades_GradeSetId]
    ON [dbo].[Grades]([GradeSetId] ASC);

