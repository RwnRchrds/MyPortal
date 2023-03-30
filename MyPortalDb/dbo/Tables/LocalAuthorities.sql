CREATE TABLE [dbo].[LocalAuthorities] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [LeaCode]   INT              NOT NULL,
    [Name]      NVARCHAR (128)   NOT NULL,
    [Website]   NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_LocalAuthorities] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[LocalAuthorities]([ClusterId] ASC);

