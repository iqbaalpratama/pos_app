CREATE TABLE [dbo].[Payment] (
    [paymentId]   INT          NOT NULL,
    [paymentName] VARCHAR (50) NOT NULL,
    [paymentDesc] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([paymentId] ASC)
);

