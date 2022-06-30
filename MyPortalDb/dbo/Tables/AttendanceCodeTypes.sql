CREATE TABLE [dbo].[AttendanceCodeTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    CONSTRAINT [PK_AttendanceCodeTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

