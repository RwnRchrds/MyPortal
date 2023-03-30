CREATE TABLE [dbo].[AttendanceWeekPatterns] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (128)   NOT NULL,
    CONSTRAINT [PK_AttendanceWeekPatterns] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[AttendanceWeekPatterns]([ClusterId] ASC);

