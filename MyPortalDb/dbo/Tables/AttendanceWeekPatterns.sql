CREATE TABLE [dbo].[AttendanceWeekPatterns] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (128)   NOT NULL,
    CONSTRAINT [PK_AttendanceWeekPatterns] PRIMARY KEY CLUSTERED ([Id] ASC)
);

