CREATE TABLE [dbo].[Restaurants] (
    [RestaurantID]   INT             IDENTITY (1, 1) NOT NULL,
    [RestaurantName] NVARCHAR (1000) NOT NULL,
    [AddressID]      INT             NOT NULL,
    CONSTRAINT [PK_Restaurants] PRIMARY KEY CLUSTERED ([RestaurantID] ASC),
    CONSTRAINT [FK_Restaurants_Addresses] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Addresses] ([AddressID])
);

