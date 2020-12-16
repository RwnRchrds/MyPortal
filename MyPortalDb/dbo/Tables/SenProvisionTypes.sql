CREATE TABLE [dbo].[SenProvisionTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_SenProvisionTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

