CREATE TABLE [dbo].[DocumentTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Staff]       BIT              NOT NULL,
    [Student]     BIT              NOT NULL,
    [Contact]     BIT              NOT NULL,
    [General]     BIT              NOT NULL,
    [Sen]         BIT              NOT NULL,
    CONSTRAINT [PK_DocumentTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

