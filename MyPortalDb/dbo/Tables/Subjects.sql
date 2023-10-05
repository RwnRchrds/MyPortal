CREATE TABLE [dbo].[Subjects] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]     INT              IDENTITY (1, 1) NOT NULL,
    [SubjectCodeId] UNIQUEIDENTIFIER NOT NULL,
    [Name]          NVARCHAR (256)   NOT NULL,
    [Code]          NVARCHAR (5)     NOT NULL,
    [Deleted]       BIT              NOT NULL,
    CONSTRAINT [PK_Subjects] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Subjects_SubjectCodes_SubjectCodeId] FOREIGN KEY ([SubjectCodeId]) REFERENCES [dbo].[SubjectCodes] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Subjects]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Subjects_SubjectCodeId]
    ON [dbo].[Subjects]([SubjectCodeId] ASC);

