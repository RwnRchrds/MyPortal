CREATE TABLE [dbo].[ExamBoards] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]    INT              IDENTITY (1, 1) NOT NULL,
    [Abbreviation] NVARCHAR (20)    NULL,
    [FullName]     NVARCHAR (128)   NULL,
    [Code]         NVARCHAR (5)     NULL,
    [Domestic]     BIT              NOT NULL,
    [UseEdi]       BIT              NOT NULL,
    [Active]       BIT              NOT NULL,
    CONSTRAINT [PK_ExamBoards] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamBoards]([ClusterId] ASC);

