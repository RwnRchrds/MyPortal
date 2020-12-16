CREATE TABLE [dbo].[ObservationOutcomes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [ColourCode]  NVARCHAR (128)   NULL,
    CONSTRAINT [PK_ObservationOutcomes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

