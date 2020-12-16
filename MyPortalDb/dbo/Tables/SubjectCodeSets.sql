CREATE TABLE [dbo].[SubjectCodeSets] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_SubjectCodeSets] PRIMARY KEY CLUSTERED ([Id] ASC)
);

