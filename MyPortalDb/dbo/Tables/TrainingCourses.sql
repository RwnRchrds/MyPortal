CREATE TABLE [dbo].[TrainingCourses] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Code]        NVARCHAR (128)   NOT NULL,
    [Name]        NVARCHAR (128)   NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_TrainingCourses] PRIMARY KEY CLUSTERED ([Id] ASC)
);

