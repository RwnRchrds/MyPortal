CREATE TABLE [dbo].[DocumentTypes] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Staff]       BIT              NOT NULL,
    [Student]     BIT              NOT NULL,
    [Contact]     BIT              NOT NULL,
    [General]     BIT              NOT NULL,
    [Sen]         BIT              NOT NULL,
    [System]      BIT              NOT NULL,
    CONSTRAINT [PK_DocumentTypes] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[DocumentTypes]([ClusterId] ASC);

