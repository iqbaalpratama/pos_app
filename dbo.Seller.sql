CREATE TABLE [dbo].[Seller] (
    [SellerId]    INT          NOT NULL,
    [SellerName]  VARCHAR (50) NOT NULL,
    [SellerAge]   INT          NOT NULL,
    [SellerPhone] VARCHAR (50) NOT NULL,
    [SellerEmail] VARCHAR (50) NOT NULL,
    [SellerPass]  VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([SellerId] ASC)
);

