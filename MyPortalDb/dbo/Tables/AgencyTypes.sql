CREATE TABLE [dbo].[AgencyTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_AgencyTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

