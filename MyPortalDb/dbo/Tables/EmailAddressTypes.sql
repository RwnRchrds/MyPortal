CREATE TABLE [dbo].[EmailAddressTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_EmailAddressTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

