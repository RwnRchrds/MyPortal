CREATE TABLE [dbo].[AttendanceCodes] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]            INT              IDENTITY (1, 1) NOT NULL,
    [Code]                 NVARCHAR (1)     NOT NULL,
    [Description]          NVARCHAR (128)   NOT NULL,
    [AttendanceCodeTypeId] UNIQUEIDENTIFIER NOT NULL,
    [Active]               BIT              NOT NULL,
    [Restricted]           BIT              NOT NULL,
    [System]               BIT              NOT NULL,
    CONSTRAINT [PK_AttendanceCodes] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AttendanceCodes_AttendanceCodeTypes_AttendanceCodeTypeId] FOREIGN KEY ([AttendanceCodeTypeId]) REFERENCES [dbo].[AttendanceCodeTypes] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_AttendanceCodes_AttendanceCodeTypeId]
    ON [dbo].[AttendanceCodes]([AttendanceCodeTypeId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[AttendanceCodes]([ClusterId] ASC);

