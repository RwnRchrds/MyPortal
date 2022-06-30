CREATE TABLE [dbo].[SystemSettings] (
    [Name]    NVARCHAR (450) NOT NULL,
    [Setting] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_SystemSettings] PRIMARY KEY CLUSTERED ([Name] ASC)
);

