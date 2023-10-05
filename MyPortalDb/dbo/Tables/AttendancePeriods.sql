CREATE TABLE [dbo].[AttendancePeriods] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]     INT              IDENTITY (1, 1) NOT NULL,
    [WeekPatternId] UNIQUEIDENTIFIER NOT NULL,
    [Weekday]       INT              NOT NULL,
    [Name]          NVARCHAR (128)   NOT NULL,
    [StartTime]     TIME (2)         NOT NULL,
    [EndTime]       TIME (2)         NOT NULL,
    [AmReg]         BIT              NOT NULL,
    [PmReg]         BIT              NOT NULL,
    CONSTRAINT [PK_AttendancePeriods] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AttendancePeriods_AttendanceWeekPatterns_WeekPatternId] FOREIGN KEY ([WeekPatternId]) REFERENCES [dbo].[AttendanceWeekPatterns] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[AttendancePeriods]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AttendancePeriods_WeekPatternId]
    ON [dbo].[AttendancePeriods]([WeekPatternId] ASC);

