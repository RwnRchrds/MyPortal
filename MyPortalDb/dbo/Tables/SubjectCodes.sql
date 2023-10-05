CREATE TABLE [dbo].[SubjectCodes] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]        INT              IDENTITY (1, 1) NOT NULL,
    [Description]      NVARCHAR (256)   NOT NULL,
    [Active]           BIT              NOT NULL,
    [Code]             NVARCHAR (MAX)   NULL,
    [SubjectCodeSetId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_SubjectCodes] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SubjectCodes_SubjectCodeSets_SubjectCodeSetId] FOREIGN KEY ([SubjectCodeSetId]) REFERENCES [dbo].[SubjectCodeSets] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[SubjectCodes]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SubjectCodes_SubjectCodeSetId]
    ON [dbo].[SubjectCodes]([SubjectCodeSetId] ASC);

