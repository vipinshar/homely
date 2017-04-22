CREATE TABLE [dbo].[tbl_Master_Furnished] (
    [FurnishedId] NUMERIC (18) IDENTITY (1, 1) NOT NULL,
    [Furnished]   VARCHAR (20) NOT NULL,
    [Enabled]     BIT          CONSTRAINT [DF_tbl_Master_Furnished_Enabled] DEFAULT ((1)) NOT NULL,
    [Order]       NUMERIC (18) CONSTRAINT [DF_tbl_Master_Furnished_Order] DEFAULT ((0)) NOT NULL,
    [CreateDate]  DATETIME     CONSTRAINT [DF_tbl_Master_Furnished_CreateDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tbl_Master_Furnished] PRIMARY KEY CLUSTERED ([Furnished] ASC)
);

