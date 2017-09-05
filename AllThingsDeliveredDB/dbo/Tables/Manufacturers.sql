CREATE TABLE [dbo].[Manufacturers] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (256) NOT NULL,
    [AddressID] INT            NULL,
    CONSTRAINT [PK_Manufacturers] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Manufacturers_Addresses] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Addresses] ([ID]) ON DELETE SET NULL,
    CONSTRAINT [UQ_Manufacturers_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

