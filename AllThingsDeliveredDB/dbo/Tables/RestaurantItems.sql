CREATE TABLE [dbo].[RestaurantItems] (
    [RestaurantItemID]     INT             IDENTITY (1, 1) NOT NULL,
    [ItemName]             NVARCHAR (1000) NOT NULL,
    [ItemDescription]      NVARCHAR (2000) NOT NULL,
    [RestaurantID]         INT             NOT NULL,
    [RestaurantCategoryID] INT             NULL,
    CONSTRAINT [PK_RestaurantItems] PRIMARY KEY CLUSTERED ([RestaurantItemID] ASC),
    CONSTRAINT [FK_RestaurantItems_RestaurantCategories] FOREIGN KEY ([RestaurantCategoryID]) REFERENCES [dbo].[RestaurantCategories] ([RestaurantCategoryID]),
    CONSTRAINT [FK_RestaurantItems_Restaurants] FOREIGN KEY ([RestaurantID]) REFERENCES [dbo].[Restaurants] ([RestaurantID])
);

