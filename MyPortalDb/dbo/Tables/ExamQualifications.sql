﻿CREATE TABLE [dbo].[ExamQualifications] (
    [Id]                  UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [JcQualificationCode] NVARCHAR (MAX)   NULL,
    [System]              BIT              NOT NULL,
    [Description]         NVARCHAR (256)   NOT NULL,
    [Active]              BIT              NOT NULL,
    CONSTRAINT [PK_ExamQualifications] PRIMARY KEY CLUSTERED ([Id] ASC)
);

