CREATE TABLE [dbo].[TrainingCourses] (
    [Id]   UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Code] NVARCHAR (128)   NOT NULL,
    [Name] NVARCHAR (128)   NOT NULL,
    CONSTRAINT [PK_TrainingCourses] PRIMARY KEY CLUSTERED ([Id] ASC)
);

