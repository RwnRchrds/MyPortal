CREATE TABLE [dbo].[ExamQualifications] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]           INT              IDENTITY (1, 1) NOT NULL,
    [Description]         NVARCHAR (256)   NOT NULL,
    [Active]              BIT              NOT NULL,
    [JcQualificationCode] NVARCHAR (MAX)   NULL,
    [System]              BIT              NOT NULL,
    CONSTRAINT [PK_ExamQualifications] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamQualifications]([ClusterId] ASC);

