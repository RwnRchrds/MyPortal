CREATE TABLE [dbo].[TrainingCertificateStatus] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [ColourCode]  NVARCHAR (128)   NULL,
    CONSTRAINT [PK_TrainingCertificateStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);

