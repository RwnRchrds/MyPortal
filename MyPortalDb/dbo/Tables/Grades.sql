CREATE TABLE [dbo].[Grades] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [GradeSetId]  UNIQUEIDENTIFIER NOT NULL,
    [Code]        NVARCHAR (25)    NOT NULL,
    [Description] NVARCHAR (50)    NULL,
    [Value]       DECIMAL (10, 2)  NOT NULL,
    CONSTRAINT [PK_Grades] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Grades_GradeSets_GradeSetId] FOREIGN KEY ([GradeSetId]) REFERENCES [dbo].[GradeSets] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Grades_GradeSetId]
    ON [dbo].[Grades]([GradeSetId] ASC);

