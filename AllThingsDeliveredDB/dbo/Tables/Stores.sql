CREATE TABLE [dbo].[Stores] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (100) NOT NULL,
    [AddressID] INT            NULL,
    CONSTRAINT [PK_Stores] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Stores_Addresses] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Addresses] ([ID]) ON DELETE SET NULL
);

