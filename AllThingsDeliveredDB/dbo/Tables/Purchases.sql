CREATE TABLE [dbo].[Purchases] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [StoreID]    INT NULL,
    [CustomerID] INT NULL,
    [ProductID]  INT NULL,
    [Quantity]   INT NULL,
    CONSTRAINT [PK_Purchases] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Purchases_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([ID]) ON DELETE SET NULL,
    CONSTRAINT [FK_Purchases_Products] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ID]) ON DELETE SET NULL,
    CONSTRAINT [FK_Purchases_Stores] FOREIGN KEY ([StoreID]) REFERENCES [dbo].[Stores] ([ID]) ON DELETE SET NULL
);

