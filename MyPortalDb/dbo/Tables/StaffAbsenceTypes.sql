CREATE TABLE [dbo].[StaffAbsenceTypes] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [System]      BIT              NOT NULL,
    [Authorised]  BIT              NOT NULL,
    CONSTRAINT [PK_StaffAbsenceTypes] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[StaffAbsenceTypes]([ClusterId] ASC);

