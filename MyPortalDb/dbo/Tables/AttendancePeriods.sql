CREATE TABLE [dbo].[AttendancePeriods] (
    [Id]            UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [WeekPatternId] UNIQUEIDENTIFIER NOT NULL,
    [Weekday]       INT              NOT NULL,
    [Name]          NVARCHAR (128)   NOT NULL,
    [StartTime]     TIME (2)         NOT NULL,
    [EndTime]       TIME (2)         NOT NULL,
    [AmReg]         BIT              NOT NULL,
    [PmReg]         BIT              NOT NULL,
    CONSTRAINT [PK_AttendancePeriods] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AttendancePeriods_AttendanceWeekPatterns_WeekPatternId] FOREIGN KEY ([WeekPatternId]) REFERENCES [dbo].[AttendanceWeekPatterns] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AttendancePeriods_WeekPatternId]
    ON [dbo].[AttendancePeriods]([WeekPatternId] ASC);

