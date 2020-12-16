CREATE TABLE [dbo].[Houses] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Name]       NVARCHAR (128)   NOT NULL,
    [HeadId]     UNIQUEIDENTIFIER NULL,
    [ColourCode] NVARCHAR (128)   NULL,
    CONSTRAINT [PK_Houses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Houses_StaffMembers_HeadId] FOREIGN KEY ([HeadId]) REFERENCES [dbo].[StaffMembers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Houses_HeadId]
    ON [dbo].[Houses]([HeadId] ASC);

