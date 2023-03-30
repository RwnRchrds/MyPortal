﻿CREATE TABLE [dbo].[EnrolmentStatus] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Code]        NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_EnrolmentStatus] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[EnrolmentStatus]([ClusterId] ASC);

