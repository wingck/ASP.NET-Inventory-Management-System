CREATE TABLE [dbo].[Items] (
    [ItemID]   INT            IDENTITY (1, 1) NOT NULL,
    [ItemName] NVARCHAR (MAX) NULL,
    [ItemNum]  INT            NOT NULL,
    CONSTRAINT [PK_dbo.Items] PRIMARY KEY CLUSTERED ([ItemID] ASC)
);
GO