CREATE TABLE [dbo].[IgnoredRestaurants] (
    [IgnoredRestaurantsID] INT            IDENTITY (1, 1) NOT NULL,
    [RestaurantFSID]       NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_IgnoredRestaurants] PRIMARY KEY CLUSTERED ([IgnoredRestaurantsID] ASC)
);



