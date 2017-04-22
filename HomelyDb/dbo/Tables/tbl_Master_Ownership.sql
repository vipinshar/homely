CREATE TABLE [dbo].[tbl_Master_Ownership] (
    [OwnershipId] NUMERIC (18) IDENTITY (1, 1) NOT NULL,
    [Ownership]   VARCHAR (20) NOT NULL,
    [Enabled]     BIT          CONSTRAINT [DF_tbl_Ownership_Enabled] DEFAULT ((1)) NOT NULL,
    [Order]       NUMERIC (18) CONSTRAINT [DF_tbl_Ownership_Order] DEFAULT ((0)) NOT NULL,
    [CreateDate]  DATETIME     CONSTRAINT [DF_tbl_Ownership_CreateDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tbl_Ownership] PRIMARY KEY CLUSTERED ([Ownership] ASC)
);

