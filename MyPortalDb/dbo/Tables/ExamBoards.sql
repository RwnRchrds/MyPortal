CREATE TABLE [dbo].[ExamBoards] (
    [Id]           UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Abbreviation] NVARCHAR (20)    NULL,
    [FullName]     NVARCHAR (128)   NULL,
    [Code]         NVARCHAR (5)     NULL,
    [Domestic]     BIT              NOT NULL,
    [UseEdi]       BIT              NOT NULL,
    [Active]       BIT              NOT NULL,
    CONSTRAINT [PK_ExamBoards] PRIMARY KEY CLUSTERED ([Id] ASC)
);

