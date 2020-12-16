CREATE TABLE [dbo].[AttendanceCodeMeanings] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    CONSTRAINT [PK_AttendanceCodeMeanings] PRIMARY KEY CLUSTERED ([Id] ASC)
);

