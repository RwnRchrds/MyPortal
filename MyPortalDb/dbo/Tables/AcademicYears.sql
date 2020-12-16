CREATE TABLE [dbo].[AcademicYears] (
    [Id]     UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Name]   NVARCHAR (128)   NOT NULL,
    [Locked] BIT              NOT NULL,
    CONSTRAINT [PK_AcademicYears] PRIMARY KEY CLUSTERED ([Id] ASC)
);

