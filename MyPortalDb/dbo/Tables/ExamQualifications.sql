CREATE TABLE [dbo].[ExamQualifications] (
    [Id]                  UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description]         NVARCHAR (256)   NOT NULL,
    [Active]              BIT              NOT NULL,
    [JcQualificationCode] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ExamQualifications] PRIMARY KEY CLUSTERED ([Id] ASC)
);

