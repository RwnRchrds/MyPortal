CREATE TABLE [dbo].[AttendanceCodes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Code]        NVARCHAR (1)     NOT NULL,
    [Description] NVARCHAR (128)   NOT NULL,
    [AttendanceCodeTypeId]   UNIQUEIDENTIFIER NOT NULL,
    [Active]      BIT              NOT NULL,
    [Restricted]  BIT              NOT NULL,
    [System]      BIT              NOT NULL,
    CONSTRAINT [PK_AttendanceCodes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AttendanceCodes_AttendanceCodeTypes_MeaningId] FOREIGN KEY ([AttendanceCodeTypeId]) REFERENCES [dbo].[AttendanceCodeTypes] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AttendanceCodes_MeaningId]
    ON [dbo].[AttendanceCodes]([AttendanceCodeTypeId] ASC);

