CREATE TABLE [dbo].[AttendanceWeeks] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]      INT              IDENTITY (1, 1) NOT NULL,
    [WeekPatternId]  UNIQUEIDENTIFIER NOT NULL,
    [AcademicTermId] UNIQUEIDENTIFIER NOT NULL,
    [Beginning]      DATE             NOT NULL,
    [IsNonTimetable] BIT              NOT NULL,
    CONSTRAINT [PK_AttendanceWeeks] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AttendanceWeeks_AcademicTerms_AcademicTermId] FOREIGN KEY ([AcademicTermId]) REFERENCES [dbo].[AcademicTerms] ([Id]),
    CONSTRAINT [FK_AttendanceWeeks_AttendanceWeekPatterns_WeekPatternId] FOREIGN KEY ([WeekPatternId]) REFERENCES [dbo].[AttendanceWeekPatterns] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_AttendanceWeeks_AcademicTermId]
    ON [dbo].[AttendanceWeeks]([AcademicTermId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AttendanceWeeks_WeekPatternId]
    ON [dbo].[AttendanceWeeks]([WeekPatternId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[AttendanceWeeks]([ClusterId] ASC);

