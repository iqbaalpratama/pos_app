CREATE TABLE [dbo].[Prod] (
    [ProdId]        INT          NOT NULL,
    [ProdName]      VARCHAR (50) NOT NULL,
    [ProdQty]       INT          NOT NULL,
    [ProdCat]       VARCHAR (50) NOT NULL,
    [ProdPriceBuy]  INT          NOT NULL,
    [ProdPriceSell] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([ProdId] ASC)
);

