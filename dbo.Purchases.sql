CREATE TABLE [dbo].[Purchases] (
    [PurchaseID]  INT            IDENTITY (1, 1) NOT NULL,
    [PurchaseNum] NVARCHAR (MAX) NULL,
    [ItemID]      INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Purchases] PRIMARY KEY CLUSTERED ([PurchaseID] ASC),
    CONSTRAINT [FK_dbo.Purchases_dbo.Items_ItemID] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Items] ([ItemID]) ON DELETE CASCADE
);
GO

-- Create the non-clustered index
CREATE NONCLUSTERED INDEX [IX_ItemID] ON [dbo].[Purchases]([ItemID] ASC);
GO

-- Create the trigger on the Purchases table
GO
CREATE TRIGGER trg_Purchases_AfterInsert ON Purchases
AFTER INSERT
AS
BEGIN
    -- Update the ItemNum in the Items table
    UPDATE Items
    SET ItemNum = ItemNum - (SELECT PurchaseNum FROM inserted WHERE Items.ItemID = inserted.ItemID)
    FROM Items
    INNER JOIN inserted ON Items.ItemID = inserted.ItemID
END;