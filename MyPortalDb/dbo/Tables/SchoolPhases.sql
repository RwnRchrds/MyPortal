﻿CREATE TABLE [dbo].[SchoolPhases] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Code]        NVARCHAR (10)    NOT NULL,
    CONSTRAINT [PK_SchoolPhases] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[SchoolPhases]([ClusterId] ASC);

