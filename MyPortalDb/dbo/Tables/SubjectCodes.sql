CREATE TABLE [dbo].[SubjectCodes] (
    [Id]               UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description]      NVARCHAR (256)   NOT NULL,
    [Active]           BIT              NOT NULL,
    [Code]             NVARCHAR (MAX)   NULL,
    [SubjectCodeSetId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_SubjectCodes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SubjectCodes_SubjectCodeSets_SubjectCodeSetId] FOREIGN KEY ([SubjectCodeSetId]) REFERENCES [dbo].[SubjectCodeSets] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SubjectCodes_SubjectCodeSetId]
    ON [dbo].[SubjectCodes]([SubjectCodeSetId] ASC);

