CREATE TABLE [dbo].[LogNoteTypes] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [ColourCode]  NVARCHAR (128)   NOT NULL,
    [IconClass]   NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_LogNoteTypes] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[LogNoteTypes]([ClusterId] ASC);

