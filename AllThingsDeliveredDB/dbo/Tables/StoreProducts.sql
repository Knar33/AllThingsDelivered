CREATE TABLE [dbo].[StoreProducts] (
    [StoreID]   INT NOT NULL,
    [ProductID] INT NOT NULL,
    [Stock]     INT NOT NULL,
    CONSTRAINT [PK_StoreProducts] PRIMARY KEY CLUSTERED ([StoreID] ASC, [ProductID] ASC),
    CONSTRAINT [FK_StoreProducts_Products] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_StoreProducts_Stores] FOREIGN KEY ([StoreID]) REFERENCES [dbo].[Stores] ([ID]) ON DELETE CASCADE
);

