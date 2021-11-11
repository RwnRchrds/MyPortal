﻿CREATE TABLE [dbo].[Buildings] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_Buildings] PRIMARY KEY CLUSTERED ([Id] ASC)
);

