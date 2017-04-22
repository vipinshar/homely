CREATE TABLE [dbo].[tbl_Master_Transaction] (
    [TransactionId] NUMERIC (18) IDENTITY (1, 1) NOT NULL,
    [Transaction]   VARCHAR (20) NOT NULL,
    [Enabled]       BIT          CONSTRAINT [DF_tbl_Transaction_Enabled] DEFAULT ((1)) NOT NULL,
    [Order]         NUMERIC (18) CONSTRAINT [DF_tbl_Transaction_Order] DEFAULT ((0)) NOT NULL,
    [CreateDate]    DATETIME     CONSTRAINT [DF_tbl_Transaction_CreateDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tbl_Transaction] PRIMARY KEY CLUSTERED ([Transaction] ASC)
);

