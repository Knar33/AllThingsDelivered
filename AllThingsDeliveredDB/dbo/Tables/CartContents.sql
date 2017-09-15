CREATE TABLE [dbo].[CartContents] (
    [CartContentID]   INT             IDENTITY (1, 1) NOT NULL,
    [CustomerID]      INT             NOT NULL,
    [ItemName]        NVARCHAR (1000) NOT NULL,
    [ItemDescription] NVARCHAR (2000) NULL,
    [Quantity]        INT             NOT NULL,
    [Price]           DECIMAL (18, 2)    NULL,
    [Customize]       NVARCHAR (1000) NULL,
    [RestaurantName] NVARCHAR (1000) NOT NULL,
    [Phone]          NVARCHAR (50)   NULL,
    [Line1]       NVARCHAR (100) NULL,
    [Line2]       NVARCHAR (100) NULL,
    [City]        NVARCHAR (100) NULL,
    [State]       NVARCHAR (100) NULL,
    [ZipCode]     NVARCHAR (25)  NULL,
    [Country]     NVARCHAR (100) NULL,
    CONSTRAINT [PK_CartContents] PRIMARY KEY CLUSTERED ([CartContentID] ASC),
    CONSTRAINT [FK_CartContents_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID])
);

