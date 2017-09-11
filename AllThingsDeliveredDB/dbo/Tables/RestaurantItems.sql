CREATE TABLE [dbo].[RestaurantItems] (
    [RestaurantItemID]     INT             IDENTITY (1, 1) NOT NULL,
    [ItemName]             NVARCHAR (1000) NOT NULL,
    [ItemDescription]      NVARCHAR (2000) NULL,
    [RestaurantCategoryID] INT             NULL,
    [Price]                DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_RestaurantItems] PRIMARY KEY CLUSTERED ([RestaurantItemID] ASC),
    CONSTRAINT [FK_RestaurantItems_RestaurantCategories] FOREIGN KEY ([RestaurantCategoryID]) REFERENCES [dbo].[RestaurantCategories] ([RestaurantCategoryID])
);





