CREATE TABLE [dbo].[RestaurantCategories] (
    [RestaurantCategoryID] INT             IDENTITY (1, 1) NOT NULL,
    [RestaurantID]         INT             NOT NULL,
    [CategoryName]         NVARCHAR (1000) NULL,
    CONSTRAINT [PK_RestaurantCategories] PRIMARY KEY CLUSTERED ([RestaurantCategoryID] ASC),
    CONSTRAINT [FK_RestaurantCategories_Restaurants] FOREIGN KEY ([RestaurantID]) REFERENCES [dbo].[Restaurants] ([RestaurantID])
);



