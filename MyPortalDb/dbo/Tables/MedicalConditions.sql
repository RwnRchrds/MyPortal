CREATE TABLE [dbo].[MedicalConditions] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_MedicalConditions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

