CREATE TABLE [dbo].[Addresses] (
    [AddressID]   INT            IDENTITY (1, 1) NOT NULL,
    [Line1]       NVARCHAR (100) NULL,
    [Line2]       NVARCHAR (100) NULL,
    [City]        NVARCHAR (100) NULL,
    [State]       NVARCHAR (100) NULL,
    [ZipCode]     NVARCHAR (25)  NULL,
    [Country]     NVARCHAR (100) NULL,
    [Deleted]     BIT            NOT NULL,
    [AddressType] NVARCHAR (100) NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED ([AddressID] ASC)
);





