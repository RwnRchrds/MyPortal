CREATE TABLE [dbo].[DietaryRequirements] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_DietaryRequirements] PRIMARY KEY CLUSTERED ([Id] ASC)
);

