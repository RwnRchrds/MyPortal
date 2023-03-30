CREATE TABLE [dbo].[People] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]          INT              IDENTITY (1, 1) NOT NULL,
    [DirectoryId]        UNIQUEIDENTIFIER NOT NULL,
    [Title]              NVARCHAR (128)   NULL,
    [PreferredFirstName] NVARCHAR (256)   NULL,
    [PreferredLastName]  NVARCHAR (256)   NULL,
    [FirstName]          NVARCHAR (256)   NOT NULL,
    [MiddleName]         NVARCHAR (256)   NULL,
    [LastName]           NVARCHAR (256)   NOT NULL,
    [PhotoId]            UNIQUEIDENTIFIER NULL,
    [NhsNumber]          NVARCHAR (10)    NULL,
    [CreatedDate]        DATETIME2 (7)    NOT NULL,
    [Gender]             CHAR (1)         NOT NULL,
    [Dob]                DATE             NULL,
    [Deceased]           DATE             NULL,
    [EthnicityId]        UNIQUEIDENTIFIER NULL,
    [Deleted]            BIT              NOT NULL,
    CONSTRAINT [PK_People] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_People_Directories_DirectoryId] FOREIGN KEY ([DirectoryId]) REFERENCES [dbo].[Directories] ([Id]),
    CONSTRAINT [FK_People_Ethnicities_EthnicityId] FOREIGN KEY ([EthnicityId]) REFERENCES [dbo].[Ethnicities] ([Id]),
    CONSTRAINT [FK_People_Photos_PhotoId] FOREIGN KEY ([PhotoId]) REFERENCES [dbo].[Photos] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_People_DirectoryId]
    ON [dbo].[People]([DirectoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_People_EthnicityId]
    ON [dbo].[People]([EthnicityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_People_PhotoId]
    ON [dbo].[People]([PhotoId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[People]([ClusterId] ASC);

