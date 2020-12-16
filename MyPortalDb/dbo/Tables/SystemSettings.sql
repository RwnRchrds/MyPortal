CREATE TABLE [dbo].[SystemSettings] (
    [Name]    NVARCHAR (450) NOT NULL,
    [Type]    NVARCHAR (MAX) NULL,
    [Setting] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_SystemSettings] PRIMARY KEY CLUSTERED ([Name] ASC)
);

