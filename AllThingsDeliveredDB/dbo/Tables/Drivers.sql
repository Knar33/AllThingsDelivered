CREATE TABLE [dbo].[Drivers] (
    [DriverID]  INT            IDENTITY (1, 1) NOT NULL,
    [AddressID] INT            NULL,
    [FirstName] NVARCHAR (100) NOT NULL,
    [LastName]  NVARCHAR (100) NOT NULL,
    [ClockedIn] BIT            NOT NULL,
    [StartDate] DATE           NOT NULL,
    [EndDate]   DATE           NOT NULL,
    CONSTRAINT [PK_Drivers] PRIMARY KEY CLUSTERED ([DriverID] ASC),
    CONSTRAINT [FK_Drivers_Addresses] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Addresses] ([AddressID]) ON DELETE SET NULL
);

