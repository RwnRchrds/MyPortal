CREATE TABLE [dbo].[LogNoteTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [ColourCode]  NVARCHAR (128)   NOT NULL,
    [IconClass]   NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_LogNoteTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

