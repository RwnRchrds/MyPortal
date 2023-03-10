CREATE TABLE [dbo].[PhoneNumberTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_PhoneNumberTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

