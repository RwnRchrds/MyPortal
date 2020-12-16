CREATE TABLE [dbo].[Charges] (
    [Id]                 UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description]        NVARCHAR (256)   NOT NULL,
    [Active]             BIT              NOT NULL,
    [Code]               NVARCHAR (64)    NULL,
    [Name]               NVARCHAR (128)   NULL,
    [Amount]             DECIMAL (10, 2)  NOT NULL,
    [Variable]           BIT              NOT NULL,
    [DefaultRecurrences] INT              NOT NULL,
    CONSTRAINT [PK_Charges] PRIMARY KEY CLUSTERED ([Id] ASC)
);

