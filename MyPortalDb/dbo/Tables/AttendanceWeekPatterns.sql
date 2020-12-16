CREATE TABLE [dbo].[AttendanceWeekPatterns] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [AcademicYearId] UNIQUEIDENTIFIER NOT NULL,
    [Order]          INT              NOT NULL,
    [Description]    NVARCHAR (128)   NOT NULL,
    CONSTRAINT [PK_AttendanceWeekPatterns] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AttendanceWeekPatterns_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [dbo].[AcademicYears] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AttendanceWeekPatterns_AcademicYearId]
    ON [dbo].[AttendanceWeekPatterns]([AcademicYearId] ASC);

