CREATE TABLE [dbo].[LocalAuthorities] (
    [Id]      UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [LeaCode] INT              NOT NULL,
    [Name]    NVARCHAR (128)   NOT NULL,
    [Website] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_LocalAuthorities] PRIMARY KEY CLUSTERED ([Id] ASC)
);

